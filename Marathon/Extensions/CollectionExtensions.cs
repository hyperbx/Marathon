using System;
using System.Collections.Generic;

namespace Marathon.Extensions
{
    public static class CollectionExtensions
    {
        public static void CopyTo<T1, T2>(this IDictionary<T1, T2> in_dict, KeyValuePair<T1, T2>[] in_array, int in_arrayIndex)
        {
            if (in_dict.Count > in_array.Length - in_arrayIndex)
                throw new ArgumentException("The array is not large enough.");

            var i = 0;

            foreach (var pair in in_dict)
                in_array[i++ + in_arrayIndex] = pair;
        }
    }
}
