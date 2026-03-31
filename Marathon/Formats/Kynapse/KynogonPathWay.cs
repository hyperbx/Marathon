using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Extensions;
using Marathon.Formats.Kynapse.Types;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

// Format names:        Kynogon Path Way
// Format references:   Kaim::CPathWay
// Format designers:    Kynogon
// Format researchers:  Hyper
//
// Format research references:
// - Various Kynogon Path Ways' internal comments for the file extension.
// - Sacred 2: Fallen Angel for Kynapse symbols.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.pwl files; used for Kynapse paths.
    /// </summary>
    [FileType("Kynogon Path Way", "Kynapse", _extension)]
    public class KynogonPathWay : FileBase
    {
        private const string _extension = ".pwl"; // "Path Way List" (speculatory)
        private const string _signature = "Kynogon Path Way";
        private const int _version = 2;
        private const int _maxCommentLength = 255;

        private byte _seekCheckValue = 1;

        public string Comment { get; set; }

        public KynogonPathWayAction Action { get; set; }

        public List<Waypoint> Waypoints { get; set; } = [];

        public override string Extension => _extension;

        public KynogonPathWay() { }

        public KynogonPathWay(string in_path) : base(in_path) { }

        public KynogonPathWay(Stream in_stream) : base(in_stream) { }

        public KynogonPathWay(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            Comment = KynapseString.Read(reader, _maxCommentLength);
            _seekCheckValue = reader.Read<byte>();
            Action = (KynogonPathWayAction)reader.Read<byte>();

            while (reader.Position < reader.Length)
            {
                Waypoints.Add(reader.ReadObjectEx<Waypoint>());

                if (reader.Read<byte>() != 1)
                    break;
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);
            KynapseString.Write(writer, Comment, _maxCommentLength);
            writer.Write(_seekCheckValue);
            writer.Write((byte)Action);

            if (Waypoints.Count <= 0)
                return;

            for (int i = 0; i < Waypoints.Count; i++)
            {
                Waypoints[i].Write(writer);

                if (i < Waypoints.Count - 1)
                    writer.Write<byte>(1);
            }

            writer.WriteZero<byte>();
        }

        public override void Import(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            using var sr = new StreamReader(in_path);

            var waypoint = new Waypoint();
            var script = new StringBuilder();
            var isReadingScript = false;

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();

                if (line.StartsWith('#'))
                {
                    var comment = line[1..].TrimStart();

                    if (isReadingScript)
                    {
                        script.AppendLine(comment);
                    }
                    else
                    {
                        var value = line[line.LastIndexOf(' ')..].TrimStart();

                        if (comment.StartsWith("Comment"))
                        {
                            Comment = value;
                        }
                        else if (comment.StartsWith("Action"))
                        {
                            if (Enum.TryParse<KynogonPathWayAction>(value, false, out var out_action))
                                Action = out_action;
                        }
                        else if (comment.StartsWith("Script"))
                        {
                            isReadingScript = true;
                        }
                    }
                }
                else
                {
                    isReadingScript = false;

                    if (line.StartsWith('v'))
                    {
                        waypoint.Script = script.ToString();

                        var values = line[1..].TrimStart().Split([' '], StringSplitOptions.RemoveEmptyEntries);

                        if (values.Length > 3 || values.Length <= 0)
                            throw new InvalidDataException("Invalid OBJ vector.");

                        if (VectorExtensions.TryParseVector3(values[0], values[1], values[2], out var out_position))
                            waypoint.Position = out_position;

                        Waypoints.Add(waypoint);

                        waypoint = new();
                        script.Clear();
                    }
                }
            }
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.TruncateAllExtensions(path));

            using var sw = new StreamWriter($"{in_path}.obj");

            sw.WriteLine("# Kynogon Path Way");
            sw.WriteLine($"# Comment: {Comment}");
            sw.WriteLine($"# Action: {Action}");
            sw.WriteLine();

            if (Waypoints.Count <= 0)
                return;

            var vertexIndex = 1;
            var line = "l ";

            foreach (var waypoint in Waypoints)
            {
                sw.WriteLine("# Script:");

                if (!string.IsNullOrEmpty(waypoint.Script))
                {
                    foreach (var scriptLine in waypoint.Script.SplitLineBreaks())
                        sw.WriteLine($"# {scriptLine}");
                }

                sw.WriteLine($"v {waypoint.Position.X} {waypoint.Position.Y} {waypoint.Position.Z}");
                sw.WriteLine();

                line += $"{vertexIndex++} ";
            }

            sw.WriteLine($"g {Path.GetFileName(in_path)}");
            sw.WriteLine(line);
        }
    }

    public class Waypoint : IBinarySerializableEx
    {
        private const int _maxScriptLength = 63;

        public Vector3 Position { get; set; }

        public string Script { get; set; }

        public Waypoint() { }

        public Waypoint(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Position = in_reader.Read<Vector3>();
            in_reader.JumpAhead(1);
            Script = KynapseString.Read(in_reader, _maxScriptLength);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Position);
            in_writer.Write<byte>(0x0E);
            KynapseString.Write(in_writer, Script, _maxScriptLength);
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }

    public enum KynogonPathWayAction
    {
        None,
        Walk,
        Run,
        Jump,
        Crouch
    }
}
