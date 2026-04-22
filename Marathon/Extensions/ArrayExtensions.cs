using Marathon.Helpers;
using System;

namespace Marathon.Extensions
{
    public static class ArrayExtensions
    {
        extension(Array)
        {
            /// <summary>
            /// Creates a two-dimensional jagged <see cref="Array"/> of the specified <see cref="Type"/> and length, with zero-based indexing.
            /// </summary>
            /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Array"/> to create.</typeparam>
            /// <param name="in_lengthA">The size of the left <see cref="Array"/> to create.</param>
            /// <param name="in_lengthB">The size of the right <see cref="Array"/> to create.</param>
            /// <returns>A new two-dimensional jagged <see cref="Array"/> of the specified <see cref="Type"/> with the specified length, using zero-based indexing.</returns>
            /// <exception cref="ArgumentNullException"/>
            /// <exception cref="ArgumentException"/>
            /// <exception cref="NotSupportedException"/>
            /// <exception cref="ArgumentOutOfRangeException"/>
            public static T[][] CreateInstanceJagged2D<T>(int in_lengthA, int in_lengthB = 0)
            {
                return (T[][])CreateInstanceJagged<T>([in_lengthA, in_lengthB]);
            }

            /// <summary>
            /// Creates a jagged <see cref="Array"/> of the specified <see cref="Type"/> and length, with zero-based indexing.
            /// </summary>
            /// <typeparam name="T">The <see cref="Type"/> of the <see cref="Array"/> to create.</typeparam>
            /// <param name="in_lengths">An array of 32-bit integers that represent the size of each dimension of the <see cref="Array"/> to create.</param>
            /// <returns>A new jagged <see cref="Array"/> of the specified <see cref="Type"/> with the specified length, using zero-based indexing.</returns>
            /// <exception cref="ArgumentNullException"/>
            /// <exception cref="ArgumentException"/>
            /// <exception cref="NotSupportedException"/>
            /// <exception cref="ArgumentOutOfRangeException"/>
            public static Array CreateInstanceJagged<T>(params int[] in_lengths)
            {
                return CreateInstanceJagged(typeof(T), in_lengths);
            }

            /// <summary>
            /// Creates a jagged <see cref="Array"/> of the specified <see cref="Type"/> and length, with zero-based indexing.
            /// </summary>
            /// <param name="in_elementType">The <see cref="Type"/> of the <see cref="Array"/> to create.</param>
            /// <param name="in_lengths">An array of 32-bit integers that represent the size of each dimension of the <see cref="Array"/> to create.</param>
            /// <returns>A new jagged <see cref="Array"/> of the specified <see cref="Type"/> with the specified length, using zero-based indexing.</returns>
            /// <exception cref="ArgumentNullException"/>
            /// <exception cref="ArgumentException"/>
            /// <exception cref="NotSupportedException"/>
            /// <exception cref="ArgumentOutOfRangeException"/>
            public static Array CreateInstanceJagged(Type in_elementType, int[] in_lengths)
            {
                return CreateInstanceJagged(in_elementType, in_lengths, 0);
            }

            private static Array CreateInstanceJagged(Type in_elementType, int[] in_lengths, int in_depth)
            {
                ThrowHelper.ThrowArgumentNullException(nameof(in_elementType), in_elementType);
                ThrowHelper.ThrowArgumentNullException(nameof(in_lengths), in_lengths);

                var depth = in_lengths.Length - in_depth - 1;

                var arrayElementType = depth == 0
                    ? in_elementType
                    : in_elementType.MakeArrayType();

                var result = Array.CreateInstance(arrayElementType, in_lengths[in_depth]);

                if (depth > 0)
                {
                    for (int i = 0; i < in_lengths[in_depth]; i++)
                        result.SetValue(CreateInstanceJagged(in_elementType, in_lengths, in_depth + 1), i);
                }

                return result;
            }
        }
    }
}
