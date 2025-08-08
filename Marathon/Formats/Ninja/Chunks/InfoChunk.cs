using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class InfoChunk : IChunk
    {
        private uint _chunkStart;
        private uint _chunkOffset;
        private uint _chunkDataLength;
        private uint _offsetChunkOffset;
        private uint _offsetChunkLength;

        public const string ID = "NXIF"; // Ninja directX InFo

        public const int Size = 0x20;

        public int Version { get; set; } = 1;

        public List<IChunk> Chunks { get; set; } = [];

        public List<IChunk> ExtraChunks { get; set; } = [];

        public InfoChunk() { }

        public InfoChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            var chunkCount = in_reader.Read<int>();
            var chunkOffset = in_reader.Read<int>();
            var chunkDataLength = in_reader.Read<int>();
            var relocChunkOffset = in_reader.Read<int>();
            var relocChunkLength = in_reader.Read<int>();

            Version = in_reader.Read<int>();

            for (int i = 0; i < chunkCount; i++)
            {
                if (!ReadChunk(in_reader, Chunks))
                    break;
            }

            while (in_reader.Position < in_reader.Length)
            {
                if (!ReadChunk(in_reader, ExtraChunks))
                    break;
            }
        }

        private bool ReadChunk(BinaryObjectReaderEx in_reader, List<IChunk> in_chunkList)
        {
            var pos = in_reader.Position;
            var nextChunkSignature = in_reader.Read<FourCC>();
            var nextChunkLength = in_reader.Read<uint>();

            // Return to chunk start.
            in_reader.JumpTo(pos);

            var chunk = ChunkFactory.GetChunkByFourCC(in_reader, nextChunkSignature);

            in_chunkList.Add(chunk);

            // Jump to next chunk.
            in_reader.JumpTo(pos + nextChunkLength + 8);

            return chunk.GetChunkID() != EndChunk.ID;
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);
            in_writer.Write(Size - 8);
            in_writer.Write(Chunks.Count);

            _chunkOffset = in_writer.Reserve<uint>(true);
            _chunkDataLength = in_writer.Reserve<uint>(true);
            _offsetChunkOffset = in_writer.Reserve<uint>(true);
            _offsetChunkLength = in_writer.Reserve<uint>(true);

            in_writer.Write(Version);
        }

        public void WriteChunks(BinaryObjectWriterEx in_writer)
        {
            _chunkStart = (uint)in_writer.Position;

            foreach (var chunk in Chunks)
                chunk.Write(in_writer);
        }

        public void WriteExtraChunks(BinaryObjectWriterEx in_writer)
        {
            foreach (var chunk in ExtraChunks)
            {
                var isOffsetChunk = chunk.GetChunkID() == OffsetChunk.ID;
                var offsetChunkPos = (uint)in_writer.Position;

                if (isOffsetChunk)
                {
                    in_writer.WriteReserved(_chunkOffset, _chunkStart);
                    in_writer.WriteReserved(_chunkDataLength, (uint)(in_writer.Position - _chunkStart));
                    in_writer.WriteReserved(_offsetChunkOffset, (uint)in_writer.Position);
                }

                chunk.Write(in_writer);

                if (isOffsetChunk)
                    in_writer.WriteReserved(_offsetChunkLength, (uint)(in_writer.Position - offsetChunkPos));
            }
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
