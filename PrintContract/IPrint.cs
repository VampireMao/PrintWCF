using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;

namespace PrintContract
{
    [ServiceContract]
    public interface IPrint
    {
        [OperationContract]
        void Print(string orderID);
    }
}
