using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class Gameobject
    {
        public uint Guid { get; set; }
        public uint Id { get; set; }
        public ushort Map { get; set; }
        public ushort ZoneId { get; set; }
        public ushort AreaId { get; set; }
        public byte SpawnMask { get; set; }
        public uint PhaseMask { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public float Rotation0 { get; set; }
        public float Rotation1 { get; set; }
        public float Rotation2 { get; set; }
        public float Rotation3 { get; set; }
        public int Spawntimesecs { get; set; }
        public byte Animprogress { get; set; }
        public byte State { get; set; }
        public string ScriptName { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
