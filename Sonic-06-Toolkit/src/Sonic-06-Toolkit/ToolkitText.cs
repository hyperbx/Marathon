using System.IO;
using System.Web;
using Toolkit.EnvironmentX;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Toolkit.Text
{
    class SystemMessages
    {
        public static string tl_DefaultTitle = $"Sonic '06 Toolkit";
        public static string tl_DefaultTitleVersion = $"Sonic '06 Toolkit ({Main.versionNumber})";
        public static string tl_AreYouSure = "Are you sure?";
        public static string tl_FatalError = "Fatal Error";
        public static string tl_MetadataError = "Metadata Error";
        public static string tl_RepackAs = "Repack Archive As...";
        public static string tl_SelectArchive = "Please select an archive...";
        public static string tl_CreateARC = "Create an archive...";
        public static string ex_NewWindowError = "An error occurred whilst launching a new instance of Sonic '06 Toolkit.";
        public static string msg_CloseTab = "Are you sure you want to close this tab? All unsaved changes will be lost if you haven't repacked.";
        public static string msg_CloseAllTabs = "Are you sure you want to close all tabs? All unsaved changes will be lost if you haven't repacked.";
        public static string ex_InvalidARC = "The selected archive is invalid...";
        public static string ex_OpenFolderError = "An error occurred whilst opening the current directory.";
        public static string ex_ClipboardFolderError = "An error occurred whilst copying the current directory to the clipboard.";
        public static string ex_MetadataWriteError = "An error occurred whilst rewriting the metadata - the current tab will now close.";
        public static string ex_MetadataMissing = "This archive's metadata is missing or unreadable. Please specify a new location for the selected ARC.";
        public static string msg_UserClosing = "Are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.";
        public static string msg_WindowsShutDown = "Sonic '06 Toolkit is still running - are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.";
        public static string msg_SelectGameDirectory = "Please select your game directory...";
        public static string ex_SETExportError = "An error occurred when exporting the SET files.";
        public static string ex_XMLImportError = "An error occurred when importing the XML files.";
        public static string ex_XMLDeleteError = "Failed to delete the original XML file.";
        public static string msg_NoSETsInDir = "There are no SET files to export in this directory.";
        public static string msg_NoXMLsInDir = "There are no XML files to import in this directory.";
        public static string tl_Exploring(string path) { return $"{tl_DefaultTitleVersion} - Exploring '{path}'"; }
        public static string tl_NoFilesAvailable(string fileType) { return $"No {fileType} files available"; }
    }

    class StatusMessages
    {
        public static string msg_DefaultStatus = "Ready.";
        public static string msg_Clipboard = "Copied to Clipboard!";
        public static string arc_Unpacking(string file) { return $"Unpacking '{file}...'"; }
        public static string arc_Unpacked(string file) { return $"Unpacked '{file}' successfully..."; }
        public static string arc_UnpackFailed(string file) { return $"Failed to unpack '{file}...'"; }
        public static string arc_Repacking(string file) { return $"Repacking '{file}...'"; }
        public static string arc_Repacked(string file) { return $"Repacked '{file}' successfully..."; }
        public static string arc_RepackFailed(string file) { return $"Failed to repack '{file}...'"; }
        public static string ex_InvalidFile(string file, string fileType) { return $"'{file}' is not a valid {fileType} file..."; }
    }

    class Filters
    {
        public static string Archives = "Sonic '06 Archives (*.arc)|*.arc";
    }

    class Paths
    {
        public static string Archives = @"Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
        public static string Tools = @"Hyper_Development_Team\Sonic '06 Toolkit\Tools\";

        public static string Unpack = Path.Combine(Tools, "unpack.exe");
        public static string Repack = Path.Combine(Tools, "repack.exe");

        public static string currentPath = string.Empty;
    }
}
