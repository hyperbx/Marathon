using System.Linq;
using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class ProfileEntityInfos : KynapseElementList<ProfileEntityInfo>
    {
        public override string RootName => "EntityInfos";

        public override string ItemName => "EntityInfo";

        public ProfileEntityInfo this[string in_name] => this.FirstOrDefault(x => x.Name == in_name);

        public ProfileEntityInfos() { }

        public ProfileEntityInfos(KynapseElement in_element) : base(in_element) { }

        public ProfileEntityInfos(XElement in_element) : base(in_element) { }
    }
}
