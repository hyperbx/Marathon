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
        public static string tl_CollisionTagsDetected = "Collision Tags Detected";
        public static string tl_Success = "Success";
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
        public static string ex_UpdateError = "An error occurred when updating Sonic '06 Toolkit.";
        public static string ex_ARCUnpackError = "An error occurred when unpacking the archive.";
        public static string ex_ARCRepackError = "An error occurred when repacking the archive.";
        public static string ex_ARCUnpackExceptionUnknown = "An unknown exception when unpacking the archive.";
        public static string ex_ARCRepackExceptionUnknown = "An unknown exception when repacking the archive.";
        public static string ex_SETExportError = "An error occurred when exporting the SET files.";
        public static string ex_MSTExportError = "An error occurred when exporting the MST files.";
        public static string ex_XMLImportError = "An error occurred when importing the XML files.";
        public static string ex_BINExportError = "An error occurred when exporting the BIN files.";
        public static string ex_OBJImportError = "An error occurred when importing the OBJ files.";
        public static string ex_XNOConvertError = "An error occurred when converting the XNO files.";
        public static string ex_XNMConvertError = "An error occurred when converting the animation.";
        public static string ex_LUBCompileError = "An error occurred when decompiling the LUB files.";
        public static string ex_LUBDecompileError = "An error occurred when decompiling the LUB files.";
        public static string ex_LUBDecompileExceptionUnknown = "An unknown exception occurred when decompiling the LUB files.";
        public static string ex_XMLDeleteError = "Failed to delete the original XML file.";
        public static string ex_XEXModificationError = "An error occurred when modifying the XEX files.";
        public static string msg_NoSETsInDir = "There are no SET files to export in this directory.";
        public static string msg_NoXNOsInDir = "There are no XNO files to convert in this directory.";
        public static string msg_NoXNMsInDir = "There are no XNM files to convert in this directory.";
        public static string msg_NoXMLsInDir = "There are no XML files to import in this directory.";
        public static string msg_NoLUBsInDir = "There are no LUB files to decompile in this directory.";
        public static string msg_NoLUAsInDir = "There are no LUB files to compile in this directory.";
        public static string msg_NoXEXsInDir = "There are no XEX files to modify in this directory.";
        public static string msg_NoEncodableFiles = "There are no encodable files in this directory.";
        public static string msg_NoConvertableFiles = "There are no convertable files in this directory.";
        public static string msg_NoCompilableFiles = "There are no compilable files in this directory.";
        public static string msg_NoDecompilableFiles = "There are no decompilable files in this directory.";
        public static string msg_NoEditableFiles = "There are no editable files in this directory.";
        public static string msg_NoRepackableCSBs = "There are no repackable CSBs in this directory.";
        public static string ex_EncoderError = "An error occurred whilst encoding your audio file.";
        public static string ex_PreviewFailure = "Failed to preview the selected sound byte...";
        public static string ex_DDSConvertError = "An error occurred when converting the DDS files.";
        public static string ex_MergeError = "An error occurred when merging the archives.";
        public static string ex_InvalidFiles = "The selected files were invalid...";
        public static string ex_IntegrityCheckFailed = "Important files are missing to be able to run components for Sonic '06 Toolkit. Please restart the application and try again...";
        public static string msg_RestartAsAdmin = "Sonic '06 Toolkit is not running in administrator mode. Do you want to restart Sonic '06 Toolkit as an administrator to make your changes?";
        public static string ex_RegistryError = "An error occurred whilst editing the registry.";
        public static string msg_UpdateComplete = $"Update complete! Restarting {tl_DefaultTitle}...";
        public static string msg_UnpackForModManager = "This will unpack and erase all archives in this directory. Are you sure you want to continue?";

        public static string tl_Exploring(string path) { return $"{tl_DefaultTitleVersion} - Exploring '{path}'"; }

        public static string tl_NoFilesAvailable(string fileType) {
            if (fileType != string.Empty) return $"No {fileType} files available";
            else return "No files available";
        }

        public static string msg_CollisionTagsDetected(int count, string fileName) {
            if (count > 1) return $"Found {count} tags in '{fileName}...' Do you want to convert them to collision tags?";
            else return $"Found 1 tag in '{fileName}...' Do you want to convert it to a collision tag?";
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

        public static string cmn_RepackingAs(string file1, string file2, bool fullPath) {
            if (fullPath) return $"Repacking '{file1}' as '{file2}...'";
            else return $"Repacking '{Path.GetFileName(file1)}' as '{Path.GetFileName(file2)}...'";
        }

        public static string cmn_Repacked(string file, bool fullPath) {
            if (fullPath) return $"Repacked '{file}' successfully...";
            else return $"Repacked '{Path.GetFileName(file)}' successfully...";
        }

        public static string cmn_RepackedAs(string file1, string file2, bool fullPath) {
            if (fullPath) return $"Repacked '{file1}' as '{file2}' successfully...";
            else return $"Repacked '{Path.GetFileName(file1)}' as '{Path.GetFileName(file2)}' successfully...";
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

        public static string ex_InvalidFile(string file, string fileType, bool fullPath) {
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

        public static string lua_DecompileFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to decompile '{file}...'";
            else return $"Failed to decompile '{Path.GetFileName(file)}...'";
        }

        public static string xex_Decrypting(string file, bool fullPath) {
            if (fullPath) return $"Decrypting '{file}...'";
            else return $"Decrypting '{Path.GetFileName(file)}...'";
        }

        public static string xex_DecryptFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to decrypt '{file}...'";
            else return $"Failed to decrypt '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Exporting(string file, bool fullPath) {
            if (fullPath) return $"Exporting '{file}...'";
            else return $"Exporting '{Path.GetFileName(file)}...'";
        }

        public static string cmn_ExportFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to export '{file}...'";
            else return $"Failed to export '{Path.GetFileName(file)}...'";
        }

        public static string cmn_Importing(string file, bool fullPath) {
            if (fullPath) return $"Importing '{file}...'";
            else return $"Importing '{Path.GetFileName(file)}...'";
        }

        public static string xno_Culling(string file, bool fullPath) {
            if (fullPath) return $"Culling '{file}...'";
            else return $"Culling '{Path.GetFileName(file)}...'";
        }

        public static string xno_NothingToCull(string file, bool fullPath) {
            if (fullPath) return $"No materials to cull in '{file}...'";
            else return $"No materials to cull in '{Path.GetFileName(file)}...'";
        }

        public static string xno_CullFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to cull '{file}...'";
            else return $"Failed to cull '{Path.GetFileName(file)}...'";
        }

        public static string xno_Deculling(string file, bool fullPath) {
            if (fullPath) return $"Deculling '{file}...'";
            else return $"Deculling '{Path.GetFileName(file)}...'";
        }

        public static string xno_NothingToDecull(string file, bool fullPath) {
            if (fullPath) return $"No materials to decull in '{file}...'";
            else return $"No materials to decull in '{Path.GetFileName(file)}...'";
        }

        public static string xno_DecullFailed(string file, bool fullPath) {
            if (fullPath) return $"Failed to decull '{file}...'";
            else return $"Failed to decull '{Path.GetFileName(file)}...'";
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
        public static string Tools = Path.Combine(Program.applicationData, @"Hyper_Development_Team\Sonic '06 Toolkit\Tools\");
        public static string Root = @"Hyper_Development_Team\Sonic '06 Toolkit\";

        public static string Shell = "cmd.exe";
        public static string Arctool = Path.Combine(Tools, "arctool.exe");
        public static string Unpack = Path.Combine(Tools, "unpack.exe");
        public static string Repack = Path.Combine(Tools, "repack.exe");
        public static string LuaCompiler = Path.Combine(Tools, "luac50.exe");
        public static string LuaDecompiler = Path.Combine(Tools, "unlub.jar");
        public static string XexTool = Path.Combine(Tools, "xextool.exe");
        public static string AT3Tool = Path.Combine(Tools, "PS3_at3tool.exe");
        public static string XMATool = Path.Combine(Tools, "xmaencode2008.exe");
        public static string XMADecoder = Path.Combine(Tools, "towav.exe");
        public static string DDSTool = Path.Combine(Tools, "texconv.exe");
        public static string MSTTool = Path.Combine(Tools, "mst06.exe");
        public static string XNODecoder = Path.Combine(Tools, "xno2dae.exe");
        public static string BINEncoder = Path.Combine(Tools, "s06col.exe");
        public static string BINDecoder = Path.Combine(Tools, "s06collision.exe");

        public static string currentPath = string.Empty;
    }
}
