using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellRanks
    {
        public uint FirstSpellId { get; set; }
        public uint SpellId { get; set; }
        public byte Rank { get; set; }
    }
}
