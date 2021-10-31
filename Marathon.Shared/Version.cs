using System.Reflection;

namespace Marathon.Shared
{
    public class Version
    {
        /// <summary>
        /// Retrieves the assembly informational version from the entry assembly. 
        /// </summary>
        public static string GetInformationalVersion()
            => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
