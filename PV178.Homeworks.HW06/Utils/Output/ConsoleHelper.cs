using System;

namespace PV178.Homeworks.HW06.Utils.Output
{
    /// <summary>
    /// Helper class for console communication
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Writes: "Type in command:"
        /// </summary>
        public static void PrintTypeInCommand()
        {
            Console.WriteLine(Environment.NewLine + "Type in command:");
        }

        /// <summary>
        /// Erases previously written "Type in command"
        /// </summary>
        public static void EraseTypeInText()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine("                ");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }
    }
}
