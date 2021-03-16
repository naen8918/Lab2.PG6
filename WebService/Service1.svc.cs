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
    /// <summary>
    /// The Service1 class connects to the API and converts the JSON files to XElements
    /// It contains methods for handeling the XElements
    /// </summary>
    public class Service1 : IService1
    {
        static XElement _testData;
        static XElement _interchanges;
        public Service1()
        {
            //Connects to the API and converts the collected data from JSON to XElements.
            using (WebClient webClient = new WebClient())
            {
                string jsonTestString = webClient.DownloadString(
                Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vdGVzdERhdGEuanNvbg==")));
                _testData = JsonConvert.DeserializeObject<XElement>(jsonTestString);

                string jsonIscString = webClient.DownloadString(
                Encoding.UTF8.GetString(Convert.FromBase64String("aHR0cDovL3ByaXZhdC5iYWhuaG9mLnNlL3diNzE0ODI5L2pzb24vaWNzLmpzb24=")));
                _interchanges = JsonConvert.DeserializeObject<XElement>(jsonIscString);
            }
        }
        /// <summary>
        /// The method gets all intercahnges from the XElement _testData
        /// </summary>
        /// <returns>All interchanges</returns>
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
        /// <summary>
        /// The method gets all intercahnges from the XElement _interchanges
        /// </summary>
        /// <returns>All interchanges</returns>
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
        /// <summary>
        /// The method gets all interchanges that matches the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All interchanges which contain the given ID</returns>
        public XElement FilterByInterchangeID(int id)
        {
            try
            {
                XElement interchangeByID = new XElement("Interchanges",
                    (from i in _interchanges.Elements("Interchange")
                     from td in i.Elements("MessageRoutingAddress")
                     where (int)td.Element("InterchangeRef") == id
                     select i));

                return interchangeByID;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        /// <summary>
        /// This method finds the node that matches the input node name
        /// </summary>
        /// <param name="node"></param>
        /// <returns>XElement containing matching nodes and their value</returns>
        public XElement FilterByInterchangeNode(string node)
        {
            try
            {
                XElement interchangeByNode = new XElement(node,
                    (from td in _interchanges.Descendants(node)
                     group td by td.Value into tdgroups
                     select tdgroups.First()));

                return interchangeByNode;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        /// <summary>
        /// Finds the node and node value after the given input ID and node name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        /// <returns> XElement containing the node and it's node value. </returns>
        public XElement FilterByInterchangeIDAndNode(int id, string node)
        {
            try
            {
                XElement interchangeByIDNode = new XElement("InterchangeforIDandNode",
                                    (from td in _interchanges.Elements("Interchange")
                                     where (int)td.Element("MessageRoutingAddress").Element("InterchangeRef") == id
                                     select td.Descendants(node).FirstOrDefault()));

                return interchangeByIDNode;
            }
            catch (Exception ex)
            {
                return new XElement("Error", ex.Message);
            }
        }
        /// <summary>
        /// the method gets the interchanges that contains the input node with the same node value. 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns>The iterchanges which contains <node>value</node> as a child node. </returns>
        public XElement FilterByInterchangeNodeValue(string node, string value)
        {
            try
            {
                XElement givenInfo = new XElement(node, value);

                XElement interchangeByNodeValue = new XElement("interchangeByNodeValue",
                                           (from td in _interchanges.Elements("Interchange")
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

