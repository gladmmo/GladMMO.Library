using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterReputation
    {
        public uint Guid { get; set; }
        public ushort Faction { get; set; }
        public int Standing { get; set; }
        public ushort Flags { get; set; }
    }
}
