using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureMovementOverride
    {
        public uint SpawnId { get; set; }
        public byte Ground { get; set; }
        public byte Swim { get; set; }
        public byte Flight { get; set; }
        public byte Rooted { get; set; }
        public byte Chase { get; set; }
        public byte Random { get; set; }
    }
}
