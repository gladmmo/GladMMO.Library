using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class LfgDungeonTemplate
    {
        public uint DungeonId { get; set; }
        public string Name { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
