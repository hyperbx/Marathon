using System;
using System.Text;

namespace Marathon.Shared
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Builds a text log of the exception.
        /// </summary>
        /// <param name="markdown">Enables markdown for a better preview with services that use it.</param>
        public static string CreateLog(this Exception ex, bool markdown = false)
        {
            StringBuilder exception = new();

            if (markdown)
                exception.AppendLine("```");

            exception.AppendLine("Marathon " + $"({AssemblyExtensions.GetInformationalVersion()})");

            if (!string.IsNullOrEmpty(ex.GetType().Name))
                exception.AppendLine($"\nType: {ex.GetType().Name}");

            if (!string.IsNullOrEmpty(ex.Message))
                exception.AppendLine($"Message: {ex.Message}");

            if (!string.IsNullOrEmpty(ex.Source))
                exception.AppendLine($"Source: {ex.Source}");

            if (ex.TargetSite != null)
                exception.AppendLine($"Function: {ex.TargetSite}");

            if (!string.IsNullOrEmpty(ex.StackTrace))
                exception.AppendLine($"\nStack Trace: \n{ex.StackTrace}");

            if (ex.InnerException != null)
                exception.AppendLine($"\nInner Exception: \n{ex.InnerException}");

            if (markdown)
                exception.AppendLine("```");

            return exception.ToString();
        }
    }
}
