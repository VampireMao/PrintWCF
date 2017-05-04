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

namespace PrintService
{
    public class PrintService : IPrint
    {
        public bool Print(Dictionary<string, string> dc)
        {
            GridppReport report = new GridppReport();
            Container components = null;
            report.LoadFromFile(@"C:\Grid++Report 6\Samples\Reports\1a.简单表格.grf");
            report.Print(false);
            return true;
        }
    }
}
