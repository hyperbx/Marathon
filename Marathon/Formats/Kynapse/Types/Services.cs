using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Services : KynapseElementList<Service>
    {
        public override string RootName => nameof(Services);

        public override string ItemName => nameof(Service);

        public Services() { }

        public Services(KynapseElement in_element) : base(in_element) { }

        public Services(XElement in_element) : base(in_element) { }
    }
}
