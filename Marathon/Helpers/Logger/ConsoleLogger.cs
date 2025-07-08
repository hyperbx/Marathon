using System;

namespace Marathon.Helpers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string in_message, LogLevel in_logLevel, string in_caller)
        {
            var oldColour = Console.ForegroundColor;

            switch (in_logLevel)
            {
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogLevel.Utility:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.WriteLine(string.IsNullOrEmpty(in_caller) ? in_message : $"[{in_caller}] {in_message}");

            Console.ForegroundColor = oldColour;
        }
    }
}
