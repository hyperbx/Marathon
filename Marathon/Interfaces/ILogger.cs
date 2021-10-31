using System.Runtime.CompilerServices;

namespace Marathon.Interfaces
{
    public enum LogLevel
    {
        /// <summary>
        /// Unimportant logging - usually reserved for debugging.
        /// <para>Colour: White</para>
        /// </summary>
        None,

        /// <summary>
        /// General logging - used for user output.
        /// <para>Colour: Green</para>
        /// </summary>
        Utility,

        /// <summary>
        /// Warnings - reserved for minor issues that aren't critical.
        /// <para>Colour: Yellow</para>
        /// </summary>
        Warning,

        /// <summary>
        /// Errors - reserved for fatal issues.
        /// <para>Colour: Red</para>
        /// </summary>
        Error
    }

    public interface ILogger
    {
        void Log(string message, LogLevel level, [CallerMemberName] string caller = null);
    }
}
