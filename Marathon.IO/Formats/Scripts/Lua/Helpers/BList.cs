using System.Threading;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class BList<T> : BObject where T: BObject
    {
        public readonly BInteger length;
        private readonly List<T> values;

        public BList(BInteger length, List<T> values)
        {
            this.length = length;
            this.values = values;
        }

        public T get(int index) => values[index];

        public T[] asArray(T[] array)
        {
            int i = 0;

            length.iterate(new Thread(new ThreadStart(Run)));

            void Run()
            {
                array[i] = values[i];
                i++;
            }

            return array;
        }
    }
}
