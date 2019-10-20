using System;

namespace XISOExtractorGUI
{

    static class EndianConverter
    {
        #region Byteswap Methods

        private static ushort Swap16(ushort input)
        {
            return (ushort)((input & 0xFF00U) >> 8 | (input & 0x00FFU) << 8);
        }

        private static uint Swap32(uint input)
        {
            return
                (input & 0x000000FFU) << 24 |
                (input & 0x0000FF00U) << 8 |
                (input & 0x00FF0000U) >> 8 |
                (input & 0xFF000000U) >> 24;
        }

        private static ulong Swap64(ulong input)
        {
            return
                (input & 0x00000000000000FFU) >> 56 |
                (input & 0x000000000000FF00U) >> 40 |
                (input & 0x0000000000FF0000U) >> 24 |
                (input & 0x00000000FF000000U) >> 8 |
                (input & 0x000000FF00000000U) << 8 |
                (input & 0x0000FF0000000000U) << 24 |
                (input & 0x00FF000000000000U) << 40 |
                (input & 0xFF00000000000000U) << 56;
        }

        #endregion Byteswap Methods

        #region Little Endian

        internal static ushort Little16(ushort ret)
        {
            return !BitConverter.IsLittleEndian ? Swap16(ret) : ret;
        }

        internal static bool Little16(ref byte[] data, out ushort retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 2)
                return false;
            retval = BitConverter.ToUInt16(data, offset);
            retval = Little16(retval);
            return true;
        }

        internal static bool Little16(byte[] data, out ushort retval, int offset = 0)
        {
            return Little16(ref data, out retval, offset);
        }

        internal static uint Little32(uint ret)
        {
            return !BitConverter.IsLittleEndian ? Swap32(ret) : ret;
        }

        internal static bool Little32(ref byte[] data, out uint retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 4)
                return false;
            retval = BitConverter.ToUInt32(data, offset);
            retval = Little32(retval);
            return true;
        }

        internal static bool Little32(byte[] data, out uint retval, int offset = 0)
        {
            return Little32(ref data, out retval, offset);
        }

        internal static ulong Little64(ulong ret)
        {
            return !BitConverter.IsLittleEndian ? Swap64(ret) : ret;
        }

        internal static bool Little64(ref byte[] data, out ulong retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 8)
                return false;
            retval = BitConverter.ToUInt64(data, offset);
            retval = Little64(retval);
            return true;
        }

        #endregion Little Endian

        #region Big Endian

        internal static ushort Big16(ushort ret)
        {
            return BitConverter.IsLittleEndian ? Swap16(ret) : ret;
        }

        internal static bool Big16(ref byte[] data, out ushort retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 2)
                return false;
            retval = BitConverter.ToUInt16(data, offset);
            retval = Big16(retval);
            return true;
        }

        internal static uint Big32(uint ret)
        {
            return BitConverter.IsLittleEndian ? Swap32(ret) : ret;
        }

        internal static bool Big32(ref byte[] data, out uint retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 4)
                return false;
            retval = BitConverter.ToUInt32(data, offset);
            retval = Big32(retval);
            return true;
        }

        internal static bool Big32(byte[] data, out uint retval, int offset = 0)
        {
            return Big32(ref data, out retval, offset);
        }

        internal static ulong Big64(ulong ret)
        {
            return BitConverter.IsLittleEndian ? Swap64(ret) : ret;
        }

        internal static bool Big64(ref byte[] data, out ulong retval, int offset = 0)
        {
            retval = 0;
            if (data.Length < offset + 8)
                return false;
            retval = BitConverter.ToUInt64(data, offset);
            retval = Big64(retval);
            return true;
        }

        internal static bool Big64(byte[] data, out ulong retval, int offset = 0)
        {
            return Big64(ref data, out retval, offset);
        }

        #endregion Big Endian

    }
}