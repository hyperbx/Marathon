using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Formats.Ninja.Chunks;
using Marathon.IO;
using System.Collections.Generic;
using System.IO;

// Format names:        Ninja Next (speculatory)
// Format references:   Sonicteam::NN
// Format designers:    SEGA Consumer Research and Development Dept. #2
// Format researchers:  Radfordhound, ItsEasyActually, ArMM1998, Shadowth117, Knuxfan24, Hyper

namespace Marathon.Formats.Ninja
{
    /// <summary>
    /// Support for *.xn* files; used for various resources.
    /// </summary>
    public class NinjaNext : FileBase
    {
        public string Name { get; set; }

        public InfoChunk Info { get; set; }

        public List<IChunk> Chunks => Info?.Chunks;

        public List<IChunk> ExtraChunks => Info?.ExtraChunks;

        public IChunk this[string in_id]
        {
            get => Info[in_id];
        }

        public NinjaNext() { }

        public NinjaNext(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            Info = new InfoChunk(reader);

            if (this[FileNameChunk.ID] is not FileNameChunk out_fileNameChunk)
                return;

            Name = out_fileNameChunk.Name;
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            Info.Write(writer);
            Info.WriteChunks(writer);

            var offsetChunk = new OffsetChunk();

            foreach (var offset in writer.Offsets)
                offsetChunk.Offsets.Add((uint)(offset.Value - InfoChunk.Size));

            offsetChunk.Offsets.Sort();

            ExtraChunks.Clear();
            ExtraChunks.Add(offsetChunk);

            if (!string.IsNullOrEmpty(Name))
                ExtraChunks.Add(new FileNameChunk(Name));

            ExtraChunks.Add(new EndChunk());

            Info.WriteExtraChunks(writer);
        }

        public T GetChunk<T>() where T : IChunk
        {
            return Info.GetChunk<T>();
        }

        public IChunk GetChunk(string in_id)
        {
            return Info[in_id];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
