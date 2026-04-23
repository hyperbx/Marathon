using Marathon.Extensions;
using Marathon.Formats.Archive;
using Marathon.Formats.Script.Lua;
using Marathon.Formats.Script.Lua.Decompiler;
using Marathon.IO.Types.FileSystem;
using Newtonsoft.Json;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Marathon.LuaSymbolGenerator
{
    public static class Program
    {
        public static void Main(string[] in_args)
        {
            Console.MarkupLine("[bold]Marathon Lua Symbol Generator[/]");

            if (in_args.Length <= 0)
                Console.WriteLine();

            var app = new CommandApp<MainCommand>();
            app.Run(in_args);
        }
    }

    public class MainCommand : Command<MainSettings>
    {
        public VirtualDirectory GameFileSystem { get; set; } = new();

        protected override int Execute(CommandContext in_context, MainSettings in_settings, CancellationToken in_cancellationToken)
        {
            var archives = Directory.GetFiles(in_settings.Root, "*.arc", SearchOption.AllDirectories);

            if (archives.Length <= 0)
            {
                Console.MarkupLine("\n[red]Error: invalid filesystem.[/]");
                return -1;
            }

            AnsiConsole.Progress().Start(ctx =>
            {
                var indexingFilesystemTask = ctx.AddTask("Indexing filesystem", maxValue: archives.Length);
                var resolvingNamesTask = ctx.AddTask("Resolving symbol names").IsIndeterminate();
                var resolvingFilesTask = ctx.AddTask("Resolving symbol files").IsIndeterminate();

                // Indexing filesystem

                foreach (var file in archives)
                {
                    var arc = new ArcFile(file);

                    foreach (var node in arc.EnumerateNodes())
                    {
                        if (node.IsDirectory)
                        {
                            GameFileSystem.AddDirectory(node as IDirectory);
                        }
                        else
                        {
                            GameFileSystem.AddFile(node as IFile);
                        }
                    }

                    indexingFilesystemTask.Increment(1);
                }

                // Resolving symbol names

                var lubs = GameFileSystem.GetFiles("*.lub", SearchOption.AllDirectories);
                var unmapped = new HashSet<string>();

                resolvingNamesTask.IsIndeterminate(false);
                resolvingNamesTask.MaxValue(lubs.Length);

                foreach (var file in lubs)
                {
                    if (_prohibitedScripts.Contains(file.Name))
                    {
                        resolvingNamesTask.Increment(1);
                        continue;
                    }

                    var lub = new LuaBinary(file.Decompress());

                    try
                    {
                        var lua = lub.Decompile();
                        var luaLines = lua.SplitLineBreaks();
                        var enemyName = string.Empty;
                        var isEnemyTable = false;

                        foreach (var line in luaLines)
                        {
                            if (line.StartsWith("Enemy.") && line.EndsWith(" = {"))
                            {
                                enemyName = line[(line.IndexOf('.') + 1)..];
                                enemyName = enemyName[..enemyName.IndexOf(" = {")];
                                isEnemyTable = true;
                                continue;
                            }

                            if (isEnemyTable)
                            {
                                // Add anonymous function argument names for enemy scripts.
                                if (line.Contains(" = function"))
                                {
                                    var funcName = line[..line.IndexOf(" = function")].TrimStart();
                                    var symbolName = $"Enemy.{enemyName}.{funcName}";

                                    _symbols.Add(new Symbol(symbolName, ["enemy"], in_isAnonymousFunction: true));
                                }
                                else if (line.StartsWith('}'))
                                {
                                    isEnemyTable = false;
                                }
                            }
                            else if (line.StartsWith("function"))
                            {
                                var symbolName = line["function ".Length..];

                                symbolName = symbolName[..symbolName.LastIndexOf('(')];

                                // Skip duplicates.
                                if (_symbols.Any(x => x.Scope == symbolName))
                                    continue;

                                // Add function argument names for action area scripts.
                                if (_stages.Any(line.Contains))
                                {
                                    if (line.Contains("(a1, a2, a3, a4)"))
                                    {
                                        // Function is event handler for switch.
                                        _symbols.Add(new Symbol(symbolName, ["self", "on", "actorID", "otherID"]));
                                    }
                                    else if (line.Contains("(a1, a2, a3)"))
                                    {
                                        // Function is event handler with extra user data.
                                        _symbols.Add(new Symbol(symbolName, ["self", "actorID", "otherID"]));
                                    }
                                    else if (line.Contains("(a1, a2)"))
                                    {
                                        var arguments = new List<string>()
                                        {
                                            "self"
                                        };

                                        if (symbolName.Contains("Step"))
                                        {
                                            arguments.Add("delta_time");
                                        }
                                        else if (!symbolName.Contains("PlayerReachesTheGoal"))
                                        {
                                            arguments.Add("actorID");
                                        }

                                        _symbols.Add(new Symbol(symbolName, arguments));
                                    }
                                    else if (line.Contains("(a1)") && _instanceMethods.Any(line.Contains))
                                    {
                                        // Function is instance method.
                                        _symbols.Add(new Symbol(symbolName, ["self"]));
                                    }
                                }
                                else if (!_symbols.Any(x => x.Scope == symbolName))
                                {
                                    // Unmapped scope, add to TODOs.
                                    unmapped.Add($"{file.Path}: {line}");
                                }
                            }
                        }
                    }
                    catch { }

                    resolvingNamesTask.Increment(1);
                }

                using (var sw = new StreamWriter("SymbolsToDo.txt"))
                {
                    foreach (var symbol in unmapped)
                        sw.WriteLine(symbol);
                }

                // Resolving symbol files

                resolvingFilesTask.IsIndeterminate(false);
                resolvingFilesTask.MaxValue(lubs.Length);

                // Very hacky way of checking which symbols come from
                // which files. This ensures all existing symbols in
                // the table of known ones are also resolved here.
                foreach (var file in lubs)
                {
                    if (_prohibitedScripts.Contains(file.Name))
                    {
                        resolvingFilesTask.Increment(1);
                        continue;
                    }

                    var lub = new LuaBinary(file.Decompress());
                    var lua = lub.Decompile();
                    var luaLines = lua.SplitLineBreaks();

                    for (int i = 0; i < _symbols.Count; i++)
                    {
                        var symbol = _symbols[i];

                        if (symbol.IsAnonymousFunction.GetValueOrDefault())
                        {
                            var scopeSplit = symbol.Scope.Split('.');
                            var root = string.Join('.', scopeSplit.Take(scopeSplit.Length - 1));
                            var func = scopeSplit.TakeLast(1).Single();

                            // Anonymous function not found, continue...
                            if (!luaLines.Any(x => x.Contains($"{root} = {{")) || !luaLines.Any(x => x.Contains($"{func} = function(")))
                                continue;
                        }
                        else
                        {
                            if (symbol.Upvalues?.Count > 0)
                            {
                                // Upvalue scope not found, continue...
                                if (!luaLines.Any(x => x.Contains($"{symbol.Scope} = {{")))
                                    continue;
                            }
                            else
                            {
                                // Function not found, continue...
                                if (!luaLines.Any(x => x.Contains($"function {symbol.Scope}(")))
                                    continue;
                            }
                        }

                        _symbols[i].Files.Add(file.Name);
                    }

                    resolvingFilesTask.Increment(1);
                }

                _symbols = [.. _symbols.OrderBy(x => x.Scope)];

                File.WriteAllText("Symbols.json", JsonConvert.SerializeObject(_symbols, Formatting.Indented));
            });

            Console.WriteLine("Done.");

            return 0;
        }

        private readonly string[] _prohibitedScripts =
        [
            "standard.lub"
        ];

        private readonly string[] _stages =
        [
            "AquaticBase",
            "Boss",
            "CrisisCity",
            "DustyDesert",
            "EndOfTheWorld",
            "Event",
            "FlameCore",
            "KingdomValley",
            "Other",
            "RadicalTrain",
            "TropicalJungle",
            "Town",
            "WhiteAcropolis",
            "WaveOcean"
        ];

        private readonly string[] _instanceMethods =
        [
            "constructor",
            "Setup",
            "StartPlaying",
            "boss_is_dead",
            "StageTitle",
            "GetRankTable",
            "LightAllFlames",
            "OffAllLightCollision",
            "GetName",
            "AtoB",
            "BtoC",
            "BtoF2",
            "CtoF",
            "CtoE",
            "F1toB"
        ];

        private List<Symbol> _symbols =
        [
            new("ScriptCallback", ["index", "path"]),
            new("Begin", ["level"]),
            new("Step", ["delta_time"]),
            new("ProcessMessage", ["eventID"]),
            new("CreateTask", ["taskID"]),
            new("inherits_from", ["base"]),
            new("Class.constructor", ["self"]),
            new("Class.GetName", ["self"]),
            new("Class.Type", ["self"]),
            new("Object.constructor", ["self"]),
            new("Object.Setup", ["self"]),
            new("Object.Exec", ["self", "delta_time"]),
            new("Object.Step", ["self", "delta_time"]),
            new("Object.ChangeState", ["self", "state"]),
            new("Object.Wake", ["self"]),
            new("Object.ProcessEvent", ["self", "eventID"]),
            new("State.Main", ["self", "stage"]),
            new("ActionArea.constructor", ["self"]),
            new("ActionArea.Type", ["self"]),
            new("ActionArea.Setup", ["self"]),
            new("ActionArea.AddComponent", ["self", "comp"]),
            new("ActionArea.ChangeArea", ["self", "area"]),
            new("ActionArea.StartPlaying", ["self"]),
            new("ActionStage.constructor", ["self"]),
            new("ActionStage.Type", ["self"]),
            new("ActionStage.Setup", ["self"]),
            new("ActionStage.StageTitle", ["self"]),
            new("ActionStage.NewArea", ["self", "area"]),
            new("ActionStage.Load", ["self"]),
            new("ActionStage.AddComponent", ["self", "comp"]),
            new("ActionStage.stepNormal", ["self", "delta_time"]),
            new("ActionStage.stepLoad", ["self", "delta_time"]),
            new("ActionStage.stepLoad2", ["self", "delta_time"]),
            new("ActionStage.stepLoad3", ["self", "delta_time"]),
            new("ActionStage.Step", ["self", "delta_time"]),
            new("ActionStage.ChangeArea", ["self", "area"]),
            new("ActionStage.Retry", ["self"]),
            new("ActionStage.StartPlaying", ["self"]),
            new("ActionStage.GetScore", ["self"]),
            new("ActionStage.SetScore", ["self", "value"]),
            new("ActionStage.AddScore", ["self", "value"]),
            new("ActionStage.GetPlayTime", ["self"]),
            new("ActionStage.GetLife", ["self"]),
            new("ActionStage.GetRingCount", ["self"]),
            new("ActionStage.SetLife", ["self", "value"]),
            new("ActionStage.setRingCount", ["self", "value"]),
            new("ActionStage.CalcTimeBonus", ["self"]),
            new("ActionStage.ExtendLife", ["self"]),
            new("ActionStage.MissionComplete", ["self"]),
            new("ActionStage.GetRankTable", ["self"]),
            new("ActionStage.ControlPause", ["self", "enabled"]),
            new("ActionStage.CallEvent", ["self", "event"]),
            new("ActionStage.Switch", ["self", "switch"]),
            new("ActionStage.PlayerGetsItem", ["self", "info"]),
            new("ActionStage.Score", ["self", "info"]),
            new("ActionStage.SetScoreMsg", ["self", "info"]),
            new("ActionStage.PlayerGetsRing", ["self", "info"]),
            new("ActionStage.PlayerGetsPsiValue", ["self", "info"]),
            new("ActionStage.GetPlayerCount", ["self", "info"]),
            new("ActionStage.PlayerReachesTheGoal", ["self"]),
            new("ActionStage.NotifyRestart", ["self"]),
            new("ActionStage.SetRingCount", ["self", "info"]),
            new("ActionStage.Start.constructor", ["self"]),
            new("ActionStage.Start.Main", ["self", "stage"]),
            new("ActionStage.Retry.constructor", ["self"]),
            new("ActionStage.Retry.Main", ["self", "stage"]),
            new("ActionStage.Playing.Main", ["self", "stage"]),
            new("ActionStage.Playing.PlayerDies", ["self", "stage"]),
            new("ActionStage.Clear.Main", ["self", "stage"]),
            new("ActionStage.Clear.HudNotify", ["self", "stage"]),
            new("ActionStage.GameOver.Main", ["self", "stage"]),
            new("FlameSingleSwitch.constructor", ["self"]),
            new("FlameSingleSwitch.Setup", ["self", "count_flame_switch_all"]),
            new("FlameSingleSwitch.OnOff", ["self", "on", "area", "eventID", "actorID"]),
            new("FlameCore.ShadowB.flame_switch_onoff", ["self", "on", "actorID", "otherID"]),
            new("main", ["mission"]),
            new("on_event", ["mission", "eventID"]),
            new("on_hint", ["mission", "hintID"]),
            new("on_goto", ["mission", "gotoID"]),
            new("on_talk_icon", ["mission", "msgID"]),
            new("on_talk_icon_talk", ["mission", "msgID"]),
            new("on_talk_setup", ["mission", "msgID"]),
            new("on_talk_oepn", ["mission", "msgID"]),
            new("on_talk_open", ["mission", "msgID"]),
            new("on_talk_close", ["mission", "msgID"]),
            new("mission_talk", ["mission", "msgID"]),
            new("missionman_talk_icon", ["mission", "msgID"]),
            new("missionman_talk_setup", ["mission", "msgID"]),
            new("missionman_talk_close", ["mission", "msgID"]),
            new("missionman_event", ["mission", "eventID"]),
            new("on_goal", ["mission"]),
            new("SetupModule", ["player"]),
            new("SetupModuleSub", ["player"]),
            new("SetupModuleDebug", ["player"]),
            new("SetupModuleDebugSub", ["player"]),
            new("HeightToSpeed", ["height"]),
            new("SpeedToHeight", ["speed"]),
            new("SpeedByHeightBias", ["speed", "bias"]),
            new("HeightAndDistanceToSpeed", ["height", "distance"]),
            new("ToMeter", ["value"]),
            new("GetRandomAppearRadius", ["enemy", "min", "max"]),
            new("Build", ["render"]),
            new("Render2D", ["render"]),
            new("CreateCSM", ["render"]),
            new("RenderCSM", ["render", "levels", "objects"]),
            new("MyPrepareColorRender", ["render"]),
            new("PrepareColorRender", ["render"]),
            new("PostColorRender", ["render"]),
            new("RenderAfterPostprocess", ["render"]),
            new("ViewTexture", ["render"]),
            new("RenderRaderMap", ["render"]),
            new("ViewTextureRaderMap", ["render"]),
            new("RenderMain", ["render", "surface", "texture", "camera"]),
            new("RenderMainForSingle", ["render", "surface", "texture"]),
            new("RenderMainForMulti", ["render", "surface", "texture"]),
            new("RenderCustom", ["render", "camera", "world"]),
            new("CreateReflectionTexture", ["render"]),
            new("RenderReflection", ["render"]),
            new("RenderVolumeLights", ["render"]),
            new("RenderMefiress", ["render"]),
            new("OutputLinearZ", ["render", "name", "surface", "world"]),
            new("OutputOnlyZ", ["render", "name", "surface", "world"]),
            new("OutputZNone", ["render", "name", "surface", "world"]),
            new("Event.Init", ["mode"], in_isAnonymousFunction: true),
            new("ObjectPhysics.Init", ["obj"], in_isAnonymousFunction: true),
            new("ObjectPhysics.OnBroken", ["obj"], in_isAnonymousFunction: true),
            new("ObjectPhysics.OnDamage", ["obj"], in_isAnonymousFunction: true),
            new("ObjectPhysics.EndAnimation", ["obj"], in_isAnonymousFunction: true),
            new("Seesaw.Init", ["obj"], in_isAnonymousFunction: true),
            new("Enemy.firstmefiress_omega", in_upvalues: ["hit_count"]),
            new("Enemy.firstmefiress_shadow", in_upvalues: ["hit_count", null, null, "state"]),
            new("Enemy.secondmefiress_shadow", in_upvalues: ["hit_count", null, null, "state"])
        ];
    }

    public class MainSettings : CommandSettings
    {
        [CommandArgument(0, "<root>")]
        [Description("The path to the root directory of the game.")]
        public required string Root { get; init; }
    }
}
