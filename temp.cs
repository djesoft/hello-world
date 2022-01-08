using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GetDate
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                bool ShowHelp = false;
                for (int i = 0; i < args.Length; i++)
                {
                    if (HelpRequired(args[i].ToLower()))
                    {
                        ShowHelp = true;
                    }
                }
                
                if (ShowHelp)
                {
                    DisplayHelp();                    
                }
                else
                {
                    int Count = 0;
                    for (int i = 0; i <= args.Length - 1; i++)
                    {
                        if (DateTime.TryParse(args[i], out DateTime SomeDT))
                        {
                            Console.Write("{0:d} ", GetJDN(SomeDT));
                            Count++;
                        }
                    }
                    Console.Write(Count);
                }
            }
            if (args.Length >= 2)
            {
                if (args[args.Length-1].ToLower() == "pause")
                {
                    Console.WriteLine("\nPress any key");
                    Console.ReadKey(true); }
            }
        }

        private static void DisplayHelp()
        {
            string ConAppName = Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Console.WriteLine("{0} {1} - HELP\n", ConAppName, FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
            Console.WriteLine("For each date/time group a value is output giving the number of minutes\nbetween 01/01/2000 00:00 and the input date/time.");
            Console.WriteLine("After all values are output the number of values is output too.");
            Console.WriteLine("If a date/time group cannot be understood it will be skipped.\n\n USAGE\n");
            Console.WriteLine("{0} [DT1] [DT2] ... [DTn]\n [pause]", ConAppName);
            Console.WriteLine("[DT1] [DT2] are date and time information from a file.");
            Console.WriteLine("The format is usually dd/mm/yyyy hh:mm\n");
            Console.WriteLine("Since this contains a space do make sure to enclose the date/time \nbetween double quotes\n");
            Console.WriteLine("[pause] waits for a key press after output displayed before continuing.\nMust be last input.\n");
            Console.WriteLine(" EXAMPLE\n");
            Console.WriteLine("{0} \"06/06/2012 18:52\" \"24/06/2012 11:27\"\n", ConAppName);
            Console.WriteLine("6538732 6564207 2");
        }

        private static bool HelpRequired(string param)
        {
            return param == "-h" || param == "--help" || param == "/?" || param=="/help";
        }

        private static int GetJDN(DateTime date)
        {
            int a= (1461 * (date.Year + 4800 + (date.Month - 14) / 12)) / 4 +
                (367 * (date.Month - 2 - 12 * ((date.Month - 14) / 12))) / 12 -
                (3 * ((date.Year + 4900 + (date.Month - 14) / 12) / 100)) / 4 + 
                date.Day - 32075;
            int b = (date.Hour * 60) + date.Minute;
            return ((a-2451545) *1440)+b;
        }
    }
}
