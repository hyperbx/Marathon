using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Services : KynapseElementList<Service>
    {
        public override string RootName => nameof(Services);

        public override string ItemName => nameof(Service);

        public Service this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Services() { }

        public Services(KynapseElement in_element) : base(in_element) { }

        public Services(XElement in_element) : base(in_element) { }
    }
}
