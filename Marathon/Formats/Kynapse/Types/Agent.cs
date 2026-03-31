using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Agent : KynapseElementClass
    {
        public Agent() { }

        public Agent(KynapseElement in_element) : base(in_element) { }

        public Agent(XElement in_element) : base(in_element) { }

        public override string GetRootName()
        {
            return nameof(Agent);
        }
    }
}
