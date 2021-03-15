using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab2.PG6.ServiceReference;


namespace Lab2.PG6
{
    class CommunicationToServer
    {
        private XElement result;

        public XElement Result
        {
            get { return result; }
            private set { result = value; result.Add(new XAttribute("Collected", DateTime.Now)); }
        }

        public Service1Client Clientobj { get; private set; } //new Service1Client(); 

        public CommunicationToServer()
        {
            Clientobj = new Service1Client();
            Result = new XElement("Result");
        }

        public void GetTestData()
        {
            result = Clientobj.GetTestData();     
        }
        public void GetAll()
        {
            result = Clientobj.GetAllInterchanges();
        }
        public void GetFilteredByID(int id)
        {
            result = Clientobj.FilterByInterchangeID(id);
        }
        public void GetFilteredByNode(string node)
        {
            result = Clientobj.FilterByInterchangeNode(node);
        }
        public void GetFilteredByIDAndNode(int id, string node)
        {
            result = Clientobj.FilterByInterchangeIDAndNode(id, node);
        }
        public void GetFilteredByNodeValue(string node, string nodeValue)
        {
            result = Clientobj.FilterByInterchangeNodeValue(node, nodeValue);
        }

    }
}
