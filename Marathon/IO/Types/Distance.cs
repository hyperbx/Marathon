namespace Marathon.IO.Types
{
    public struct Distance<T>
    {
        public T Min;
        public T Max;

        public Distance() { }

        public Distance(T in_min, T in_max)
        {
            Min = in_min;
            Max = in_max;
        }

        public override string ToString()
        {
            return $"<{Min}, {Max}>";
        }
    }
}
