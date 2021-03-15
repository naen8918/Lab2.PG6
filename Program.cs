using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab2.PG6.ServiceReference;   

namespace Lab2.PG6
{
    class Program
    {
        static int givenID;
        static string nodeName;
        static string nodeValue;
        static string input;

        //felhantering vid varje input t.ex om det inte är en int

        static void Main(string[] args)
        {
            CommunicationToServer cts = new CommunicationToServer();

            while (input != "e")
            {
                Console.WriteLine("Wellcome, to select method please enter the given number for each method");
                Console.WriteLine();
                Console.WriteLine("Method 1: GetTestData. Please enter 1.");
                Console.WriteLine("Method 2: GetAllInterchanges. Please enter 2.");
                Console.WriteLine("Method 3: FilterByInterchangeID. Please enter 3.");
                Console.WriteLine("Method 4: FilterByInterchangeNode. Please enter 4.");
                Console.WriteLine("Method 5: FilterByInterchangeIDAndNode. Please enter 5.");
                Console.WriteLine("Method 6: FilterByInterchangeNodeValue. Please enter 6.");
                Console.WriteLine();
                Console.WriteLine("If you wish to present data in a more readable format enter 'r' and press enter.");
                Console.WriteLine("If you wish to clear data enter 'c' and press enter.");
                Console.WriteLine("If you wish to exit program enter 'e' and press enter.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        cts.GetTestData();
                        Console.WriteLine(cts.Result);
                        break;
                    case "2":
                        cts.GetAll();
                        Console.WriteLine(cts.Result);
                        break;
                    case "3":
                        Console.WriteLine("Please enter ID");
                        givenID =  Convert.ToInt32(Console.ReadLine());
                        cts.GetFilteredByID(givenID);
                        Console.WriteLine(cts.Result);
                        break;
                    case "4":
                        Console.WriteLine("Please enter name of node");
                        nodeName = Console.ReadLine();
                        cts.GetFilteredByNode(nodeName);
                        Console.WriteLine(cts.Result);
                        break;
                    case "5":
                        Console.WriteLine("Please enter ID");
                        givenID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please enter name of node");
                        nodeName = Console.ReadLine();
                        cts.GetFilteredByIDAndNode(givenID, nodeName);
                        Console.WriteLine(cts.Result);
                        break;
                    case "6":
                        Console.WriteLine("Please enter name of node");
                        nodeName = Console.ReadLine();
                        Console.WriteLine("Please enter value of node");
                        nodeValue = Console.ReadLine();
                        cts.GetFilteredByNodeValue(nodeName, nodeValue);
                        Console.WriteLine(cts.Result);
                        break;
                    case "r":
                        PlainText(cts.Result);
                        Console.WriteLine();
                        break;
                    case "c":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Incorrect input, try again.");
                        break;
                }
            }
            void PlainText(XElement xe) //kan vara flera interchanges
            {

                //#region Uppgift 2.2
                //var Names = from s in studenter.Descendants("Student")
                //            select s.Element("Efternamn").Value + ", " + s.Element("Fornamn").Value;

                foreach (var item in xe.Elements("Interchanges"))   //item = interchange
                {
                    var Patient = from p in item.Descendants("Name")
                                  select p.Element("Name").Value;

                    var Physician = from p in item.Descendants("Name")
                                    select "Physician: " + p.Element("Name").Value;

                    //var MoreThanTwoYears = from s in studenter.Descendants("Student")
                    //                       where (int)s.Element("AntalArStuderat") > 2
                    //                       select s.Element("ID");
                    var Medicine = from p in item.Descendants("Name")
                                   select p.Element("Name").Value;

                    var Dosage = from p in item.Descendants("Name")
                                 select p.Element("Name").Value;
                    Console.WriteLine(Physician);
                }

            }

            //clientobj.Close();
            // cts.Close();
        }
    }
}

