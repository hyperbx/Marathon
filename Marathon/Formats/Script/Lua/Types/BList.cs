using System.Collections.Generic;

// Format names:        Lua Binary
// Format designers:    Tecgraf, PUC-Rio
// Format researchers:  tehtmi, Thomas Klaeger, Shadow LAG

namespace Marathon.Formats.Script.Lua.Types
{
    public class BList<T>(BInteger in_length, List<T> in_values) : BObject where T : BObject
    {
        private readonly List<T> _values = in_values;

        public BInteger Length => in_length;

        public T Get(int in_index)
        {
            return _values[in_index];
        }

        public T[] AsArray(T[] in_array)
        {
            var i = 0;

            Length.Iterate
            (
                () =>
                {
                    in_array[i] = _values[i];
                    i++;
                }
            );

            return in_array;
        }
    }
}
