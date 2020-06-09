using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpawnGroup
    {
        public uint GroupId { get; set; }
        public byte SpawnType { get; set; }
        public uint SpawnId { get; set; }
    }
}
