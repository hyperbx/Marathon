using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Brains : KynapseElementList<Brain>
    {
        public override string RootName => nameof(Brains);

        public override string ItemName => nameof(Brain);

        public Brains() { }

        public Brains(KynapseElement in_element) : base(in_element) { }

        public Brains(XElement in_element) : base(in_element) { }
    }
}
