using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Marathon.Interfaces;

namespace Marathon
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, LogLevel level, string caller)
        {
            var oldColour = Console.ForegroundColor;

            switch (level)
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

            Console.WriteLine(string.IsNullOrEmpty(caller) ? message : $"[{caller} {message}]");

            Console.ForegroundColor = oldColour;
        }
    }

    public static class Logger
    {
        private static List<ILogger> _handlers = new() { new ConsoleLogger() };
        
        public static void Add(ILogger logger)
            => _handlers.Add(logger);

        public static bool Remove(ILogger logger)
            => _handlers.Remove(logger);

        public static void Log(string message, LogLevel level = LogLevel.Utility, [CallerMemberName] string caller = null)
        {
            foreach (var logger in _handlers)
            {
                logger.Log(message, level, caller);
            }
        }
    }
}
