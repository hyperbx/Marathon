﻿using System.Reflection;

namespace Marathon.Shared
{
    public class AssemblyExtensions
    {
        /// <summary>
        /// Returns the assembly informational version from the entry assembly. 
        /// </summary>
        public static string GetInformationalVersion()
            => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        /// <summary>
        /// Returns the current assembly name.
        /// </summary>
        public static string GetAssemblyName()
            => Assembly.GetEntryAssembly().GetName().Name;

        /// <summary>
        /// Returns the current assembly version.
        /// </summary>
        public static string GetAssemblyVersion()
            => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
