using System.Xml.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class Traversal : KynapseElementClassPropertyList
    {
        public Traversal() { }

        public Traversal(KynapseElement in_element) : base(in_element) { }

        public Traversal(XElement in_element) : base(in_element) { }

        public override string GetRootName()
        {
            return nameof(Traversal);
        }

        public override string GetNameAttribute()
        {
            return "name";
        }
    }
}
