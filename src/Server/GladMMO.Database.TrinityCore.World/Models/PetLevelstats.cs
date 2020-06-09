using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class PetLevelstats
    {
        public uint CreatureEntry { get; set; }
        public byte Level { get; set; }
        public ushort Hp { get; set; }
        public ushort Mana { get; set; }
        public uint Armor { get; set; }
        public ushort Str { get; set; }
        public ushort Agi { get; set; }
        public ushort Sta { get; set; }
        public ushort Inte { get; set; }
        public ushort Spi { get; set; }
        public ushort MinDmg { get; set; }
        public ushort MaxDmg { get; set; }
    }
}
