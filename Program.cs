using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker
{
    class Program
    {
        static void Main()
        {
            Console.Write("Enter country name\n(Special are: UK, USA, UAE, CAR)\n> ");
            string country = Console.ReadLine();

            switch(country)
            {
                case "USA":
                    country = "us";
                    break;
                case "UAE":
                    country = "united arab emirates";
                    break;
                case "CAR":
                    country = "central african republic";
                    break;

                default:
                    country.ToLower();
                    break;
            }

            country = replaceSpaces(country, '-');

            Console.Clear();

            string url = "https://www.worldometers.info/coronavirus/country/" + country + "/";
            Get(url);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void Get(string url)
        {
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(url);

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if(sLine == null)
                {
                    break;
                }
                if (sLine.Contains("<title>"))
                {
                    sLine = removeCode(sLine);
                    Console.WriteLine(sLine);

                    break;
                }
            }
        }

        private static string removeCode(string line)
        {
            string tempLine = "";

            int start = 0, end = 0;

            for(int i = 0; i < line.Length; ++i)
            {
                if(line[i] == '>')
                {
                    start = i + 1;
                    break;
                }
            }
            for (int i = line.Length - 1; i > 0; --i)
            {
                if (line[i] == '<')
                {
                    end = i - 1;
                    break;
                }
            }

            for(int i = start; i <= end; ++i)
            {
                tempLine += line[i];
            }

            return tempLine;
        }

        private static string replaceSpaces(string ctry, char replacer)
        {
            string output = "";

            for(int i = 0; i < ctry.Length; ++i)
            {
                if(ctry[i] == ' ')
                {
                    output += replacer;
                    continue;
                }
                output += ctry[i];
            }

            return output;
        }
    }
}
