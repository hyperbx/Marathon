using System;

namespace Sonic_06_Toolkit
{
    public class Global
    {
        public static string versionNumber = "1.78";
        public static int sessionID;
        public static string currentPath;
        public static int getIndex;
        public static string repackState = "save";
        public static string unpackState = "typical";
        public static string convertState = "XNO";

        #region Directories
        public static string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string rootPath = Properties.Settings.Default.rootPath;
        public static string toolsPath = Properties.Settings.Default.toolsPath;
        public static string archivesPath = Properties.Settings.Default.archivesPath;
        public static string unlubPath = toolsPath + @"unlub\";
        public static string xnoPath = toolsPath + @"xno2dae\";
        #endregion

        #region Files
        public static string unpackFile = toolsPath + "unpack.exe";
        public static string repackFile = toolsPath + "repack.exe";
        public static string xnoFile = xnoPath + "xno2dae.exe";
        #endregion
    }
}
