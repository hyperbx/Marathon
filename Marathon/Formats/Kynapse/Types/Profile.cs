using Marathon.Extensions;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Profile : IKynapseElementSerializable
    {
        private const string _nameOfName = "name";

        public string Name { get; set; }

        public int MaxCount { get; set; }

        public ProfileEntityInfos EntityInfos { get; set; } = [];

        public Profile() { }

        public Profile(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Profile(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                switch (child.GetElementType())
                {
                    case KynapseElementType.Property:
                    {
                        if (child.Name != nameof(MaxCount))
                            continue;

                        MaxCount = int.Parse(child.Value);

                        break;
                    }

                    case KynapseElementType.Object:
                    {
                        if (child.Type != nameof(EntityInfos))
                            continue;

                        EntityInfos.FromKynapseElement(child);

                        break;
                    }
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Profile));

            result.AddChild(new KynapseElement(nameof(MaxCount), MaxCount));
            result.AddChild(EntityInfos.ToKynapseElement());

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(_nameOfName, Name);

            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(MaxCount):
                        MaxCount = element.GetElementValue(MaxCount);
                        break;

                    case nameof(EntityInfos):
                        EntityInfos.FromXElement(in_element);
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Profile));

            result.Add(new XAttribute(_nameOfName, Name));
            result.Add(new XElement(nameof(MaxCount), MaxCount));

            if (EntityInfos.Count > 0)
                result.Add(EntityInfos.ToXElement());

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
