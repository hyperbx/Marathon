using System;
using System.Text;

namespace Marathon.Shared
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Builds a text log of the exception.
        /// </summary>
        /// <param name="in_useMarkdown">Determines whether to use markdown for a better preview with services that use it.</param>
        public static string CreateLog(this Exception in_ex, bool in_useMarkdown = false)
        {
            StringBuilder exception = new();

            if (in_useMarkdown)
                exception.AppendLine("```");

            exception.AppendLine("Marathon " + $"({AssemblyExtensions.GetInformationalVersion()})");

            if (!string.IsNullOrEmpty(in_ex.GetType().Name))
                exception.AppendLine($"\nType: {in_ex.GetType().Name}");

            if (!string.IsNullOrEmpty(in_ex.Message))
                exception.AppendLine($"Message: {in_ex.Message}");

            if (!string.IsNullOrEmpty(in_ex.Source))
                exception.AppendLine($"Source: {in_ex.Source}");

            if (in_ex.TargetSite != null)
                exception.AppendLine($"Function: {in_ex.TargetSite}");

            if (!string.IsNullOrEmpty(in_ex.StackTrace))
                exception.AppendLine($"\nStack Trace: \n{in_ex.StackTrace}");

            if (in_ex.InnerException != null)
                exception.AppendLine($"\nInner Exception: \n{in_ex.InnerException}");

            if (in_useMarkdown)
                exception.AppendLine("```");

            return exception.ToString();
        }
    }
}
