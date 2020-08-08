using System;
using System.Text;
using System.Threading;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LStringType : BObjectType<LString>
    {
        protected StringBuilder initialValue() => new StringBuilder();

        public override LString parse(ExtendedBinaryReader buffer, BHeader header)
        {
            BSizeT sizeT = header.sizeT.parse(buffer, header);
            StringBuilder b = new StringBuilder();

            sizeT.iterate(new Thread(new ThreadStart(Run)));

            void Run() => b.Append((char)(0xFF & buffer.ReadByte()));

            string s = b.ToString();

            if (header.debug)
                Console.WriteLine($"-- parsed <string> \"{s}\"");

            return new LString(sizeT, s);
        }
    }
}
