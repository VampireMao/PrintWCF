using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintContract;
using PrintService;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PrintHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(PrintService.PrintService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("服务已启动");
                };
                host.Open();
                Console.Read();
            }
        }
    }
}
