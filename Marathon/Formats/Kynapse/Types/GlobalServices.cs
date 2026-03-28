using Marathon.Extensions;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class GlobalServices : IKynapseElementSerializable
    {
        private const string _nameOfServices = "Service";

        public List<string> Services { get; set; } = [];

        public GlobalServices() { }

        public GlobalServices(KynapseElement in_element)
        {
            FromKynapseElement(in_element);
        }

        public GlobalServices(XElement in_element)
        {
            FromXElement(in_element);
        }

        public void FromKynapseElement(KynapseElement in_element)
        {
            foreach (var child in in_element.Children)
            {
                if (child.GetElementType() != KynapseElementType.Property || child.Name != _nameOfServices)
                    continue;

                Services.Add(child.Value);
            }
        }

        public KynapseElement ToKynapseElement()
        {
            if (Services.Count <= 0)
                return null;

            var result = new KynapseElement(string.Empty, nameof(GlobalServices));

            foreach (var service in Services)
                result.AddChild(new KynapseElement(_nameOfServices, service));

            return result;
        }

        public void FromXElement(XElement in_element)
        {
            var globalServices = in_element.Element(nameof(GlobalServices));

            if (globalServices == null)
                return;

            foreach (var element in globalServices.Descendants())
            {
                if (element.Name != _nameOfServices)
                    continue;

                Services.Add(element.GetElementValue<string>(_nameOfServices));
            }
        }

        public XElement ToXElement()
        {
            if (Services.Count <= 0)
                return null;

            var result = new XElement(nameof(GlobalServices));

            foreach (var service in Services)
                result.Add(new XElement(_nameOfServices, service));

            return result;
        }
    }
}
