using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marathon.IO
{
    public class SignatureScanner
    {
        public static IEnumerable<long> ScanAll(Stream in_stream, byte[] in_pattern, string in_mask, long in_begin = 0, long in_length = 0, bool in_single = false)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);
            var patternLength = in_mask.Length;

            if (in_length == 0)
                in_length = in_stream.Length;

            var originalPos = in_stream.Position;

            for (long i = 0; i < in_length; i++)
            {
                var pos = in_begin + i;
                var patternSeek = 0;

                reader.JumpTo(pos);

                if (pos + patternLength > in_length)
                    break;

                for (patternSeek = 0; patternSeek < patternLength; patternSeek++)
                {
                    if (in_mask[patternSeek] != '?' && in_pattern[patternSeek] != reader.ReadValueAtOffset<byte>(pos + patternSeek))
                        break;
                }

                if (patternSeek == patternLength)
                {
                    if (in_single)
                    {
                        yield return pos;
                        break;
                    }
                    else
                    {
                        yield return pos;
                    }
                }
            }

            in_stream.Seek(originalPos, SeekOrigin.Begin);
        }

        public static IEnumerable<long> ScanAll(Stream in_stream, string in_wildcardPattern, long in_begin = 0, long in_length = 0, bool in_single = false)
        {
            var (pattern, mask) = TransformWildcardPatternToCodePattern(in_wildcardPattern);

            return ScanAll(in_stream, pattern, mask, in_begin, in_length, in_single);
        }

        public static long Scan(Stream in_stream, byte[] in_pattern, string in_mask, long in_begin = 0, long in_length = 0)
        {
            return ScanAll(in_stream, in_pattern, in_mask, in_begin, in_length, true).FirstOrDefault();
        }

        public static long Scan(Stream in_stream, string in_wildcardPattern, long in_begin = 0, long in_length = 0)
        {
            return ScanAll(in_stream, in_wildcardPattern, in_begin, in_length, true).FirstOrDefault();
        }

        public static (byte[] Pattern, string Mask) TransformWildcardPatternToCodePattern(string in_pattern)
        {
            var bytes = in_pattern.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            var pattern = new List<byte>();
            var mask = new StringBuilder();

            foreach (var b in bytes)
            {
                if (b == "?")
                {
                    pattern.Add(0xCC);
                    mask.Append("?");
                }
                else
                {
                    pattern.Add(Convert.ToByte(b, 16));
                    mask.Append("x");
                }
            }

            return (pattern.ToArray(), mask.ToString());
        }
    }
}
