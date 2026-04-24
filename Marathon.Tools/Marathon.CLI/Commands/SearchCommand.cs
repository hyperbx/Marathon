using Marathon.Extensions;
using Marathon.Formats.Archive;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.Formats.Text;
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
            if (!File.Exists(in_settings.Source))
            {
                AnsiConsole.MarkupLine($"[red]File not found:[/] {in_settings.Source}");
                return -1;
            }

            using var arc = new ArcFile(in_settings.Source);

            var resultsCount = 0;

            foreach (var file in arc.EnumerateFiles(in_settings.SearchPattern, SearchOption.AllDirectories))
            {
                if (in_cancellationToken.IsCancellationRequested)
                    break;

                if (in_settings.BinaryPattern == null && in_settings.RegexPattern == null)
                {
                    Console.WriteLine(file.Path);
                    resultsCount++;
                }
                else
                {
                    var results = new List<string>();
                    var uncompressedFile = file.Decompress();

                    if (in_settings.TextSearch)
                    {
                        if (in_settings.RegexPattern == null)
                        {
                            AnsiConsole.MarkupLine("[red]No pattern provided for text search.[/]");
                            return -1;
                        }

                        if (in_settings.DecompileLua && Path.GetExtension(file.Name) is ".lub" or ".lua")
                        {
                            var lub = new LuaBinary(uncompressedFile);
                            lub.LoadSymbols(JsonConvert.DeserializeObject<List<Symbol>>(Properties.Resources.Symbols));

                            var lua = lub.Decompile(new SymbolResolverOptions(file.Name)).SplitLineBreaks();

                            foreach (var line in lua)
                            {
                                if (!IsRegexMatch(in_settings, line))
                                    continue;

                                results.Add(line);
                            }

                            resultsCount += results.Count;
                        }
                        else if (Path.GetExtension(file.Name) == ".mst")
                        {
                            var mst = new TextBook(uncompressedFile);

                            foreach (var card in mst.Cards)
                            {
                                var matches = IsRegexMatch(in_settings, card.Name) || IsRegexMatch(in_settings, card.Text);
                                var text = string.Join("[gray]\\n[/]", string.Join("[gray]\\f[/]", card.Pages).SplitLineBreaks());

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

                                            text += $"[gray]${{[/][blue]{type}[/]";

                                            if (!string.IsNullOrEmpty(value))
                                                text += $"[blue]([/][yellow]{value}[/][blue])[/]";

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
                            }
                        }
                        else
                        {
                            using var reader = new StreamReader(uncompressedFile.Open());

                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();

                                if (!IsRegexMatch(in_settings, line))
                                    continue;

                                results.Add(line!);
                            }
                        }

                        resultsCount += results.Count;
                    }
                    else if (in_settings.BinaryPattern != null)
                    {
                        var scanResults = SignatureScanner.ScanAll(uncompressedFile.Open(), in_settings.BinaryPattern);

                        resultsCount += scanResults.Count();

                        foreach (var scanResult in scanResults)
                            results.Add($"0x{scanResult:X8}");
                    }

                    if (results.Count <= 0)
                        continue;

                    AnsiConsole.MarkupLine($"[gray]{file.Path}[/]");

                    foreach (var result in results)
                        AnsiConsole.MarkupLine($"[gray]-[/] {result}");

                    Console.WriteLine();
                }
            }

            Console.WriteLine($"{resultsCount} results");

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

        [CommandOption("-b|--binary-pattern")]
        [Description("The binary pattern to search with inside of binary files.")]
        public string? BinaryPattern { get; init; }

        [CommandOption("-r|--regex-pattern")]
        [Description("The regular expression to search with inside of text files.")]
        public string? RegexPattern { get; init; }

        [CommandOption("-l|--decompile-lua")]
        [Description("Decompiles Lua scripts when searching inside of files as text.")]
        [DefaultValue(true)]
        public bool DecompileLua { get; init; } = true;

        [CommandOption("-x|--text-search")]
        [Description("Searches inside of files as text.\nUse this for Lua Binaries (*.lub), Text Books (*.mst) or plaintext formats.")]
        public bool TextSearch { get; init; }
    }
}
