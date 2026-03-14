using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Marathon.Helpers
{
    public class BinaryHelper
    {
        /// <summary>
        /// Transforms a byte array to a hexadecimal string.
        /// </summary>
        /// <param name="in_buffer">The byte array to transform.</param>
        public static string TransformByteArrayToHexString(byte[] in_buffer)
        {
            var result = new StringBuilder();

            foreach (var b in in_buffer)
                result.Append($"{b:X2} ");

            return result.ToString();
        }

        /// <summary>
        /// Transforms a hexadecimal string (formatted "AA BB CC DD" or "AABBCCDD") to a byte array.
        /// </summary>
        /// <param name="in_hexStr">The hexadecimal string to transform.</param>
        public static byte[] TransformHexStringToByteArray(string in_hexStr)
        {
            // Remove any spaces in case the string is formatted as such.
            in_hexStr = in_hexStr.Replace(" ", "");

            return Enumerable.Range(0, in_hexStr.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(in_hexStr.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Transforms a byte array to a managed type.
        /// </summary>
        /// <typeparam name="T">The type to transform the byte array into.</typeparam>
        /// <param name="in_buffer">The byte array containing the type data.</param>
        public static T TransformByteArrayToManagedType<T>(byte[] in_buffer)
        {
            var handle = GCHandle.Alloc(in_buffer, GCHandleType.Pinned);

            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        /// <summary>
        /// Swaps the endianness of a given value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="in_data">The data to swap.</param>
        public static T SwapEndianness<T>(T in_data)
        {
            var bytes = new byte[Marshal.SizeOf<T>()];

            Marshal.StructureToPtr(in_data, Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0), false);
            Array.Reverse(bytes);

            return TransformByteArrayToManagedType<T>(bytes);
        }

        /// <summary>
        /// Prints a byte array to the console.
        /// </summary>
        /// <param name="in_data">The byte array to print.</param>
        /// <param name="in_baseAddr">The address to start the left-most column at.</param>
        public static void PrintBytes(byte[] in_data, uint in_baseAddr = 0)
        {
            var oldColour = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Address  ");

            // Print top row.
            for (int i = 0; i < 16; i++)
                Console.Write($"{(i + in_baseAddr % 16):X2} ");

            // Print top row for ASCII table.
            for (int i = 0; i < 16; i++)
                Console.Write($"{(((i + in_baseAddr) % 16 + 16) % 16):X}");

            Console.WriteLine();
            Console.ForegroundColor = oldColour;

            for (int i = 0; i < in_data.Length; i += 16)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{(in_baseAddr + i):X8} ");
                Console.ForegroundColor = oldColour;

                // Print bytes.
                for (int j = 0; j < 16; j++)
                {
                    int index = i + j;

                    if (index < in_data.Length)
                    {
                        Console.Write($"{in_data[index]:X2} ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("?? ");
                        Console.ForegroundColor = oldColour;
                    }
                }

                // Print ASCII table.
                for (int j = 0; j < 16; j++)
                {
                    int index = i + j;

                    if (index < in_data.Length)
                    {
                        char c = (char)in_data[index];
                        Console.Write(char.IsControl(c) ? '.' : c);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("?");
                        Console.ForegroundColor = oldColour;
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints a stream to the console.
        /// </summary>
        /// <param name="in_stream">The stream to print.</param>
        /// <param name="in_baseAddr">The address to start the left-most column at.</param>
        public static void PrintBytes(Stream in_stream, uint in_baseAddr = 0)
        {
            var pos = in_stream.Position;

            // TODO: this is inefficient, do main impl as
            // Stream instead of byte[] instead, then pass
            // byte[] as MemoryStream.
            using var memoryStream = new MemoryStream();
            {
                in_stream.Position = 0;
                in_stream.CopyTo(memoryStream);
                in_stream.Position = pos;
            }

            PrintBytes(memoryStream.ToArray(), in_baseAddr);
        }

        /// <summary>
        /// Transforms a length value into a Windows-like suffix string.
        /// </summary>
        /// <param name="in_length">The length to represent.</param>
        public static string TransformDataLengthToFormattedString(long in_length)
        {
            var abs = in_length < 0 ? -in_length : in_length;

            string suffix;
            double readable;

            // Exabyte
            if (abs >= 0x1000000000000000)
            {
                suffix = "EB";
                readable = in_length >> 50;
            }

            // Petabyte
            else if (abs >= 0x4000000000000)
            {
                suffix = "PB";
                readable = in_length >> 40;
            }

            // Terabyte
            else if (abs >= 0x10000000000)
            {
                suffix = "TB";
                readable = in_length >> 30;
            }

            // Gigabyte
            else if (abs >= 0x40000000)
            {
                suffix = "GB";
                readable = in_length >> 20;
            }

            // Megabyte
            else if (abs >= 0x100000)
            {
                suffix = "MB";
                readable = in_length >> 10;
            }

            // Kilobyte
            else if (abs >= 0x400)
            {
                suffix = "KB";
                readable = in_length;
            }

            // Byte
            else
            {
                suffix = "KB";
                readable = in_length % 1024 >= 1 ? in_length + 1024 - in_length % 1024 : in_length - in_length % 1024;
            }

            // Get fractional value.
            readable /= 1024;

            // Return formatted number with suffix.
            return $"{readable:0} {suffix}";
        }

        /// <summary>
        /// Gets a rounded data length.
        /// </summary>
        /// <param name="in_length">The length to represent.</param>
        public static double GetDataLengthRounded(long in_length, DataLengthType in_lengthType)
        {
            double readable = 0;

            switch (in_lengthType)
            {
                case DataLengthType.B:
                    readable = in_length % 1024 >= 1 ? in_length + 1024 - in_length % 1024 : in_length - in_length % 1024;
                    break;

                case DataLengthType.KB:
                    readable = in_length;
                    break;

                case DataLengthType.MB:
                    readable = in_length >> 10;
                    break;

                case DataLengthType.GB:
                    readable = in_length >> 20;
                    break;

                case DataLengthType.TB:
                    readable = in_length >> 30;
                    break;

                case DataLengthType.PB:
                    readable = in_length >> 40;
                    break;

                case DataLengthType.EB:
                    readable = in_length >> 50;
                    break;
            }

            return readable / 1024;
        }

        public enum DataLengthType
        {
            B,
            KB,
            MB,
            GB,
            TB,
            PB,
            EB
        }
    }
}
