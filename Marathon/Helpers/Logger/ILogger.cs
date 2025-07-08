using System.Runtime.CompilerServices;

namespace Marathon.Helpers
{
    public interface ILogger
    {
        void Log(string in_message, LogLevel in_logLevel, [CallerMemberName] string in_caller = null);
    }
}
