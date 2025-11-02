using System.Reflection;

namespace Marathon.Shared
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the assembly informational version from the entry assembly. 
        /// </summary>
        public static string GetInformationalVersion(this Assembly in_assembly)
        {
            var attribute = in_assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            if (attribute == null)
                return "0.0.0";

            return attribute.InformationalVersion.Split('+')[0];
        }

        /// <summary>
        /// Gets the current assembly name.
        /// </summary>
        public static string GetAssemblyName(this Assembly in_assembly)
        {
            var name = in_assembly.GetName();

            if (name == null || string.IsNullOrEmpty(name.Name))
                return "(null)";

            return name.Name;
        }

        /// <summary>
        /// Gets the current assembly version.
        /// </summary>
        public static string GetAssemblyVersion(this Assembly in_assembly)
        {
            var name = in_assembly.GetName();

            if (name == null || name.Version == null)
                return "(null)";

            return name.Version.ToString();
        }
    }
}
