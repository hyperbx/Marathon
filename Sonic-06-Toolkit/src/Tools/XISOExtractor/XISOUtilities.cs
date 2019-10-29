using System;
using System.IO;

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
    static class Utilities
    {
        public static uint GetSmallest(uint val1, uint val2) { return val1 < val2 ? val1 : val2; }

        public static long GetSmallest(long val1, long val2) { return val1 < val2 ? val1 : val2; }

        public static string GetSizeReadable(long i) {
            if (i >= 0x1000000000000000) // Exabyte
                return string.Format("{0:0.##} EB", (double)(i >> 50) / 1024);
            if (i >= 0x4000000000000) // Petabyte
                return string.Format("{0:0.##} PB", (double)(i >> 40) / 1024);
            if (i >= 0x10000000000) // Terabyte
                return string.Format("{0:0.##} TB", (double)(i >> 30) / 1024);
            if (i >= 0x40000000) // Gigabyte
                return string.Format("{0:0.##} GB", (double)(i >> 20) / 1024);
            if (i >= 0x100000) // Megabyte
                return string.Format("{0:0.##} MB", (double)(i >> 10) / 1024);
            return i >= 0x400 ? string.Format("{0:0.##} KB", (double)i / 1024) : string.Format("{0} B", i);
        }

        public static long GetTotalFreeSpace(string path) {
            foreach (var drive in DriveInfo.GetDrives())
                if (drive.IsReady && drive.RootDirectory.FullName.Equals(Path.GetPathRoot(path), StringComparison.CurrentCultureIgnoreCase))
                    return drive.TotalFreeSpace;
            return -1;
        }

        public static double GetPercentage(long current, long max) { return ((double)current / max) * 100; }
    }
}