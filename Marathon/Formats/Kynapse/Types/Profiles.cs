using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Profiles : KynapseElementList<Profile>
    {
        public override string RootName => nameof(Profiles);

        public override string ItemName => nameof(Profile);

        public Profile this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public Profiles() { }

        public Profiles(KynapseElement in_element) : base(in_element) { }

        public Profiles(XElement in_element) : base(in_element) { }
    }
}
