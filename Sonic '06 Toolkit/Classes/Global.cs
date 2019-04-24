using System;

namespace Sonic_06_Toolkit
{
    public class Global
    {
        public static string versionNumber = "1.78-beta";
        public static int sessionID;
        public static string currentPath;
        public static int getIndex;
        public static string repackState = "save";

        #region Directories
        public static string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string tempPath = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
        public static string archivesPath = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
        public static string toolsPath = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
        public static string unlubPath = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\unlub\";
        public static string xnoPath = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\xno2dae\";
        #endregion

        #region Files
        public static string unpackFile = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\unpack.exe";
        public static string repackFile = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\repack.exe";
        public static string xnoFile = applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\xno2dae\xno2dae.exe";
        #endregion
    }
}
