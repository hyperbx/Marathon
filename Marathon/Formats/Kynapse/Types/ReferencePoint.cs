using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class ReferencePoint : IKynapseElementSerializable
    {
        public float Right { get; set; }

        public float Up { get; set; }

        public float Front { get; set; }

        public ReferencePoint() { }

        public ReferencePoint(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public ReferencePoint(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Property)
                    continue;

                switch (child.Name)
                {
                    case nameof(Right):
                        Right = float.Parse(child.Value.TrimEnd('f'));
                        break;

                    case nameof(Up):
                        Up = float.Parse(child.Value.TrimEnd('f'));
                        break;

                    case nameof(Front):
                        Front = float.Parse(child.Value.TrimEnd('f'));
                        break;
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, nameof(ReferencePoint));

            result.AddChild(new KynapseElement(nameof(Right), Right));
            result.AddChild(new KynapseElement(nameof(Up), Up));
            result.AddChild(new KynapseElement(nameof(Front), Front));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Right = in_element.GetDescendantElementValue(nameof(Right), Right);
            Up = in_element.GetDescendantElementValue(nameof(Up), Up);
            Front = in_element.GetDescendantElementValue(nameof(Front), Front);
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(ReferencePoint));

            result.Add(new XElement(nameof(Right), Right));
            result.Add(new XElement(nameof(Up), Up));
            result.Add(new XElement(nameof(Front), Front));

            return result;
        }
    }
}
