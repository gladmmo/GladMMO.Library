using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellLearnSpell
    {
        public ushort Entry { get; set; }
        public ushort SpellId { get; set; }
        public byte Active { get; set; }
    }
}
