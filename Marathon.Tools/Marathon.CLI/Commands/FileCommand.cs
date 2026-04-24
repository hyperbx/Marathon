using Amicitia.IO.Binary;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Marathon.CLI.Commands
{
    [Description("Exports a file to or from an intermediate format.")]
    public class FileCommand : Command<FileSettings>
    {
        protected override int Execute(CommandContext in_context, FileSettings in_settings, CancellationToken in_cancellationToken)
        {
            return 0;
        }
    }

    public class FileSettings : CommandSettings
    {
        [CommandArgument(0, "<source>")]
        [Description("The path to the input file.")]
        public required string Source { get; init; }

        [CommandArgument(1, "[destination]")]
        [Description("The path to the output file.")]
        public string? Destination { get; init; }

        [CommandOption("-o|--overwrite")]
        [Description("Overwrites existing files without prompting.")]
        public bool Overwrite { get; init; }

        [CommandOption("-e|--endianness")]
        [Description("The endianness of the output file.")]
        [DefaultValue(Endianness.Big)]
        public Endianness Endianness { get; init; } = Endianness.Big;
    }
}
