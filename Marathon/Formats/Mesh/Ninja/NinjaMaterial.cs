namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaMaterial
    {
        public NinjaNext_MaterialType Type { get; set; }

        public NinjaNext_MaterialType Flag { get; set; }

        public int UserDefined { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }
        
        public uint MaterialColourOffset { get; set; }
        
        public uint MaterialLogicOffset { get; set; }
        
        public uint MaterialTexMapDescriptionOffset { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            Type = (NinjaNext_MaterialType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            Flag = (NinjaNext_MaterialType)reader.ReadUInt32();
            UserDefined = reader.ReadInt32();
            MaterialColourOffset = reader.ReadUInt32();
            MaterialLogicOffset = reader.ReadUInt32();
            MaterialTexMapDescriptionOffset = reader.ReadUInt32();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
        }
    
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets, List<NinjaMaterialColours> MaterialColours, List<NinjaMaterialLogic> MaterialLogics, List<NinjaTextureMap> TextureMaps)
        {
            ObjectOffsets.Add($"Material{index}", (uint)writer.BaseStream.Position);
            writer.Write((uint)Flag);
            writer.Write(UserDefined);

            writer.AddOffset($"Material{index}ColourOffset", 0);
            for (int i = 0; i < MaterialColours.Count; i++)
            {
                if (MaterialColourOffset == MaterialColours[i].Offset)
                {
                    writer.Write(ObjectOffsets[$"ColourOffset{i}"] - writer.Offset);
                }
            }

            writer.AddOffset($"Material{index}LogicOffset", 0);
            for (int i = 0; i < MaterialLogics.Count; i++)
            {
                if (MaterialLogicOffset == MaterialLogics[i].Offset)
                {
                    writer.Write(ObjectOffsets[$"LogicOffset{i}"] - writer.Offset);
                }
            }

            if (MaterialTexMapDescriptionOffset != 0)
            {
                writer.AddOffset($"Material{index}TexDescOffset", 0);
                for (int i = 0; i < TextureMaps.Count; i++)
                {
                    if (MaterialTexMapDescriptionOffset == TextureMaps[i].Offset)
                    {
                        writer.Write(ObjectOffsets[$"TexDescOffset{i}"] - writer.Offset);
                    }
                }
            }
            else
                writer.Write(0);
            
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
        }
    
        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            writer.Write((uint)Type);
            writer.AddOffset($"Material{index}", 0);
            writer.Write(ObjectOffsets[$"Material{index}"] - writer.Offset);
        }
    }
}
