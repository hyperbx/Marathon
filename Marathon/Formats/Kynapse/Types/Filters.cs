using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Filters : KynapseElementList<Filter>
    {
        public override string RootName => nameof(Filters);

        public override string ItemName => nameof(Filter);

        public Filter this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Filters() { }

        public Filters(KynapseElement in_element) : base(in_element) { }

        public Filters(XElement in_element) : base(in_element) { }
    }
}
