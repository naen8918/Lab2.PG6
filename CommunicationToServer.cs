using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab2.PG6.ServiceReference;


namespace Lab2.PG6
{
    /// <summary>
    /// The CommunicationToServer class connects to the ServiceReference by creating an instance of ServiceReference.
    /// Through the created object the methods from Service1 is accesed and assigned to the variable Result.
    /// </summary>
    class CommunicationToServer
    {
        private XElement result;
        public XElement Result
        {
            //Adds a new attribute to Result containing the current date and time of when Result is assigned, (when the data is collected).
            get { return result; }
            private set
            {
                result = value;
                result.Add(new XAttribute("Collected", DateTime.Now));
            }
        }
        ServiceReference.Service1Client clientobj = new ServiceReference.Service1Client();
        /// <summary>
        /// Through clientobj each methods return value is assigned to "Result" which will be used in the Program.
        /// </summary>
        public void GetTestData()
        {
            Result = clientobj.GetTestData();
        }
        public void GetAll()
        {
            Result = clientobj.GetAllInterchanges();
        }
        public void GetFilteredByID(int id)
        {
            Result = clientobj.FilterByInterchangeID(id);
        }
        public void GetFilteredByNode(string node)
        {
            Result = clientobj.FilterByInterchangeNode(node);
        }
        public void GetFilteredByIDAndNode(int id, string node)
        {
            Result = clientobj.FilterByInterchangeIDAndNode(id, node);
        }
        public void GetFilteredByNodeValue(string node, string nodeValue)
        {
            Result = clientobj.FilterByInterchangeNodeValue(node, nodeValue);
        }
    }
}

