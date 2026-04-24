using Marathon.Extensions;
using Marathon.Formats.Archive;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Text;
using Marathon.Helpers;
using Marathon.IO;
using Newtonsoft.Json;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Marathon.CLI.Commands
{
    [Description("Searches the contents of an archive.")]
    public class SearchCommand : Command<SearchSettings>
    {
        protected override int Execute(CommandContext in_context, SearchSettings in_settings, CancellationToken in_cancellationToken)
        {
            if (!Common.FileExists(in_settings.Source))
                return -1;

            var resultsCount = 0;

            using var arc = new ArcFile(in_settings.Source);

            var files = arc.GetFiles(in_settings.SearchPattern, SearchOption.AllDirectories);
            var fileIndex = -1;

            foreach (var file in files)
            {
                if (in_cancellationToken.IsCancellationRequested)
                    break;

                fileIndex++;

                if (in_settings.BinaryPattern == null && in_settings.RegexPattern == null)
                {
                    AnsiConsole.WriteLine(file.Path);

                    if (!string.IsNullOrEmpty(in_settings.Destination))
                        file.Export(Path.Combine(in_settings.Destination, file.Path));

                    if (fileIndex == files.Length - 1)
                        AnsiConsole.WriteLine();

                    resultsCount++;
                }
                else
                {
                    var results = new List<string>();
                    var resultsPrefix = true;
                    var uncompressedFile = file.Decompress();

                    if (in_settings.BinaryPattern != null)
                    {
                        var scanResults = SignatureScanner.ScanAll(uncompressedFile.Open(), in_settings.BinaryPattern);

                        foreach (var scanResult in scanResults)
                        {
                            results.Add($"0x{scanResult:X8}");
                            resultsCount++;
                        }
                    }

                    if (in_settings.RegexPattern != null)
                    {
                        if (in_settings.DecompileLua && Path.GetExtension(file.Name) is ".lub" or ".lua")
                        {
                            // This file currently crashes the decompiler.
                            if (file.Name == "standard.lub")
                                continue;

                            var lub = new LuaBinary(uncompressedFile);
                            lub.LoadSymbols(JsonConvert.DeserializeObject<List<Symbol>>(Properties.Resources.Symbols));

                            var lua = lub.Decompile(new SymbolResolverOptions(file.Name)).SplitLineBreaks();

                            for (int i = 0; i < lua.Length; i++)
                            {
                                var line = lua[i];

                                if (!IsRegexMatch(in_settings, line))
                                    continue;

                                var prefix = string.Empty;

                                if (in_settings.LineNumbers)
                                    prefix = $"{Common.GetIntegerPrefix(i + 1, lua.Length)} ";

                                results.Add($"{prefix}{Markup.Escape(line)}");
                                resultsPrefix = false;
                                resultsCount++;
                            }
                        }
                        else if (Path.GetExtension(file.Name) == ".mst")
                        {
                            var mst = new TextBook(uncompressedFile);

                            foreach (var card in mst.Cards)
                            {
                                var matches = IsRegexMatch(in_settings, card.Name) || IsRegexMatch(in_settings, card.Text);
                                var text = string.Join("[gray]\\n[/]", string.Join("[gray]\\f[/]", card.Pages.Select(x => Markup.Escape(x))).SplitLineBreaks());

                                if (card.Variables != null)
                                {
                                    var variableMap = card.MapVariables();
                                    var variableSplit = text.Split('$', StringSplitOptions.RemoveEmptyEntries);

                                    text = string.Empty;

                                    for (int i = 0; i < card.Variables.Count; i++)
                                    {
                                        if (IsRegexMatch(in_settings, card.Variables[i]))
                                        {
                                            var (type, value) = variableMap[i];

                                            text += $"[gray]${{[/][blue]{Markup.Escape(type)}[/]";

                                            if (!string.IsNullOrEmpty(value))
                                                text += $"[blue]([/][yellow]{Markup.Escape(value)}[/][blue])[/]";

                                            text += "[gray]}[/]";

                                            matches = true;
                                        }
                                        else
                                        {
                                            text += $"[gray]$[/]";
                                        }

                                        if (variableSplit.Length < card.Variables.Count)
                                            continue;

                                        text += variableSplit[i];
                                    }
                                }

                                if (!matches)
                                    continue;

                                results.Add($"[yellow]{card.Name}[/]: {text}");
                                resultsCount++;
                            }
                        }
                        else
                        {
                            // Attempted to search binary file as text, continue...
                            if (BinaryHelper.IsBinaryStream(uncompressedFile.Open()))
                                continue;

                            using (var reader = new StreamReader(uncompressedFile.Open()))
                            {
                                var lines = reader.ReadToEnd().SplitLineBreaks();

                                for (int i = 0; i < lines.Length; i++)
                                {
                                    var line = lines[i];

                                    if (!IsRegexMatch(in_settings, line))
                                        continue;

                                    var prefix = string.Empty;

                                    if (in_settings.LineNumbers)
                                        prefix = $"{Common.GetIntegerPrefix(i + 1, lines.Length)} ";

                                    results.Add($"{prefix}{Markup.Escape(line!)}");
                                    resultsPrefix = false;
                                    resultsCount++;
                                }
                            }
                        }
                    }

                    if (results.Count <= 0)
                        continue;

                    AnsiConsole.MarkupLine($"[gray]{file.Path}[/]");

                    if (!string.IsNullOrEmpty(in_settings.Destination))
                        file.Export(Path.Combine(in_settings.Destination, file.Path));

                    for (int i = 0; i < results.Count; i++)
                        AnsiConsole.MarkupLine($"{(resultsPrefix ? "[gray]-[/] " : "")}{results[i]}");

                    AnsiConsole.WriteLine();
                }
            }

            AnsiConsole.WriteLine($"{resultsCount} results");

            return 0;
        }

        private bool IsRegexMatch(SearchSettings in_settings, string? in_str)
        {
            if (in_str == null)
                return false;

            if (in_settings.RegexPattern != null)
            {
                if (!Regex.IsMatch(in_str, in_settings.RegexPattern, RegexOptions.Compiled))
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }

    public class SearchSettings : CommandSettings
    {
        [CommandArgument(0, "<source>")]
        [Description("The path to the input file.")]
        public required string Source { get; init; }

        [CommandArgument(1, "<search-pattern>")]
        [Description("The pattern to search for file paths.")]
        public required string SearchPattern { get; init; }

        [CommandOption("-d|--destination")]
        [Description("The path to extract the found files to.")]
        public string? Destination { get; init; }

        [CommandOption("-b|--binary-pattern")]
        [Description("The binary pattern to search with inside of binary files.")]
        public string? BinaryPattern { get; init; }

        [CommandOption("-r|--regex-pattern")]
        [Description("The regular expression to search with inside of text files.")]
        public string? RegexPattern { get; init; }

        [CommandOption("-l|--decompile-lua")]
        [Description("Decompile Lua scripts when searching inside of files as text.")]
        [DefaultValue(true)]
        public bool DecompileLua { get; init; } = true;

        [CommandOption("-n|--line-numbers")]
        [Description("Show line numbers when searching inside of files as text.")]
        [DefaultValue(true)]
        public bool LineNumbers { get; init; } = true;
    }
}
