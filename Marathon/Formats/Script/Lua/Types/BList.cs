namespace Marathon.Formats.Script.Lua.Types
{
    public class BList<T> : BObject where T : BObject
    {
        public readonly BInteger Length;

        private readonly List<T> _values;

        public BList(BInteger length, List<T> values)
        {
            Length = length;
            _values = values;
        }

        public T Get(int index) => _values[index];

        public T[] AsArray(T[] array)
        {
            int i = 0;

            Length.Iterate(Run);

            void Run()
            {
                array[i] = _values[i];
                i++;
            }

            return array;
        }
    }
}
