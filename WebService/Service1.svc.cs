using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using System.Net;
using Newtonsoft.Json;


namespace WebService
{
    public class Service1 : IService1
    {
        static XElement _testData;
        static XElement _interchanges;
        public Service1()
        {

            using (WebClient webClient = new WebClient())   //Kopplar denna till API
            {


                string jsonTestString = webClient.DownloadString(
                Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vdGVzdERhdGEuanNvbg==")));
                _testData = JsonConvert.DeserializeObject<XElement>(jsonTestString);
                //testData är den XML fil som vi ser på studentportalen
                //interchanges innehåller liknade data men mycket mer, använd testData XML från studentportalen för att förstå strukturen, noder m.m. i interchanges


                string jsonIscString = webClient.DownloadString(
                Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vaWNzLmpzb24=")));
                _interchanges = JsonConvert.DeserializeObject<XElement>(jsonIscString);
            }
        }
        public XElement GetTestData()
        {
            try
            {
                return _testData;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        public XElement GetAllInterchanges()
        {
            try
            {
                return _interchanges;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }

        public XElement FilterByInterchangeID(int id)
        {
            try
            {
                XElement interchangeByID = new XElement("InterchangeID" + id,
                                 (from td in _testData.Elements("Interchange").Elements("MessageRoutingAddress")
                                  where (int)td.Element("InterchangeRef") == id
                                  select td));

                return interchangeByID;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        public XElement FilterByInterchangeNode(string node)
        {
            try
            {
                XElement interchangeByNode = new XElement(node,
                    (from td in _testData.Descendants(node)
                     select td));

                return interchangeByNode;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        public XElement FilterByInterchangeIDAndNode(int id, string node)
        {
            try
            {
                XElement interchangeByIDNode = new XElement("InterchangeforIDandNode",
                                    (from td in _testData.Elements("Interchange")
                                     where (int)td.Element("MessageRoutingAddress").Element("InterchangeRef") == id
                                     select td.Descendants(node)));

                XElement onexElement = new XElement("Result",
                                           new XElement(node, interchangeByIDNode.Element(node).Value));

                return onexElement;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        public XElement FilterByInterchangeNodeValue(string node, string value)
        {
            try
            {
                XElement givenInfo = new XElement(node, value); // exemepl ser ut: <Name>Sten Frisk</Name>"

                XElement interchangeByNodeValue = new XElement("interchangeByNodeValue",
                            (from td in _testData.Elements("Interchange")
                             from n in td.Descendants(node)
                             where n.Value == value
                             select td).Distinct());

                return interchangeByNodeValue;

            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }


    }
}
