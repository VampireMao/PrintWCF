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
using grdes6Lib;

namespace PrintService
{
    public class PrintService : IPrint
    {
        public bool Print(Dictionary<string, string> dc)
        {
            GridppReport report = new GridppReport();
            report.LoadFromFile(@"D:\PrintWCF\PrintService\asset\print.grf");
            IGRPrinter p = report.Printer;
            p.Collate = true;
            p.Copies = int.Parse(dc["打印份数"]);
            report.ParameterByName("OrderID").AsString = dc["订单号"];
            report.ParameterByName("DateTime").AsString = dc["打印日期"];
            report.ParameterByName("SMan").AsString = dc["收货人"];
            report.ParameterByName("Flag").AsString = dc["部门"];
            report.ParameterByName("Address").AsString = dc["收货地址"];
            report.ParameterByName("Phone").AsString = dc["联系电话"];
            report.Print(false);
            return true;
        }
    }
}
