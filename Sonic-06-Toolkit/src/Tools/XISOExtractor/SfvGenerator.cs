using System.IO;
using System.Text;

namespace XISOExtractorGUI
{

    public class SfvGenerator
    {
        public static readonly Crc32 Crc = new Crc32();
        private readonly string _file;
        private readonly StringBuilder _sb = new StringBuilder();

        public SfvGenerator(string file) { _file = file; }

        ~SfvGenerator() { Save(); }

        public void Save() { File.WriteAllText(_file, _sb.ToString()); }

        public void AddFile(string name, uint crc) { _sb.AppendLine(string.Format("{0} {1:X8}", name.TrimStart('\\'), crc)); }

        public class Crc32
        {
            private readonly uint[] _table;

            public Crc32()
            {
                const uint poly = 0xedb88320;
                _table = new uint[256];
                for (uint i = 0; i < _table.Length; ++i)
                {
                    var temp = i;
                    for (var j = 8; j > 0; --j)
                    {
                        if ((temp & 1) == 1)
                            temp = (temp >> 1) ^ poly;
                        else
                            temp >>= 1;
                    }
                    _table[i] = temp;
                }
            }

            public uint ComputeChecksum(byte[] bytes, uint crc = 0)
            {
                crc = ~crc;
                for (var i = 0; i < bytes.Length; ++i)
                {
                    var index = (byte)(((crc) & 0xff) ^ bytes[i]);
                    crc = (crc >> 8) ^ _table[index];
                }
                return ~crc;
            }
        }
    }
}