using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper, GordinRamsay

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for Common.bin files; used for configuring physics object parameters.
    /// </summary>
    public class ObjectPhysicsParameterList : FileBase
    {
        public ObjectPhysicsParameterList() { }

        public ObjectPhysicsParameterList(string in_path) : base(in_path) { }

        public List<ObjectPhysicsParameter> Parameters { get; set; } = [];

        public ObjectPhysicsParameter this[string in_name]
        {
            get => Parameters.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            var pos = reader.Position;
            var stringPoolOffset = reader.ReadUInt32();

            reader.JumpTo(pos);

            while (reader.Position < stringPoolOffset)
            {
                var param = new ObjectPhysicsParameter();

                var nameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => param.Name = reader.ReadStringNullTerminated());

                var modelOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + modelOffset,
                    () => param.Model = reader.ReadStringNullTerminated());

                var havokOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + havokOffset,
                    () => param.Havok = reader.ReadStringNullTerminated());

                var timeEventOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + timeEventOffset,
                    () => param.TimeEvent = reader.ReadStringNullTerminated());

                var materialAnimationOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + materialAnimationOffset,
                    () => param.MaterialAnimation = reader.ReadStringNullTerminated());

                var luaOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + luaOffset,
                    () => param.Lua = reader.ReadStringNullTerminated());

                param.Type = reader.Read<ObjectPhysicsType>();

                var psiGrabNodeOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + psiGrabNodeOffset,
                    () => param.PsiGrabNode = reader.ReadStringNullTerminated());

                param.CollisionType = reader.Read<ObjectPhysicsCollisionType>();
                param.GravityType = reader.Read<ObjectPhysicsGravityType>();
                param.DebrisType = reader.Read<ObjectPhysicsDebrisType>();
                param.EnemyDamage = reader.Read<uint>();
                param.UnknownField3 = reader.Read<float>();
                param.UnknownField4 = reader.Read<uint>();
                param.HomingPriority = reader.Read<int>();
                param.Health = reader.Read<uint>();
                param.LifeTime = reader.Read<float>();
                param.LifeTimeRandom = reader.Read<float>();
                param.Score = reader.Read<uint>();

                var destroyObjectNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + destroyObjectNameOffset,
                    () => param.OnDestroy = reader.ReadStringNullTerminated());

                var explosionNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + explosionNameOffset,
                    () => param.ExplosionName = reader.ReadStringNullTerminated());

                var particleContainerOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleContainerOffset,
                    () => param.ParticleContainer = reader.ReadStringNullTerminated());

                var particleNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + particleNameOffset,
                    () => param.ParticleName = reader.ReadStringNullTerminated());

                var soundBankOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundBankOffset,
                    () => param.SoundBank = reader.ReadStringNullTerminated());

                var soundNameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + soundNameOffset,
                    () => param.SoundName = reader.ReadStringNullTerminated());

                param.PsiGrabBehaviour = reader.Read<ObjectPhysicsPsiGrabBehaviour>();

                Parameters.Add(param);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            for (int i = 0; i < Parameters.Count; i++)
            {
                writer.CreateStringField($"Param{i}Name", Parameters[i].Name);
                writer.CreateStringField($"Param{i}Model", Parameters[i].Model);
                writer.CreateStringField($"Param{i}Havok", Parameters[i].Havok);
                writer.CreateStringField($"Param{i}TimeEvent", Parameters[i].TimeEvent);
                writer.CreateStringField($"Param{i}MaterialAnimation", Parameters[i].MaterialAnimation);
                writer.CreateStringField($"Param{i}Lua", Parameters[i].Lua);
                writer.Write(Parameters[i].Type);
                writer.CreateStringField($"Param{i}PsiGrabNode", Parameters[i].PsiGrabNode);
                writer.Write(Parameters[i].CollisionType);
                writer.Write(Parameters[i].GravityType);
                writer.Write(Parameters[i].DebrisType);
                writer.Write(Parameters[i].EnemyDamage);
                writer.Write(Parameters[i].UnknownField3);
                writer.Write(Parameters[i].UnknownField4);
                writer.Write(Parameters[i].HomingPriority);
                writer.Write(Parameters[i].Health);
                writer.Write(Parameters[i].LifeTime);
                writer.Write(Parameters[i].LifeTimeRandom);
                writer.Write(Parameters[i].Score);
                writer.CreateStringField($"Param{i}DestroyObjectName", Parameters[i].OnDestroy);
                writer.CreateStringField($"Param{i}ExplosionName", Parameters[i].ExplosionName);
                writer.CreateStringField($"Param{i}ParticleContainer", Parameters[i].ParticleContainer);
                writer.CreateStringField($"Param{i}ParticleName", Parameters[i].ParticleName);
                writer.CreateStringField($"Param{i}SoundBank", Parameters[i].SoundBank);
                writer.CreateStringField($"Param{i}SoundName", Parameters[i].SoundName);
                writer.Write(Parameters[i].PsiGrabBehaviour);
            }

            writer.Write(0);
            writer.FinishWrite();
        }
    }

    public class ObjectPhysicsParameter
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location of the model file for this object.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The location of the Havok data file for this object.
        /// </summary>
        public string Havok { get; set; }

        /// <summary>
        /// The location of the time event for this object.
        /// </summary>
        public string TimeEvent { get; set; }

        /// <summary>
        /// The location of the material animation file for this object.
        /// </summary>
        public string MaterialAnimation { get; set; }

        /// <summary>
        /// The location of the Lua script for this object.
        /// </summary>
        public string Lua { get; set; }

        /// <summary>
        /// The type of object this is.
        /// </summary>
        public ObjectPhysicsType Type { get; set; }

        /// <summary>
        /// The name of the node to grab using Silver's psychokinesis.
        /// </summary>
        public string PsiGrabNode { get; set; }

        /// <summary>
        /// The collision type this object uses for the sound effects and behaviour.
        /// </summary>
        public ObjectPhysicsCollisionType CollisionType { get; set; }

        /// <summary>
        /// The behaviour of this object's gravity upon spawning.
        /// </summary>
        public ObjectPhysicsGravityType GravityType { get; set; }

        /// <summary>
        /// The behaviour of this object's debris.
        /// </summary>
        public ObjectPhysicsDebrisType DebrisType { get; set; }

        /// <summary>
        /// The damage this object does to enemies when colliding with them by force.
        /// </summary>
        public uint EnemyDamage { get; set; }
        
        /// <summary>
        /// TODO: unknown - if zero, destroy object on spawn.
        /// </summary>
        public float UnknownField3 { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField4 { get; set; }

        /// <summary>
        /// The priority this object has for the homing attack.
        /// <para>If -1, this object will not be targetable. Otherwise, 15 at minimum will allow it to be targeted.</para>
        /// </summary>
        public int HomingPriority { get; set; }

        /// <summary>
        /// The amount of health this object has.
        /// </summary>
        public uint Health { get; set; }

        /// <summary>
        /// The amount of time this object persists for.
        /// </summary>
        public float LifeTime { get; set; }

        /// <summary>
        /// A modifier for <see cref="LifeTime"/> to add randomness to it.
        /// </summary>
        public float LifeTimeRandom { get; set; }

        /// <summary>
        /// The score rewarded for destroying this object.
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// The name of the object spawned upon destroying this object.
        /// </summary>
        public string OnDestroy { get; set; }

        /// <summary>
        /// The name of the explosion used when this object is destroyed.
        /// </summary>
        public string ExplosionName { get; set; }

        /// <summary>
        /// The location of the particle container for this object.
        /// </summary>
        public string ParticleContainer { get; set; }

        /// <summary>
        /// The name of the particle used when this object is destroyed.
        /// </summary>
        public string ParticleName { get; set; }

        /// <summary>
        /// The location of the sound bank for this object.
        /// </summary>
        public string SoundBank { get; set; }

        /// <summary>
        /// The name of the sound used when this object is destroyed.
        /// </summary>
        public string SoundName { get; set; }

        /// <summary>
        /// The behaviour for Silver's psychokinesis grab for this object.
        /// </summary>
        public ObjectPhysicsPsiGrabBehaviour PsiGrabBehaviour { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectPhysicsType : uint
    {
        Single,
        Multi,
        Animation,
        Ragdoll,
        Multi2,
        Barricade,
        Container,
        CerberusStatue,
        FallingFoothold,
        BossStageEwy,
        ThirdIblisFoothold,
        FootingTarget,
        BrokenTower
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectPhysicsCollisionType : uint
    {
        Stone,
        Water,
        Wood,
        Metal,
        Stone2,
        Grass,
        Dirt,
        Stone3,
        Snow,
        Sand,
        Glass,
        Stone4,
        Wet,
        Lava,
        ThinMetal,
        HeavyWeight = 22,
        LightWeight,
        MediumWeight,
        SparkImpact = 30,
        NoClipTerrain = 32,
        NoFriction = 39
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectPhysicsGravityType : uint
    {
        Normal,
        FixedUntilHit,
        Fixed = 3,
        FixedCancelHoming
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectPhysicsDebrisType : uint
    {
        None,
        PlayerCollide,
        PlayerCollideNoSelf,
        NoPlayerCollide
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectPhysicsPsiGrabBehaviour : uint
    {
        None,
        Hold,
        Pendulum,
        Projectile
    }
}