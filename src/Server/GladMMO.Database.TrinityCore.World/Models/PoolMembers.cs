using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PoolMembers
    {
        public ushort Type { get; set; }
        public uint SpawnId { get; set; }
        public uint PoolSpawnId { get; set; }
        public double Chance { get; set; }
        public string Description { get; set; }
    }
}
