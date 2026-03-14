using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ResourceTableFactory
    {
        public static IResourceTable ReadResourceTableByType(BinaryObjectReaderEx in_reader, IChunk in_parentChunk, ResourceType in_type)
        {
            IResourceTable resource = in_type switch
            {
                ResourceType.Model => new ModelResourceTable(in_reader, in_parentChunk),
                ResourceType.CellSprite => new CellSpriteResourceTable(in_reader, in_parentChunk),
                ResourceType.Camera => new CameraResourceTable(in_reader, in_parentChunk),
                ResourceType.Screen => new ScreenResourceTable(in_reader, in_parentChunk),
                ResourceType.Light => new LightResourceTable(in_reader, in_parentChunk),
                ResourceType.Primitive => new PrimitiveResourceTable(in_reader, in_parentChunk),
                ResourceType.Extra => new ExtraResourceTable(in_reader, in_parentChunk),
                _ => null
            };

            // Skip resources.
            if (resource == null)
                in_reader.JumpAhead(sizeof(uint) * 8);

            return resource;
        }
    }
}
