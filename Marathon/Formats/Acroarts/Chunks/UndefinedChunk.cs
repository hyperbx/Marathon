using Marathon.Helpers;
using Marathon.Helpers.Converters;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using Newtonsoft.Json;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class UndefinedChunk : IBinarySerializableEx
    {
        public string ChunkID { get; set; }

        public FourCC Signature { get; set; }

        [JsonConverter(typeof(ByteArrayToHexStringConverter))]
        public byte[] Data { get; set; }

        public UndefinedChunk() { }

        public UndefinedChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);

            ChunkID = Signature.ToString();
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Signature = in_reader.ReadObject<FourCC>();

            var chunkLength = in_reader.Read<uint>();

            Data = in_reader.ReadBytes((int)chunkLength);

            Logger.Warning($"Encountered undefined chunk at 0x{in_reader.OffsetOrigin:X8}: {Signature}");
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Signature);

            var length = in_writer.Reserve<uint>();

            in_writer.WriteBytes(Data);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }
}
