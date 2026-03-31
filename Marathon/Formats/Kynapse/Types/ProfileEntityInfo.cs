using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class ProfileEntityInfo : IKynapseElementSerializable
    {
        private const string _nameOfRoot = "EntityInfo";

        public string Name { get; set; }

        public List<string> ComputeWith { get; set; } = [];

        public List<string> SearchIn { get; set; } = [];

        public KynapseElementLeaves Properties { get; set; } = [];

        public ProfileEntityInfo() { }

        public ProfileEntityInfo(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public ProfileEntityInfo(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Leaf)
                    continue;

                switch (child.Name)
                {
                    case nameof(Name):
                        Name = child.Value;
                        break;

                    case nameof(ComputeWith):
                        ComputeWith.Add(child.Value);
                        break;

                    case nameof(SearchIn):
                        SearchIn.Add(child.Value);
                        break;

                    default:
                        Properties.Add(new KynapseElementLeaf(child.Name, child.Value));
                        break;
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(string.Empty, _nameOfRoot);

            result.AddChild(new KynapseElement(nameof(Name), Name));

            foreach (var computeWith in ComputeWith)
                result.AddChild(new KynapseElement(nameof(ComputeWith), computeWith));

            foreach (var searchIn in SearchIn)
                result.AddChild(new KynapseElement(nameof(SearchIn), searchIn));

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(Name):
                        Name = element.GetElementValue(Name);
                        break;

                    case nameof(ComputeWith):
                        ComputeWith.Add(element.GetElementValue<string>());
                        break;

                    case nameof(SearchIn):
                        SearchIn.Add(element.GetElementValue<string>());
                        break;

                    default:
                        Properties.Add(new KynapseElementLeaf(element.Name.ToString(), element.Value));
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(_nameOfRoot);

            if (!string.IsNullOrEmpty(Name))
                result.Add(new XElement(nameof(Name), Name));

            foreach (var computeWith in ComputeWith)
                result.Add(new XElement(nameof(ComputeWith), computeWith));

            foreach (var searchIn in SearchIn)
                result.Add(new XElement(nameof(SearchIn), searchIn));

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
