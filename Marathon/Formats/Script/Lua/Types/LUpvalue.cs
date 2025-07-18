namespace Marathon.Formats.Script.Lua.Types
{
    public class LUpvalue : BObject
    {
        public string Name { get; set; }

        public bool IsInStack { get; set; }

        public int Index { get; set; }

        public override bool Equals(object in_obj)
        {
            if (in_obj is LUpvalue out_upvalue)
            {
                if (!(IsInStack == out_upvalue.IsInStack && Index == out_upvalue.Index))
                    return false;

                if (Name == out_upvalue.Name)
                    return true;

                return Name != null && Name.Equals(out_upvalue.Name);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
