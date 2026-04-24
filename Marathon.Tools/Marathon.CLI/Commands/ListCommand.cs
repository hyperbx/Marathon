using Marathon.Formats.Archive;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Marathon.CLI.Commands
{
    [Description("Lists the contents of an archive.")]
    public class ListCommand : Command<ListSettings>
    {
        protected override int Execute(CommandContext in_context, ListSettings in_settings, CancellationToken in_cancellationToken)
        {
            if (!File.Exists(in_settings.Source))
            {
                AnsiConsole.MarkupLine($"[red]File not found:[/] {in_settings.Source}");
                return -1;
            }

            using var arc = new ArcFile(in_settings.Source);

            var table = new Table().MinimalBorder();
            table.AddColumns("Path", "Raw Size", "Uncompressed Size");

            foreach (var file in arc.EnumerateFiles(in_searchOption: SearchOption.AllDirectories))
            {
                if (in_cancellationToken.IsCancellationRequested)
                    break;

                table.AddRow(file.Path, $"{file.Length:N0} bytes", $"{file.UncompressedLength:N0} bytes");
            }
            
            AnsiConsole.Write(table);

            return 0;
        }
    }

    public class ListSettings : CommandSettings
    {
        [CommandArgument(0, "<source>")]
        [Description("The path to the input file.")]
        public required string Source { get; init; }
    }
}
