using System.Runtime.CompilerServices;

namespace Marathon.Interfaces
{
    public enum LogLevel
    {
        None,
        Warning,
        Error,
        Utility,
    }

    public interface ILogger
    {
        void Log(string message, LogLevel level, [CallerMemberName] string caller = null);
    }
}
