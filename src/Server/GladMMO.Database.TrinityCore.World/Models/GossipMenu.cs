using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GossipMenu
    {
        public ushort MenuId { get; set; }
        public uint TextId { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
