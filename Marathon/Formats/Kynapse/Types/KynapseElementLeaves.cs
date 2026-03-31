using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElementLeaves : List<KynapseElementLeaf>
    {
        public object this[string in_name]
        {
            get => this.FirstOrDefault(x => x.Name == in_name)?.Value;

            set
            {
                foreach (var leaf in this)
                {
                    if (leaf.Name != in_name)
                        continue;

                    leaf.Value = value;

                    break;
                }
            }
        }
    }
}
