using System;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

// XISOExtractorGUI is licensed under The Unlicense:
/*
 * The Unlicense

 * This is free and unencumbered software released into the public domain.

 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.

 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.

 * For more information, please refer to <http://unlicense.org>
 */

namespace Toolkit.Tools
{
    static class EndianConverter
    {
        #region Byteswap Methods
        private static ushort Swap16(ushort input) { return (ushort)((input & 0xFF00U) >> 8 | (input & 0x00FFU) << 8); }

        private static uint Swap32(uint input) {
            return
                (input & 0x000000FFU) << 24 |
                (input & 0x0000FF00U) << 8 |
                (input & 0x00FF0000U) >> 8 |
                (input & 0xFF000000U) >> 24;
        }

        private static ulong Swap64(ulong input) {
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
        #endregion

        #region Little Endian
        internal static ushort Little16(ushort ret) { return !BitConverter.IsLittleEndian ? Swap16(ret) : ret; }

        internal static bool Little16(ref byte[] data, out ushort retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 2)
                return false;
            retval = BitConverter.ToUInt16(data, offset);
            retval = Little16(retval);
            return true;
        }

        internal static bool Little16(byte[] data, out ushort retval, int offset = 0) { return Little16(ref data, out retval, offset); }

        internal static uint Little32(uint ret) { return !BitConverter.IsLittleEndian ? Swap32(ret) : ret; }

        internal static bool Little32(ref byte[] data, out uint retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 4)
                return false;
            retval = BitConverter.ToUInt32(data, offset);
            retval = Little32(retval);
            return true;
        }

        internal static bool Little32(byte[] data, out uint retval, int offset = 0) { return Little32(ref data, out retval, offset); }

        internal static ulong Little64(ulong ret) { return !BitConverter.IsLittleEndian ? Swap64(ret) : ret; }

        internal static bool Little64(ref byte[] data, out ulong retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 8)
                return false;
            retval = BitConverter.ToUInt64(data, offset);
            retval = Little64(retval);
            return true;
        }
        #endregion

        #region Big Endian
        internal static ushort Big16(ushort ret) { return BitConverter.IsLittleEndian ? Swap16(ret) : ret; }

        internal static bool Big16(ref byte[] data, out ushort retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 2)
                return false;
            retval = BitConverter.ToUInt16(data, offset);
            retval = Big16(retval);
            return true;
        }

        internal static uint Big32(uint ret) { return BitConverter.IsLittleEndian ? Swap32(ret) : ret; }

        internal static bool Big32(ref byte[] data, out uint retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 4)
                return false;
            retval = BitConverter.ToUInt32(data, offset);
            retval = Big32(retval);
            return true;
        }

        internal static bool Big32(byte[] data, out uint retval, int offset = 0) { return Big32(ref data, out retval, offset); }

        internal static ulong Big64(ulong ret) { return BitConverter.IsLittleEndian ? Swap64(ret) : ret; }

        internal static bool Big64(ref byte[] data, out ulong retval, int offset = 0) {
            retval = 0;
            if (data.Length < offset + 8)
                return false;
            retval = BitConverter.ToUInt64(data, offset);
            retval = Big64(retval);
            return true;
        }

        internal static bool Big64(byte[] data, out ulong retval, int offset = 0) { return Big64(ref data, out retval, offset); }
        #endregion
    }
}