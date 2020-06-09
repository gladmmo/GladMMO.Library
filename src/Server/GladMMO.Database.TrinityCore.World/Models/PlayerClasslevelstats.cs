using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PlayerClasslevelstats
    {
        public byte Class { get; set; }
        public byte Level { get; set; }
        public ushort Basehp { get; set; }
        public ushort Basemana { get; set; }
    }
}
