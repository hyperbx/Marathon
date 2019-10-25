using System.IO;
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
        public static string tl_XISO = "Please select an Xbox 360 ISO...";
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
        public static string ex_ARCUnpackError = "An error occurred when unpacking the archive.";
        public static string ex_ARCRepackError = "An error occurred when repacking the archive.";
        public static string ex_ARCUnpackExceptionUnknown = "An unknown exception when unpacking the archive.";
        public static string ex_ARCRepackExceptionUnknown = "An unknown exception when repacking the archive.";
        public static string ex_SETExportError = "An error occurred when exporting the SET files.";
        public static string ex_XMLImportError = "An error occurred when importing the XML files.";
        public static string ex_LUBCompileError = "An error occurred when decompiling the LUB files.";
        public static string ex_LUBDecompileError = "An error occurred when decompiling the LUB files.";
        public static string ex_LUBDecompileExceptionUnknown = "An unknown exception occurred when decompiling the LUB files.";
        public static string ex_XMLDeleteError = "Failed to delete the original XML file.";
        public static string ex_XEXModificationError = "An error occurred when modifying the XEX files.";
        public static string msg_NoSETsInDir = "There are no SET files to export in this directory.";
        public static string msg_NoXMLsInDir = "There are no XML files to import in this directory.";
        public static string msg_NoLUBsInDir = "There are no LUB files to decompile in this directory.";
        public static string msg_NoLUAsInDir = "There are no LUB files to compile in this directory.";
        public static string msg_NoXEXsInDir = "There are no XEX files to modify in this directory.";
        public static string msg_NoEncodableFiles = "There are no encodable files in this directory.";
        public static string msg_NoConvertableFiles = "There are no convertable files in this directory.";
        public static string msg_NoCompilableFiles = "There are no compilable files in this directory.";
        public static string msg_NoEditableFiles = "There are no editable files in this directory.";
        public static string msg_NoRepackableCSBs = "There are no repackable CSBs in this directory.";
        public static string ex_EncoderError = "An error occurred whilst encoding your audio file.";
        public static string ex_PreviewFailure = "Failed to preview the selected sound byte...";

        public static string tl_Exploring(string path) { return $"{tl_DefaultTitleVersion} - Exploring '{path}'"; }
        public static string tl_NoFilesAvailable(string fileType) {
            if (fileType != string.Empty) return $"No {fileType} files available";
            else return $"No files available";
        }
    }

    class StatusMessages
    {
        public static string msg_DefaultStatus = "Ready.";
        public static string msg_Clipboard = "Copied to Clipboard!";
        public static string ex_OutOfSpace = "Not enough space on the target drive...";
        public static string ex_UnknownISOExtractError = "An unknown error occurred whilst extracting the ISO...";

        public static string cmn_Unpacking(string file, bool fullPath) { 
            if (fullPath) return $"Unpacking '{file}...'";
            else return $"Unpacking '{Path.GetFileName(file)}...'"; 
        }

        public static string cmn_Converting(string file, string newFile, bool fullPath) {
            if (fullPath) return $"Converting '{file}' to {newFile}...";
            else return $"Converting '{Path.GetFileName(file)}' to {newFile}...";
        }

        public static string cmn_ConvertFailed(string file, string newFile, bool fullPath) {
            if (fullPath) return $"Failed to convert '{file}' to {newFile}...";
            else return $"Failed to convert '{Path.GetFileName(file)}' to {newFile}...";
        }

        public static string cmn_Unpacked(string file, bool fullPath) {
            if (fullPath) return $"Unpacked '{file}' successfully..."; 
            else return $"Unpacked '{Path.GetFileName(file)}' successfully...";
        }

        public static string cmn_UnpackFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to unpack '{file}...'";
            else return $"Failed to unpack '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Repacking(string file, bool fullPath) {
            if (fullPath) return $"Repacking '{file}...'";
            else return $"Repacking '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Repacked(string file, bool fullPath) {
            if (fullPath) return $"Repacked '{file}' successfully...";
            else return $"Repacked '{Path.GetFileName(file)}' successfully...";
        }

        public static string cmn_RepackFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to repack '{file}...'";
            else return $"Failed to repack '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Processing(string file, bool fullPath) {
            if (fullPath) return $"Processing '{file}...'";
            else return $"Processing '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Decrypting(string file, bool fullPath) {
            if (fullPath) return $"Decrypting '{file}...'";
            else return $"Decrypting '{Path.GetFileName(file)}...'";
        }

        public static string iso_Extracting(string file, bool fullPath) {
            if (fullPath) return $"Extracting '{file}...'";
            else return $"Extracting '{Path.GetFileName(file)}...'";
        }

        public static string iso_Extracted(string file, bool fullPath) {
            if (fullPath) return $"Extracted '{file}' successfully...";
            else return $"Extracted '{Path.GetFileName(file)}' successfully...";
        }

        public static string iso_FileTooSmall(string file, bool fullPath) {
            if (fullPath) return $"'{file}' is too small for the type expected...";
            else return $"'{Path.GetFileName(file)}' is too small for the type expected...";
        }

        public static string iso_Invalid(string file, bool fullPath) {
            if (fullPath) return $"'{file}' is invalid or unsupported...";
            else return $"'{Path.GetFileName(file)}' is invalid or unsupported...";
        }

        public static string iso_ParseFSError(string file, bool fullPath) {
            if (fullPath) return $"Failed to parse the root filesystem in '{file}' - the ISO is too small...";
            else return $"Failed to parse the root filesystem in '{Path.GetFileName(file)}' - the ISO is too small...";
        }

        public static string iso_InvalidTOC(string file, bool fullPath) {
            if (fullPath) return $"Failed to parse the root filesystem in '{file}' - invalid TOC entry detected...";
            else return $"Failed to parse the root filesystem in '{Path.GetFileName(file)}' - invalid TOC entry detected...";
        }

        public static string iso_RootSectorOffsetFailure(string file, bool fullPath) {
            if (fullPath) return $"Failed to fetch root sector offset in '{file}' - the ISO is too small...";
            else return $"Failed to fetch root sector offset in '{Path.GetFileName(file)}' - the ISO is too small...";
        }

        public static string ex_InvalidFile(string file, bool fullPath, string fileType) {
            if (fullPath) return $"'{file}' is not a valid {fileType} file...";
            else return $"'{Path.GetFileName(file)}' is not a valid {fileType} file...";
        }

        public static string lua_Compiling(string file, bool fullPath) {
            if (fullPath) return $"Compiling '{file}...'";
            else return $"Compiling '{Path.GetFileName(file)}...'";
        }

        public static string lua_Decompiling(string file, bool fullPath) {
            if (fullPath) return $"Decompiling '{file}...'";
            else return $"Decompiling '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Exporting(string file, bool fullPath) {
            if (fullPath) return $"Exporting '{file}...'";
            else return $"Exporting '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Importing(string file, bool fullPath) {
            if (fullPath) return $"Importing '{file}...'";
            else return $"Importing '{Path.GetFileName(file)}...'";
        }

        public static string xma_EncodeFooterError(string file, bool fullPath) {
            if (fullPath) return $"Failed to encode the footer in '{file}...'";
            else return $"Failed to encode the footer in '{Path.GetFileName(file)}...'";
        }

        public static string xma_DecodeFooterError(string file, bool fullPath) {
            if (fullPath) return $"Failed to decode the footer in '{file}...'";
            else return $"Failed to decode the footer in '{Path.GetFileName(file)}...'";
        }
    }

    class Filters
    {
        public static string All = "All files (*.*)|*.*";
        public static string Archives = "Sonic '06 Archives (*.arc)|*.arc";
    }

    class Paths
    {
        public static string Archives = @"Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
        public static string Tools = @"Hyper_Development_Team\Sonic '06 Toolkit\Tools\";

        public static string Unpack = Path.Combine(Program.applicationData, Tools, "unpack.exe");
        public static string Repack = Path.Combine(Program.applicationData, Tools, "repack.exe");
        public static string LuaCompiler = Path.Combine(Program.applicationData, Tools, "Lua", "luac50.exe");
        public static string LuaDecompiler = Path.Combine(Program.applicationData, Tools, "Lua", "unlub.jar");
        public static string XexTool = Path.Combine(Program.applicationData, Tools, "XexTool", "xextool.exe");
        public static string AT3Tool = Path.Combine(Program.applicationData, Tools, "SonicAudioTools", "PS3_at3tool.exe");
        public static string XMAEncoder = Path.Combine(Program.applicationData, Tools, "SonicAudioTools", "xmaencode2008.exe");
        public static string XMADecoder = Path.Combine(Program.applicationData, Tools, "SonicAudioTools", "towav.exe");

        public static string currentPath = string.Empty;
    }
}
