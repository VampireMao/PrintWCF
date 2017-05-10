using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintContract;
using System.Xml;
using System.Drawing.Printing;
using gregn6Lib;
using System.ComponentModel;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PrintService
{
    public class PrintService : IPrint
    {
        DataTable dt = new DataTable("orderinfo");
        private static string id = null;
        GridppReport report = new GridppReport();
        public PrintService()
        {
            report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FillRecordToReport);
        }

        public void Print(string orderID)
        {
            dt.Clear();
            id = orderID;
            string constr = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
            string sql = "SELECT 部门,商品名,规格,批准文号,生产厂家,数量,批号,生产日期,有效期,备注,客户名称,订单号,地址,订单日期 FROM view_select WHERE view_select.是否已删除=0 AND 订单号 LIKE @orderID";
            SqlParameter pms = new SqlParameter("@orderID", id);
            report.LoadFromFile(@".\print.grf");
            using (SqlConnection conn = new SqlConnection(constr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.Add(pms);
                conn.Open();
                adapter.Fill(dt);
            }
            report.Print(false);
        }

        private struct MatchFieldPairType
        {
            public IGRField grField;
            public int MatchColumnIndex;
        }

        public void FillRecordToReport()
        {
            MatchFieldPairType[] MatchFieldPairs = new MatchFieldPairType[Math.Min(report.DetailGrid.Recordset.Fields.Count, dt.Columns.Count)];

            //根据字段名称与列名称进行匹配，建立DataReader字段与Grid++Report记录集的字段之间的对应关系
            int MatchFieldCount = 0;
            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                foreach (IGRField fld in report.DetailGrid.Recordset.Fields)
                {
                    if (String.Compare(fld.Name, dt.Columns[i].ColumnName, true) == 0)
                    {
                        MatchFieldPairs[MatchFieldCount].grField = fld;
                        MatchFieldPairs[MatchFieldCount].MatchColumnIndex = i;
                        ++MatchFieldCount;
                        break;
                    }
                }
            }


            // 将 DataTable 中的每一条记录转储到 Grid++Report 的数据集中去
            foreach (DataRow dr in dt.Rows)
            {
                report.DetailGrid.Recordset.Append();

                for (int i = 0; i < MatchFieldCount; ++i)
                {
                    if (!dr.IsNull(MatchFieldPairs[i].MatchColumnIndex))
                        MatchFieldPairs[i].grField.Value = dr[MatchFieldPairs[i].MatchColumnIndex];
                }

                report.DetailGrid.Recordset.Post();
            }
        }
    }
}
