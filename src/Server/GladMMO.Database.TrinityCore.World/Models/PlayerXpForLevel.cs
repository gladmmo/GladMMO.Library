using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PlayerXpForLevel
    {
        public byte Level { get; set; }
        public uint Experience { get; set; }
    }
}
