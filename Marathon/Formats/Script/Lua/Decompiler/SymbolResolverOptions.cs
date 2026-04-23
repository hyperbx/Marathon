namespace Marathon.Formats.Script.Lua.Decompiler
{
    public class SymbolResolverOptions
    {
        /// <summary>
        /// The name of the file used for filtering symbols.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Determines whether to search all symbols instead of just the symbols for <see cref="FileName"/>.
        /// </summary>
        public bool IsGlobalResolver { get; set; } = true;

        public SymbolResolverOptions() { }

        public SymbolResolverOptions(string in_fileName, bool in_isGlobalResolver = false)
        {
            FileName = in_fileName;
            IsGlobalResolver = in_isGlobalResolver;
        }

        public SymbolResolverOptions(bool in_isGlobalResolver)
        {
            IsGlobalResolver = in_isGlobalResolver;
        }
    }
}
