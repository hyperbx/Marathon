using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Marathon.Interfaces;

namespace Marathon
{
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Logs information to the console output.
        /// </summary>
        /// <param name="message">Message body.</param>
        /// <param name="level">Level of importance for logging.</param>
        /// <param name="caller">Function that called the log.</param>
        public void Log(string message, LogLevel level, string caller)
        {
            // Store original console colour.
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

            // Restore original console colour.
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

        /// <summary>
        /// Logs information to the console output.
        /// </summary>
        /// <param name="message">Message body.</param>
        /// <param name="level">Level of importance for logging.</param>
        /// <param name="caller">Function that called the log.</param>
        public static void Log(string message, LogLevel level = LogLevel.Utility, [CallerMemberName] string caller = null)
        {
            foreach (var logger in _handlers)
                logger.Log(message, level, caller);
        }
    }
}
