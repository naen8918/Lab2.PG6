using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab2.PG6
{
    class Program
    {
        static int givenID;
        static string nodeName;
        static string nodeValue;
        static string input;
        static bool isNummeric;

        static void Main(string[] args)
        {
            /// <summary>
            /// Initializes a new instance of the CommunicationToServer class.
            /// </summary>
            CommunicationToServer cts = new CommunicationToServer();

            /// <summary>
            /// This is the iterative menu that will continue to print in the console until the user enters "e" for exit.
            /// </summary>
            while (input != "e")
            {
                Console.WriteLine();
                Console.WriteLine("Wellcome, to select method please enter the given number for each method");
                Console.WriteLine();
                Console.WriteLine("Method 1: GetTestData. Please enter 1.");
                Console.WriteLine("Method 2: GetAllInterchanges. Please enter 2.");
                Console.WriteLine("Method 3: FilterByInterchangeID. Please enter 3.");
                Console.WriteLine("Method 4: FilterByInterchangeNode. Please enter 4.");
                Console.WriteLine("Method 5: FilterByInterchangeIDAndNode. Please enter 5.");
                Console.WriteLine("Method 6: FilterByInterchangeNodeValue. Please enter 6.");
                Console.WriteLine();
                Console.WriteLine("If you wish to read data in plain text enter 'r' and press enter.");
                Console.WriteLine("If you wish to clear data enter 'c' and press enter."); 
                Console.WriteLine("If you wish to exit program enter 'e' and press enter."); 
                Console.WriteLine();
                input = Console.ReadLine();

                /// <summary>
                /// The iterative menu with different methods.
                /// </summary>
                /// <remarks>
                /// Depending on the given input different methods will be invoked using an object called cts of the CommunicationToServer class   
                /// The cts object is also used to access the variable Result which contains the result from each method.
                /// For case/method 1,2,3 and 6 the user is given the option to reformat the output which is done by calling the PlainText method.
                /// The input is checked to make sure valid input has been given.
                /// </remarks>

                switch (input)
                {
                    case "1":
                        cts.GetTestData();
                        Console.WriteLine(cts.Result);
                        Console.WriteLine();
                        Console.WriteLine("Would you like to present the data in a more readable format enter 'r' and press enter. If not please press any key.");
                        input = Console.ReadLine();
                        if (input == "r")
                        {
                            PlainText(cts.Result);
                        }
                        break;
                    case "2":
                        cts.GetAll();
                        Console.WriteLine(cts.Result);
                        Console.WriteLine();
                        Console.WriteLine("Would you like to present the data in a more readable format enter 'r' and press enter. If not please press any key.");
                        input = Console.ReadLine();
                        if (input == "r")
                        {
                            PlainText(cts.Result);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please enter ID");
                        input = Console.ReadLine();
                        if (checkIsInt(input) == true)  //if user has given a number then the string input is converted to an int.
                        {
                            givenID = Convert.ToInt32(input);
                        }
                        else                            //if user wrote a char, string then the input is in incorrect format.
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input, please try again");
                            break;
                        }
                        cts.GetFilteredByID(givenID);
                        if (cts.Result.Value.Count() != 0)      //if Result.Value is 0 then the method was unsuccesfull, meaning no results.
                        {
                            Console.WriteLine(cts.Result);
                            Console.WriteLine();
                            Console.WriteLine("Would you like to present the data in a more readable format enter 'r' and press enter. If not please press any key.");
                            input = Console.ReadLine();
                            if (input == "r")
                            {
                                PlainText(cts.Result);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No matching result, please try again.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Please enter name of node");
                        nodeName = Console.ReadLine();
                        if (checkIsInt(nodeName) == true)   //if input is of typ int then it will return true.
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input, please try again");
                            break;
                        }
                        cts.GetFilteredByNode(nodeName);
                        if (cts.Result.Value.Count() != 0)
                        {
                            Console.WriteLine(cts.Result);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No matching result, please try again.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Please enter ID");
                        input = Console.ReadLine();
                        if (checkIsInt(input) == true)
                        {
                            givenID = Convert.ToInt32(input);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input, please try again");
                            break;
                        }
                        Console.WriteLine("Please enter name of node");
                        nodeName = Console.ReadLine();
                        if (checkIsInt(nodeName) == true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input, please try again");
                            break;
                        }
                        cts.GetFilteredByIDAndNode(givenID, nodeName);
                        if (cts.Result.Value.Count() != 0)
                        {
                            Console.WriteLine(cts.Result);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No matching result, please try again.");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Please enter name of node.");
                        nodeName = Console.ReadLine();
                        Console.WriteLine("Please enter value of node.");
                        nodeValue = Console.ReadLine();
                        if (checkIsInt(nodeName) == true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid input, please try again.");
                            break;
                        }
                        cts.GetFilteredByNodeValue(nodeName, nodeValue);
                        if (cts.Result.Value.Count() != 0)
                        {
                            Console.WriteLine(cts.Result);
                            Console.WriteLine();
                            Console.WriteLine("Would you like to present the data in a more readable format enter 'r' and press enter. If not please press any key.");
                            input = Console.ReadLine();
                            if (input == "r")
                            {
                                PlainText(cts.Result);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("No matching result, please try again.");
                        }
                        break;
                    case "r":
                        PlainText(cts.Result);
                        Console.WriteLine();
                        break;
                    case "c":
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Incorrect input, try again.");
                        break;
                }
            }
            ///summary
            ///The method that prints out all latest result from one of several interchanges in plain text.
            ///The method identifies values to present Patient, Physician, Medicine and Dosage for the user.
            ///</summary>
            void PlainText(XElement xe)
            {
                try
                {
                    foreach (var item in xe.Elements("Interchange"))
                    {
                        Console.WriteLine("\n\nPatient: " + item.Descendants("FirstGivenName").FirstOrDefault().Value
                                                      + " " + item.Descendants("FamilyName").FirstOrDefault().Value);

                        Console.WriteLine("Physician: " + item.Descendants("Name").FirstOrDefault().Value);

                        XElement allMedicine = new XElement("Result",
                                         from i in item.Descendants("ProductId")
                                         select i.Value);

                        Console.WriteLine("Medicine: " + allMedicine.Value);

                        XElement allDosage = new XElement("Result",
                                      from i in item.Descendants("UnstructuredDosageAdmin")
                                      select i.Value);

                        if (item.Descendants("UnstructuredDosageAdmin").FirstOrDefault() != null)
                        {
                            Console.WriteLine("Dosage: " + allDosage.Value);
                        }
                        else
                        {
                            Console.WriteLine("Dosage: No Dosage found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
            ///<summary>
            ///Takes a string as input, the input given by the user and checks whether it's an integer.
            ///The method is used to check if user have given and integer as an input even though the program asked for a string, for example node name.
            ///</summary>
            /// <returns>A bool, true or false.</returns>
            bool checkIsInt(string input)
            {
                isNummeric = int.TryParse(input, out _);    // if input == int then method returns true else false.
                if (isNummeric == false)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}


