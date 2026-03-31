using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Entity : IKynapseElementSerializable
    {
        public string Name { get; set; }

        public string Class { get; set; }

        public KynapseElementLeaves Properties { get; set; } = [];

        public Entity() { }

        public Entity(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Entity(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Leaf)
                    continue;

                if (child.Name == nameof(Class))
                {
                    Class = child.Value;
                }
                else
                {
                    Properties.Add(new KynapseElementLeaf(child.Name, child.Value));
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Entity));

            result.AddChild(new KynapseElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);

            foreach (var element in in_element.Elements())
            {
                if (element.Name.ToString() == nameof(Class))
                {
                    Class = element.GetElementValue(Class);
                }
                else
                {
                    Properties.Add(new KynapseElementLeaf(element.Name.ToString(), element.Value));
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Entity));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(Class), Class));

            foreach (var property in Properties)
                result.Add(new XElement(property.Name, property.Value));

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
