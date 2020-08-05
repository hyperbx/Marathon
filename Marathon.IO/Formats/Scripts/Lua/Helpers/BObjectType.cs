using System.Threading;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public abstract class BObjectType<T> where T: BObject
    {
        public abstract T parse(ExtendedBinaryReader buffer, BHeader header);

        public BList<T> parseList(ExtendedBinaryReader buffer, BHeader header)
        {
            BInteger length = header.integer.parse(buffer, header);
            List<T> values = new List<T>();

            length.iterate(new Thread(new ThreadStart(Run)));

            void Run() => values.Add(parse(buffer, header));

            return new BList<T>(length, values);
        }
    }
}
