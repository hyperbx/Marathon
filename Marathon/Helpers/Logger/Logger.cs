using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Marathon.Helpers
{
    public static class Logger
    {
        private static List<ILogger> _loggers = [ new ConsoleLogger() ];

        public static void AddLogger(ILogger in_logger)
        {
            _loggers.Add(in_logger);
        }

        public static bool RemoveLogger(ILogger in_logger)
        {
            return _loggers.Remove(in_logger);
        }

        public static int RemoveAllLoggers(Predicate<ILogger> in_match)
        {
            return _loggers.RemoveAll(in_match);
        }

        public static void ClearLoggers()
        {
            _loggers.Clear();
        }

        public static void Log(string in_message, LogLevel in_logLevel, [CallerMemberName] string in_caller = null)
        {
            foreach (var logger in _loggers)
                logger.Log(in_message, in_logLevel, in_caller);
        }

        public static void Log(string in_message, [CallerMemberName] string in_caller = null)
        {
            Log(in_message, LogLevel.None, in_caller);
        }

        public static void Log(string in_message)
        {
            Log(in_message, string.Empty);
        }

        public static void Utility(string in_message, [CallerMemberName] string in_caller = null)
        {
            Log(in_message, LogLevel.Utility, in_caller);
        }

        public static void Utility(string in_message)
        {
            Utility(in_message, string.Empty);
        }

        public static void Warning(string in_message, [CallerMemberName] string in_caller = null)
        {
            Log(in_message, LogLevel.Warning, in_caller);
        }

        public static void Warning(string in_message)
        {
            Warning(in_message, string.Empty);
        }

        public static void Error(string in_message, [CallerMemberName] string in_caller = null)
        {
            Log(in_message, LogLevel.Error, in_caller);
        }

        public static void Error(string in_message)
        {
            Error(in_message, string.Empty);
        }
    }
}
