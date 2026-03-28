using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Profiles : KynapseElementList<Profile>
    {
        public override string RootName => nameof(Profiles);

        public override string ItemName => nameof(Profile);

        public Profiles() { }

        public Profiles(KynapseElement in_element) : base(in_element) { }

        public Profiles(XElement in_element) : base(in_element) { }
    }
}
