using System;

namespace Marathon.Helpers
{
    public class ConsoleHelper
    {
        public static void ReturnToPreviousLine(int in_count = 1)
        {
            for (int i = 0; i < in_count; i++)
            {
                if (Console.CursorTop != 0)
                    Console.SetCursorPosition(0, Console.CursorTop - 1);

                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }

        public static ConsoleKeyInfo PressAnyKey()
        {
            Console.Write("\nPress any key to continue...");
            var result = Console.ReadKey();
            Console.WriteLine();

            ReturnToPreviousLine(2);

            return result;
        }
    }
}
