using Marathon.Formats.Archive;
using Marathon.Helpers;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Marathon.CLI.Commands
{
    [Description("Trees the contents of an archive.")]
    public class TreeCommand : Command<TreeSettings>
    {
        protected override int Execute(CommandContext in_context, TreeSettings in_settings, CancellationToken in_cancellationToken)
        {
            if (!Common.FileExists(in_settings.Source))
                return -1;

            using var arc = new ArcFile(in_settings.Source);

            Console.WriteLine(FileSystemHelper.GetDirectoryTree(arc, Path.GetFileName(in_settings.Source), in_settings.ShowFiles, in_settings.ShowSizes));

            return 0;
        }
    }

    public class TreeSettings : CommandSettings
    {
        [CommandArgument(0, "<source>")]
        [Description("The path to the input file.")]
        public required string Source { get; init; }

        [CommandOption("-f|--show-files")]
        [Description("Show files in the tree.")]
        [DefaultValue(true)]
        public bool ShowFiles { get; init; } = true;

        [CommandOption("-s|--show-sizes")]
        [Description("Show sizes in the tree.")]
        [DefaultValue(true)]
        public bool ShowSizes { get; init; } = true;
    }
}
