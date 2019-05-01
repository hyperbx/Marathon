using System;

namespace Sonic_06_Toolkit
{
    public class Global
    {
        #region Strings
        public static string versionNumber = "1.79";
        public static string latestVersion = "Version " + versionNumber;
        public static string unpackState = "typical";
        public static string repackState = "save";
        public static string convertState = "XNO";
        public static string updateState = "launch";
        public static string serverStatus = "online";
        public static string currentPath;

        #region Directories
        public static string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        #endregion

        #endregion

        #region Integers
        public static int pathChange = 0;
        public static int sessionID;
        public static int getIndex;

        #region Boolean
        public static bool javaCheck = true;
        #endregion

        #endregion
    }
}
