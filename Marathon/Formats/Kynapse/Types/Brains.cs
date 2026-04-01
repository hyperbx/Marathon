using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Brains : KynapseElementList<Brain>
    {
        public override string RootName => nameof(Brains);

        public override string ItemName => nameof(Brain);

        public Brain this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Brains() { }

        public Brains(KynapseElement in_element) : base(in_element) { }

        public Brains(XElement in_element) : base(in_element) { }
    }
}
