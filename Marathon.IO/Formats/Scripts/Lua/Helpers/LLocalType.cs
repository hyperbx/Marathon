using System;

namespace Marathon.IO.Formats.Scripts.Lua.Helpers
{
    public class LLocalType : BObjectType<LLocal>
    {
        public override LLocal parse(ExtendedBinaryReader buffer, BHeader header)
        {
            LString name = header.@string.parse(buffer, header);
            BInteger start = header.integer.parse(buffer, header),
                     end = header.integer.parse(buffer, header);

            if (header.debug)
                Console.WriteLine($"-- parsing local, name: {name} from {start.asInt()} to {end.asInt()}\n");

            return new LLocal(name, start, end);
        }
    }
}
