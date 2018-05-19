using System;
using System.Threading.Tasks;
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
            Task characters = TryCharacters();
            Console.ReadLine();

            Task numbers = TryNumbers();
            Console.ReadLine();
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
            return Task.Run(() => { DiveChar("", 0); });
        }

        private static void DiveChar(string prefix, int level)
        {
            level += 1;
            foreach (char c in LowerCaseLetters)
            {
                Console.WriteLine(prefix + c);
                if ((prefix + c) == secretPassword)
                {
                    Console.WriteLine($"GOT IT: {prefix + c}");
                    Console.ReadLine();
                }
                if (level < secretPassword.Length)
                {
                    DiveChar(prefix + c, level);
                }
            }
        }

        private static Task TryNumbers()
        {
            return Task.Run(() => { Dive("", 0); });
        }

        private static void Dive(string prefix, int level)
        {
            level += 1;
            foreach (char c in Numbers)
            {
                Console.WriteLine(prefix + c);
                if ((prefix + c) == secretPassword)
                {
                    Console.WriteLine($"GOT IT: {prefix + c}");
                    Console.ReadLine();
                }
                if (level < secretPassword.Length)
                {
                    Dive(prefix + c, level);
                }
            }
        }
    }
}
