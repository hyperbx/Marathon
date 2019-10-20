using System;

namespace XISOExtractorGUI
{

    internal sealed class EventArg<T> : EventArgs
    {
        public readonly T Data;

        public EventArg(T data) { Data = data; }
    }

    internal sealed class EventArg<T1, T2> : EventArgs
    {
        public readonly T1 Data;
        public readonly T2 Data2;

        public EventArg(T1 data, T2 data2)
        {
            Data = data;
            Data2 = data2;
        }
    }

    internal sealed class EventArg<T1, T2, T3> : EventArgs
    {
        public readonly T1 Data;
        public readonly T2 Data2;
        public readonly T3 Data3;

        public EventArg(T1 data, T2 data2, T3 data3)
        {
            Data = data;
            Data2 = data2;
            Data3 = data3;
        }
    }
}