using Spectre.Console;

namespace Marathon.CLI
{
    public class Common
    {
        public static bool FileExists(string in_path)
        {
            if (!File.Exists(in_path))
            {
                AnsiConsole.MarkupLine($"[red]File not found:[/] {in_path}");
                return false;
            }

            return true;
        }

        public static bool DirectoryExists(string in_path)
        {
            if (!Directory.Exists(in_path))
            {
                AnsiConsole.MarkupLine($"[red]Directory not found:[/] {in_path}");
                return false;
            }

            return true;
        }

        public static string GetIntegerPrefix(int in_num, int in_max = 0)
        {
            return $"[gray]{in_num.ToString().PadLeft(in_max.ToString().Length)}.[/]";
        }
    }
}
