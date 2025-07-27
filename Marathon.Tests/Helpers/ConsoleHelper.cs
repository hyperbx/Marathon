namespace Marathon.Tests.Helpers
{
    internal class ConsoleHelper
    {
        public static void ReturnToPreviousLine(int in_count = 1)
        {
            for (int i = 0; i < in_count; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop);
            }
        }
    }
}
