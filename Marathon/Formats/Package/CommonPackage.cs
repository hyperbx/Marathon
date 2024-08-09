namespace Marathon.Formats.Package
{
    /// <summary>
    /// File base for the Common.bin format.
    /// <para>Used in SONIC THE HEDGEHOG for defining physics objects.</para>
    /// </summary>
    public class CommonPackage : FileBase
    {
        public CommonPackage() { }

        public CommonPackage(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Objects = JsonDeserialise<List<CommonObject>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Objects);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".bin";

        public List<CommonObject> Objects { get; set; } = [];

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the first string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            // Read stream until we've reached the end of the offset table.
            while (reader.BaseStream.Position < offsetTableLength)
            {
                CommonObject @object = new();

                // Read all the offsets.
                uint objectNameOffset          = reader.ReadUInt32();
                uint modelOffset               = reader.ReadUInt32();
                uint havokOffset               = reader.ReadUInt32();
                uint timeEventOffset           = reader.ReadUInt32();
                uint materialAnimationOffset   = reader.ReadUInt32();
                uint luaScriptOffset           = reader.ReadUInt32();
                @object.UnknownUInt32_1        = reader.ReadUInt32();
                uint unknownString1Offset      = reader.ReadUInt32();
                @object.CollisionType          = reader.ReadUInt32();
                @object.CollisionFlags         = reader.ReadUInt32();
                @object.Rigidity               = reader.ReadUInt32();
                @object.EnemyDamage            = reader.ReadUInt32();
                @object.UnknownSingle         = reader.ReadSingle();
                @object.Potency                = reader.ReadUInt32();
                @object.TargetFlags            = reader.ReadUInt32();
                @object.Health                 = reader.ReadUInt32();
                @object.DebrisLifetimeBase     = reader.ReadSingle();
                @object.DebrisLifetimeModifier = reader.ReadSingle();
                @object.UnknownUInt32_2        = reader.ReadUInt32();
                uint breakObjectOffset         = reader.ReadUInt32();
                uint explosionOffset           = reader.ReadUInt32();
                uint particleFileOffset        = reader.ReadUInt32();
                uint particleNameOffset        = reader.ReadUInt32();
                uint sceneBankOffset           = reader.ReadUInt32();
                uint soundNameOffset           = reader.ReadUInt32();
                @object.PsiBehaviour           = reader.ReadUInt32();

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values.
                @object.PropName          = reader.ReadNullTerminatedString(false, objectNameOffset, true);
                @object.ModelName         = reader.ReadNullTerminatedString(false, modelOffset, true);
                @object.HavokName         = reader.ReadNullTerminatedString(false, havokOffset, true);
                @object.TimeEvent         = reader.ReadNullTerminatedString(false, timeEventOffset, true);
                @object.MaterialAnimation = reader.ReadNullTerminatedString(false, materialAnimationOffset, true);
                @object.LuaScript         = reader.ReadNullTerminatedString(false, luaScriptOffset, true);
                @object.UnknownString   = reader.ReadNullTerminatedString(false, unknownString1Offset, true);
                @object.BreakObject       = reader.ReadNullTerminatedString(false, breakObjectOffset, true);
                @object.Explosion         = reader.ReadNullTerminatedString(false, explosionOffset, true);
                @object.ParticleFile      = reader.ReadNullTerminatedString(false, particleFileOffset, true);
                @object.ParticleName      = reader.ReadNullTerminatedString(false, particleNameOffset, true);
                @object.SoundBank         = reader.ReadNullTerminatedString(false, sceneBankOffset, true);
                @object.SoundName         = reader.ReadNullTerminatedString(false, soundNameOffset, true);

                // Jump back to the saved position to read the next object.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Objects.Add(@object);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            // Write the objects.
            for (int i = 0; i < Objects.Count; i++)
            {
                writer.AddString($"object{i}Name", Objects[i].PropName);
                writer.AddString($"object{i}ModelName", Objects[i].ModelName);
                writer.AddString($"object{i}HKXName", Objects[i].HavokName);
                writer.AddString($"object{i}TimeEvent", Objects[i].TimeEvent);
                writer.AddString($"object{i}MaterialAnimation", Objects[i].MaterialAnimation);
                writer.AddString($"object{i}LuaScript", Objects[i].LuaScript);
                writer.Write(Objects[i].UnknownUInt32_1);
                writer.AddString($"object{i}UnknownString1", Objects[i].UnknownString);
                writer.Write(Objects[i].CollisionType);
                writer.Write(Objects[i].CollisionFlags);
                writer.Write(Objects[i].Rigidity);
                writer.Write(Objects[i].EnemyDamage);
                writer.Write(Objects[i].UnknownSingle);
                writer.Write(Objects[i].Potency);
                writer.Write(Objects[i].TargetFlags);
                writer.Write(Objects[i].Health);
                writer.Write(Objects[i].DebrisLifetimeBase);
                writer.Write(Objects[i].DebrisLifetimeModifier);
                writer.Write(Objects[i].UnknownUInt32_2);
                writer.AddString($"object{i}BreakObject", Objects[i].BreakObject);
                writer.AddString($"object{i}Explosion", Objects[i].Explosion);
                writer.AddString($"object{i}ParticleFile", Objects[i].ParticleFile);
                writer.AddString($"object{i}ParticleName", Objects[i].ParticleName);
                writer.AddString($"object{i}SceneBank", Objects[i].SoundBank);
                writer.AddString($"object{i}SoundName", Objects[i].SoundName);
                writer.Write(Objects[i].PsiBehaviour);
            }

            // Write the footer.
            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }

    public class CommonObject
    {
        /// <summary>
        /// The name of the object.
        /// </summary>
        public string PropName { get; set; }

        /// <summary>
        /// The internal path to the model (*.xno) for the object.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// The internal path to the Havok data (*.hkx) for the object.
        /// </summary>
        public string HavokName { get; set; }

        /// <summary>
        /// The internal path to the <see cref="Event.TimeEvent"/> (*.tev) for the object.
        /// </summary>
        public string TimeEvent { get; set; }

        /// <summary>
        /// The internal path to the material animation (*.xnv) for the object.
        /// </summary>
        public string MaterialAnimation { get; set; }

        /// <summary>
        /// The internal path to the Lua script (*.lub) for the object.
        /// </summary>
        public string LuaScript { get; set; }

        /// <summary>
        /// TODO: Unknown - seems to screw with how the object breaks?
        /// </summary>
        public uint UnknownUInt32_1 { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public string UnknownString { get; set; }

        /// <summary>
        /// The type of collision this object uses globally for the sound effects and behaviour.
        /// </summary>
        public uint CollisionType { get; set; }

        /// <summary>
        /// The flags that determine how the player interacts with the object.
        /// </summary>
        public uint CollisionFlags { get; set; }

        /// <summary>
        /// The rigidity of the object.
        /// <para>0 = Standard</para>
        /// <para>1 = Colliable Debris</para>
        /// <para>2 = Colliable but non intersectable debries</para>
        /// <para>3 = Debris</para>
        /// </summary>
        public uint Rigidity { get; set; }

        /// <summary>
        /// The damage this object does when colliding with an enemy with applied force.
        /// </summary>
        public uint EnemyDamage { get; set; }

        /// <summary>
        /// TODO: Unknown - seems to be something regarding the object breaking from gravity?
        /// </summary>
        public float UnknownSingle { get; set; }

        /// <summary>
        /// The required force an attack needs to be able to damage this object.
        /// </summary>
        public uint Potency { get; set; }

        /// <summary>
        /// The flags that determine how the player targets the object.
        /// <para>15 = Homing Attack</para>
        /// </summary>
        public uint TargetFlags { get; set; }

        /// <summary>
        /// The amount of health this object has.
        /// </summary>
        public uint Health { get; set; }

        /// <summary>
        /// The amount of time this object lasts as debris.
        /// </summary>
        public float DebrisLifetimeBase { get; set; }

        /// <summary>
        /// A modifier to increase the time for debris to disappear to add variety.
        /// </summary>
        public float DebrisLifetimeModifier { get; set; }

        /// <summary>
        /// TODO: Unknown.
        /// </summary>
        public uint UnknownUInt32_2 { get; set; }

        /// <summary>
        /// The object spawned upon breaking this object.
        /// </summary>
        public string BreakObject { get; set; }

        /// <summary>
        /// The type of explosion stored in the <see cref="ExplosionPackage"/> format that'll be used when this object breaks.
        /// </summary>
        public string Explosion { get; set; }

        /// <summary>
        /// The internal path to the particle file used when this object breaks.
        /// </summary>
        public string ParticleFile { get; set; }

        /// <summary>
        /// The name of the particle in the particle file to use when this object breaks.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// The internal path to the <see cref="Audio.SoundBank"/> (*.sbk) used when this object breaks.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The name of the sound in the <see cref="Audio.SoundBank"/> to use when this object breaks.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// <para>0 = Can't be picked up</para>
        /// <para>1 = Normal Grab</para>
        /// <para>2 = Kingdom Valley Pendulum</para>
        /// <para>3 = Super Fast Projectile Launch</para>
        /// </summary>
        public uint PsiBehaviour { get; set; }

        public CommonObject() { }

        public override string ToString() => PropName;
    }
}