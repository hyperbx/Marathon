using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Acroarts.Types;
using Marathon.Helpers;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class _acmParticlePlay : IACM
    {
        public string Bank { get; set; }
        public string BankParticle { get; set; }
        public float Speed { get; set; }  // > 0 (Speed * 1.0/60.0)
        public uint Mode { get; set; }  // 0 - 1

        public _acmParticlePlay() { }

        public _acmParticlePlay(BINAReader in_reader, uint count)
        {
            Read(in_reader, count);
        }

        public void Read(BINAReader in_reader, uint count)
        {
            var DefaultPosition = 0x50;
            var Position = in_reader.Position;
            in_reader.ReadAtOffset(in_reader.Read<uint>() + DefaultPosition, () =>
            {
                Bank = in_reader.ReadString(StringBinaryFormat.NullTerminated);
            });

            in_reader.ReadAtOffset(in_reader.Read<uint>() + DefaultPosition, () =>
            {
                BankParticle = in_reader.ReadString(StringBinaryFormat.NullTerminated);
            });
                
            Speed = in_reader.Read<float>();
            Mode = in_reader.Read<uint>();

            Logger.Warning($"_acmParticlePlay at {Position:x} Count:{count} Bank:{Bank} Particle:{BankParticle} Speed {Speed} Mode {Mode}");
        }

        public void Write(BINAWriter in_writer)
        {
            // TODO
        }
    }
}
