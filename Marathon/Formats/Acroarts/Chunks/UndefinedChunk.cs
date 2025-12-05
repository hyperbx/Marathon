using Marathon.Helpers;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using Marathon.IO.Types.BINA;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class UndefinedChunk : IChunk
    {
        public string ChunkID { get; set; }

        public FourCC Signature { get; set; }

        public byte[] Data { get; set; }

        public UndefinedChunk() { }

        public UndefinedChunk(BINAReader in_reader)
        {
            Read(in_reader);

            ChunkID = Signature.ToString();
        }

        public void Read(BINAReader in_reader)
        {
            var pos = in_reader.Position;

            Signature = in_reader.ReadObject<FourCC>();

            var chunkLength = in_reader.Read<uint>();

            Data = in_reader.ReadBytes((int)chunkLength);

            Logger.Warning($"Encountered undefined chunk at 0x{pos:X8}: {Signature}");
        }

        public void Write(BINAWriter in_writer)
        {
            in_writer.WriteObject(Signature);
            var length = in_writer.Reserve<uint>();
            in_writer.WriteBytes(Data);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }
}
