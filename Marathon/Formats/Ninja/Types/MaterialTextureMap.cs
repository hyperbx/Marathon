using Marathon.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Ninja.Types
{
    public class MaterialTextureMap
    {
        public List<MaterialTextureMapDesc> Descriptions { get; set; } = [];

        public MaterialTextureMap() { }

        public MaterialTextureMap(BinaryObjectReaderEx in_reader, int in_textureCount)
        {
            Read(in_reader, in_textureCount);
        }

        public void Read(BinaryObjectReaderEx in_reader, int in_textureCount)
        {
            for (int i = 0; i < in_textureCount; i++)
                Descriptions.Add(new(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            foreach (var desc in Descriptions)
                desc.Write(in_writer);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not MaterialTextureMap out_textureMap)
                return false;

            return out_textureMap.Descriptions.SequenceEqual(Descriptions);
        }
    }
}
