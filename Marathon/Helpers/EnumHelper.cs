using System;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<string, object> ToDictionary<T>(Type in_valueType) where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(x => x.ToString(), x => Convert.ChangeType(x, in_valueType));
        }

        public static Dictionary<string, TValue> ToDictionary<TEnum, TValue>() where TEnum : Enum
        {
            return ToDictionary<TEnum>(typeof(TValue)).ToDictionary(x => x.Key, x => (TValue)x.Value);
        }

        public static Dictionary<string, object> ToDictionary<T>() where T : Enum
        {
            return ToDictionary<T>(Enum.GetUnderlyingType(typeof(T)));
        }
    }
}
