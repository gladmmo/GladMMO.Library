using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterArenaStats
    {
        public uint Guid { get; set; }
        public byte Slot { get; set; }
        public ushort MatchMakerRating { get; set; }
    }
}
