namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseElementLeaf
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public KynapseElementLeaf() { }

        public KynapseElementLeaf(string in_name, object in_value)
        {
            Name = in_name;
            Value = in_value;
        }

        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
    }
}
