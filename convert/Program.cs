using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace convert
{
    
    class Program
    {

        public static string targetCurencyValue;
        public static string baseCurency;
        public static string targetCurency;
        public static string exchaneUrl;
        

        static void Main(string[] args)
        {

            
            do
            {
                //init
                targetCurencyValue = "";
                baseCurency = "";
                targetCurency = "";
                exchaneUrl = "";

                Console.WriteLine("Enter Base Currency (? for Help) :");
                baseCurency = TestInput(Console.ReadLine().ToUpper());

                if (baseCurency == string.Empty)
                {
                    Console.Clear();
                    continue;
                }
                else if (baseCurency == "H")
                {
                    continue;
                }



                Console.WriteLine("Enter Target Currency :");
                targetCurency = TestInput(Console.ReadLine().ToUpper());

                if (targetCurency == string.Empty)
                {
                    Console.Clear();
                    continue;
                }
                

                Root myDeserializedClass = GetCurrency(baseCurency);

                if (myDeserializedClass != null)
                {
                    foreach (PropertyInfo prop in typeof(Rates).GetProperties())
                    {
                        if (prop.Name == targetCurency)
                        {
                            targetCurencyValue = prop.GetValue(myDeserializedClass.conversion_rates, null).ToString();
                            break;
                        }

                    }


                    if (targetCurencyValue != "")
                    {
                        Console.WriteLine();
                        Console.WriteLine("********************************************");
                        Console.WriteLine($"Course {myDeserializedClass.base_code} to {targetCurency} = {targetCurencyValue}");
                        Console.WriteLine("Update on : " + myDeserializedClass.time_last_update_utc.ToLocalTime());
                        Console.WriteLine("********************************************");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine($"Error : Target Currency = {targetCurency} not exist.");
                    }
                    
                }
                else
                {
                    Console.WriteLine($"Error : Base Currency = {baseCurency} not exist. ");
                }
                

                //Console.ReadLine();
                

            } while (true);
            

        }



        /// <summary>
        /// Ziskaj Konverzny list
        /// </summary>
        /// <param name="_baseCurency"></param>
        /// <returns>Trieda Root</returns>
        public static Root GetCurrency(string _baseCurency)
        {

            try
            {
                //exchaneUrl = @"https://open.er-api.com/v6/latest/" + _baseCurency;
                exchaneUrl = @"https://v6.exchangerate-api.com/v6/66ad1c754e0867995844b973/latest/" + _baseCurency;
                var json = new WebClient().DownloadString(exchaneUrl);


                var jsonData = JObject.Parse(json);


                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);

                if (myDeserializedClass.result == "success")
                {
                    return myDeserializedClass;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Missing internet connection ? " + e);
                return null;
            }

        }

        

        /// <summary>
        /// vypis / zoznam mien z triedy Rates
        /// </summary>
        static void PrintHelp()
        {
            Rates helpRates = new Rates();

            foreach (PropertyInfo prop in typeof(Rates).GetProperties())
            {

                Console.WriteLine(prop.Name);

            }

        }

        /// <summary>
        ///otestuj vstup uzivatela
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static string TestInput(string input)
        {
            string pattern = @"([a-zA-Z]{3})|\x3F";
            Regex rg = new Regex(pattern);

            if (rg.IsMatch(input))
            {
                if (input != "?")
                {
                    return input;
                }
                else
                {
                    PrintHelp();
                    return "H";
                }
            }
            else
            {
                Console.WriteLine("Not valid input");
                Console.ReadLine();
                return string.Empty;

            }
        }
    }
}

