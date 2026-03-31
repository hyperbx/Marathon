using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Periodic : IKynapseElementSerializable
    {
        public string Name { get; set; }

        public int Period { get; set; }

        public Periodic() { }

        public Periodic(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Periodic(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Leaf || child.Name != nameof(Period))
                    continue;

                Period = int.Parse(child.Value);

                break;
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Periodic));

            result.AddChild(new KynapseElement(nameof(Period), Period));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);
            Period = in_element.GetDescendantElementValue(nameof(Period), Period);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Periodic));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(Period), Period));

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
