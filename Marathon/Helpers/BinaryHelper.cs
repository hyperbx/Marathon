namespace Marathon.Helpers
{
    public class BinaryHelper
    {
        /// <summary>
        /// Converts byte length to a Windows-like suffix string.
        /// </summary>
        /// <param name="length">Byte length.</param>
        public static string ByteLengthToDecimalString(long length)
        {
            // Get absolute value.
            long absolute_i = length < 0 ? -length : length;

            // Determine the suffix and readable value.
            string suffix;
            double readable;

            // Exabyte
            if (absolute_i >= 0x1000000000000000)
            {
                suffix = "EB";
                readable = length >> 50;
            }

            // Petabyte
            else if (absolute_i >= 0x4000000000000)
            {
                suffix = "PB";
                readable = length >> 40;
            }

            // Terabyte
            else if (absolute_i >= 0x10000000000)
            {
                suffix = "TB";
                readable = length >> 30;
            }

            // Gigabyte
            else if (absolute_i >= 0x40000000)
            {
                suffix = "GB";
                readable = length >> 20;
            }

            // Megabyte
            else if (absolute_i >= 0x100000)
            {
                suffix = "MB";
                readable = length >> 10;
            }

            // Kilobyte
            else if (absolute_i >= 0x400)
            {
                suffix = "KB";
                readable = length;
            }

            // Byte
            else
            {
                suffix = "KB";
                readable = length % 1024 >= 1 ? length + 1024 - length % 1024 : length - length % 1024;
            }

            // Divide by 1024 to get fractional value.
            readable /= 1024;

            // Return formatted number with suffix.
            return $"{readable:0} {suffix}";
        }

        /// <summary>
        /// Converts byte length to a rounded up variant.
        /// </summary>
        /// <param name="length">Byte length.</param>
        public static double ByteLengthToDecimal(long length, DataLengthType lengthType)
        {
            double readable = 0;

            switch (lengthType)
            {
                case DataLengthType.B:
                    readable = length % 1024 >= 1 ? length + 1024 - length % 1024 : length - length % 1024;
                    break;

                case DataLengthType.KB:
                    readable = length;
                    break;

                case DataLengthType.MB:
                    readable = length >> 10;
                    break;

                case DataLengthType.GB:
                    readable = length >> 20;
                    break;

                case DataLengthType.TB:
                    readable = length >> 30;
                    break;

                case DataLengthType.PB:
                    readable = length >> 40;
                    break;

                case DataLengthType.EB:
                    readable = length >> 50;
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
