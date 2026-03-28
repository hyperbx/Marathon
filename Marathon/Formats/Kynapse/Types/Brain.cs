using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Brain : IKynapseElementSerializable
    {
        private const string _nameOfAgents = "Agent";

        public string Name { get; set; }

        public string Class { get; set; }

        public string Service { get; set; }

        public List<string> Agents { get; set; } = [];

        public List<(string Name, object Value)> Properties { get; set; } = [];

        public Brain() { }

        public Brain(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public Brain(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            Name = in_element.Name;

            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Property)
                    continue;

                switch (child.Name)
                {
                    case nameof(Class):
                        Class = child.Value;
                        break;

                    case nameof(Service):
                        Service = child.Value;
                        break;

                    case _nameOfAgents:
                        Agents.Add(child.Value);
                        break;

                    default:
                        Properties.Add((child.Name, child.Value));
                        break;
                }
            }
        }

        public KynapseElement ToKynapseElement()
        {
            var result = new KynapseElement(Name, nameof(Brain));

            result.AddChild(new KynapseElement(nameof(Class), Class));
            result.AddChild(new KynapseElement(nameof(Service), Service));

            foreach (var agent in Agents)
                result.AddChild(new KynapseElement(_nameOfAgents, agent));

            foreach (var property in Properties)
                result.AddChild(new KynapseElement(property.Name, property.Value));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            Name = in_element.GetAttributeValue(nameof(Name), Name);

            foreach (var element in in_element.Elements())
            {
                switch (element.Name.ToString())
                {
                    case nameof(Class):
                        Class = element.GetElementValue(Class);
                        break;

                    case nameof(Service):
                        Service = element.GetElementValue(Service);
                        break;

                    case _nameOfAgents:
                        Agents.Add(element.GetElementValue<string>());
                        break;

                    default:
                        Properties.Add((element.Name.ToString(), element.Value));
                        break;
                }
            }
        }

        public XElement ToXElement()
        {
            var result = new XElement(nameof(Brain));

            result.Add(new XAttribute(nameof(Name), Name));
            result.Add(new XElement(nameof(Class), Class));
            result.Add(new XElement(nameof(Service), Service));

            foreach (var agent in Agents)
                result.Add(new XElement(_nameOfAgents, agent));

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
