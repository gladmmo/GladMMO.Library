using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureFormations
    {
        public uint LeaderGuid { get; set; }
        public uint MemberGuid { get; set; }
        public float Dist { get; set; }
        public float Angle { get; set; }
        public uint GroupAi { get; set; }
        public ushort Point1 { get; set; }
        public ushort Point2 { get; set; }
    }
}
