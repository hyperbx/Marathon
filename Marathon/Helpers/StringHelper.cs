using System.Text.RegularExpressions;

namespace Marathon.Helpers
{
	public class StringHelper
	{
		/// <summary>
		/// Returns the full extension from the file name (with a dot prefix).
		/// </summary>
		public static string GetFullExtension(string fileName)
			=> Regex.Match(Path.GetFileName(fileName), @"\..*").Value;

		/// <summary>
		/// Returns the file name completely extension-less.
		/// </summary>
		public static string RemoveFullExtension(string fileName)
			=> Regex.Replace(Path.GetFileName(fileName), @"\..*", string.Empty);

		/// <summary>
		/// Appends text to the file name before the extension.
		/// </summary>
		/// <param name="filePath">Full file path.</param>
		/// <param name="text">Text to append.</param>
		public static string AppendToFileName(string filePath, string text)
			=> Path.GetFileNameWithoutExtension(filePath) + text + GetFullExtension(filePath);

		/// <summary>
		/// Returns a new path with the specified filename.
		/// </summary>
		public static string ReplaceFilename(string path, string newFile)
			=> Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(newFile));

		/// <summary>
		/// Parses all line breaks from a string and returns an array of lines.
		/// </summary>
		/// <param name="input">String to parse line breaks from.</param>
		public static string[] ParseLineBreaks(string input)
			=> input.Split(new[] { '\r', '\n' });
	}
}
