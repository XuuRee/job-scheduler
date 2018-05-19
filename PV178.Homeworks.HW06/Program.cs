using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PV178.Homeworks.HW06.Content;
using PV178.Homeworks.HW06.Utils.Input;
using PV178.Homeworks.HW06.Utils.Output;


namespace PV178.Homeworks.HW06
{   
    public class Program
    {
        private static readonly char[] Numbers =
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        private static readonly char[] LowerCaseLetters =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static string secretPassword = "142";

        static void Main(string[] args)
        {
            /*
            TryCharacters();
            TryNumbers();
            */
            LogHelper.OpenLogWriter();

            Console.WriteLine("Job scheduler ready.");
            ConsoleHelper.PrintTypeInCommand();
            for (;;)
            {
                var input = Console.ReadLine();
                CommandProcessor.AnalyzeInput(input);
            }
        }

        private static Task TryCharacters()
        {
            return Task.Run(() => { Dive(LowerCaseLetters, "", 0); });
        }
        
        private static Task TryNumbers()
        {
            return Task.Run(() => { Dive(Numbers, "", 0); });
        }

        private static void Dive(char[] allowed, string prefix, int level)
        {
            level += 1;
            foreach (char c in allowed)
            {
                Console.WriteLine(prefix + c);
                if ((prefix + c) == secretPassword)
                {
                    Console.WriteLine($"GOT IT: {prefix + c}");
                    Console.ReadLine();
                }
                if (level < secretPassword.Length)
                {
                    Dive(allowed, prefix + c, level);
                }
            }
        }
    }
}
