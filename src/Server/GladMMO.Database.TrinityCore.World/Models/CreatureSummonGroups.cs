using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureSummonGroups
    {
        public uint SummonerId { get; set; }
        public byte SummonerType { get; set; }
        public byte GroupId { get; set; }
        public uint Entry { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public byte SummonType { get; set; }
        public uint SummonTime { get; set; }
    }
}
