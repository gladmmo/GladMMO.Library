using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class Creature
    {
        public uint Guid { get; set; }
        public uint Id { get; set; }
        public ushort Map { get; set; }
        public ushort ZoneId { get; set; }
        public ushort AreaId { get; set; }
        public byte SpawnMask { get; set; }
        public uint PhaseMask { get; set; }
        public uint Modelid { get; set; }
        public sbyte EquipmentId { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public uint Spawntimesecs { get; set; }
        public float WanderDistance { get; set; }
        public uint Currentwaypoint { get; set; }
        public uint Curhealth { get; set; }
        public uint Curmana { get; set; }
        public byte MovementType { get; set; }
        public uint Npcflag { get; set; }
        public uint UnitFlags { get; set; }
        public uint Dynamicflags { get; set; }
        public string ScriptName { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
