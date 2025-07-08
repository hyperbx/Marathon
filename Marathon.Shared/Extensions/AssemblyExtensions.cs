using System.Reflection;

namespace Marathon.Shared
{
    public class AssemblyExtensions
    {
        /// <summary>
        /// Gets the assembly informational version from the entry assembly. 
        /// </summary>
        public static string GetInformationalVersion()
        {
            return Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }

        /// <summary>
        /// Gets the current assembly name.
        /// </summary>
        public static string GetAssemblyName()
        {
            return Assembly.GetEntryAssembly().GetName().Name;
        }

        /// <summary>
        /// Gets the current assembly version.
        /// </summary>
        public static string GetAssemblyVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
