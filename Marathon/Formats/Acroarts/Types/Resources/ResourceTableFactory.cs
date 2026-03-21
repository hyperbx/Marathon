using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ResourceTableFactory
    {
        public static IResourceTable ReadResourceTableByType(BinaryObjectReaderEx in_reader, ResourceType in_type)
        {
            IResourceTable resource = in_type switch
            {
                ResourceType.Model => new ModelResourceTable(in_reader),
                ResourceType.CellSprite => new CellSpriteResourceTable(in_reader),
                ResourceType.Camera => new CameraResourceTable(in_reader),
                ResourceType.Screen => new ScreenResourceTable(in_reader),
                ResourceType.Light => new LightResourceTable(in_reader),
                ResourceType.Primitive => new PrimitiveResourceTable(in_reader),
                ResourceType.Extra => new ExtraResourceTable(in_reader),
                _ => null
            };

            // Skip resources.
            if (resource == null)
                in_reader.JumpAhead(sizeof(uint) * 8);

            return resource;
        }
    }
}
