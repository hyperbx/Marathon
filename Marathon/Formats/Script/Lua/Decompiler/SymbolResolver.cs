using Marathon.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Marathon.Formats.Script.Lua.Decompiler
{
    public static class SymbolResolver
    {
        private static Stack<string> _scope = [];

        public static SymbolResolverOptions Options { get; set; }

        public static List<Symbol> Symbols { get; set; } = [];

        public static List<Symbol> GetSymbols(string in_fileName = "")
        {
            return string.IsNullOrEmpty(in_fileName)
                ? Symbols
                : [.. Symbols.Where(x => x.Files.Contains(in_fileName))];
        }

        public static string ResolveArgumentSymbol(Decompiler in_decompiler, int in_index, bool in_isInstanceMethod = false)
        {
            var index = in_isInstanceMethod ? in_index - 1 : in_index;
            var result = $"a{index + 1}";

            foreach (var symbol in GetSymbols(Options?.FileName))
            {
                var scope = string.Empty;

                if (symbol.IsAnonymousFunction.HasValue && symbol.IsAnonymousFunction.Value)
                {
                    scope = GetScopeDeclaration();
                }
                else
                {
                    if (in_decompiler.Target == null)
                        continue;

                    scope = in_decompiler.Target.ToString();
                }

                if (scope != symbol.Scope)
                    continue;

                if (symbol.Arguments == null || symbol.Arguments.Count <= in_index)
                    return result;

                if (string.IsNullOrEmpty(symbol.Arguments[in_index]))
                    return result;

                result = symbol.Arguments[in_index];
            }

            return result;
        }

        public static string ResolveUpvalueSymbol(int in_index)
        {
            var result = $"v{in_index + 1}";
            var resolvedScope = string.Empty;
            var depth = 0;

            foreach (var scope in _scope.Reverse())
            {
                resolvedScope += scope;

                foreach (var symbol in GetSymbols(Options?.FileName))
                {
                    if (resolvedScope != symbol.Scope)
                        continue;

                    if (symbol.Upvalues == null || symbol.Upvalues.Count <= in_index)
                        return result;

                    if (string.IsNullOrEmpty(symbol.Upvalues[in_index]))
                        return result;

                    result = symbol.Upvalues[in_index];
                }

                if (depth <= _scope.Count - 1)
                    resolvedScope += ".";

                depth++;
            }

            return result;
        }

        public static void SetOptions(SymbolResolverOptions in_symbolResolverOptions)
        {
            Options = in_symbolResolverOptions;
        }

        public static void LoadSymbols(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            Symbols = JsonConvert.DeserializeObject<List<Symbol>>(File.ReadAllText(in_path));
        }

        public static void LoadSymbols(List<Symbol> in_symbols)
        {
            Symbols = in_symbols ?? [];
        }

        public static void PushScope(string in_scope)
        {
            _scope.Push(in_scope);
        }

        public static void PopScope()
        {
            _scope.Pop();
        }

        public static string GetScopeDeclaration()
        {
            return string.Join('.', _scope.Reverse());
        }
    }

    public class Symbol
    {
        public string Scope { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public HashSet<string> Files { get; set; } = [];

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Arguments { get; set; } = [];

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Upvalues { get; set; } = [];

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsAnonymousFunction { get; set; }

        public Symbol() { }

        public Symbol(string in_scope, List<string> in_arguments = null, List<string> in_upvalues = null, bool? in_isAnonymousFunction = null)
        {
            Scope = in_scope;
            Arguments = in_arguments;
            Upvalues = in_upvalues;
            IsAnonymousFunction = in_isAnonymousFunction;
        }

        public override string ToString()
        {
            var result = Scope;

            if (Arguments.Count > 0)
            {
                result += "(";

                for (int i = 0; i < Arguments.Count; i++)
                {
                    result += string.IsNullOrEmpty(Arguments[i]) ? $"a{i + 1}" : Arguments[i];

                    if (i < Arguments.Count - 1)
                        result += ", ";
                }

                result += ")";
            }
            else if (Upvalues.Count > 0)
            {
                result += " = { ";

                for (int i = 0; i < Upvalues.Count; i++)
                {
                    result += string.IsNullOrEmpty(Upvalues[i]) ? $"v{i + 1}" : Upvalues[i];

                    if (i < Upvalues.Count - 1)
                        result += ", ";
                }

                result += " }";
            }

            return result;
        }
    }
}
