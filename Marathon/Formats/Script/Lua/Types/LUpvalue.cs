namespace Marathon.Formats.Script.Lua.Types
{
    public class LUpvalue : BObject
    {
        public bool InStack;
        public int Index;
        public string Name;

        public override bool Equals(object obj)
        {
            if (obj is LUpvalue lUpvalue)
            {
                if (!(InStack == lUpvalue.InStack && Index == lUpvalue.Index))
                    return false;

                if (Name == lUpvalue.Name)
                    return true;

                return Name != null && Name.Equals(lUpvalue.Name);
            }
            else
            {
                return false;
            }
        }
    }
}
