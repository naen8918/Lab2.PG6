using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace WebService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        XElement GetTestData();
        [OperationContract]
        XElement GetAllInterchanges();
        [OperationContract]
        XElement FilterByInterchangeID(int id);
        [OperationContract]
        XElement FilterByInterchangeNode(string node);
        [OperationContract]
        XElement FilterByInterchangeIDAndNode(int id, string node);
        [OperationContract]
        XElement FilterByInterchangeNodeValue(string node, string value);

    }

}
