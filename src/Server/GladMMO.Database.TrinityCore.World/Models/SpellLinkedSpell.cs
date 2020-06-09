using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellLinkedSpell
    {
        public int SpellTrigger { get; set; }
        public int SpellEffect { get; set; }
        public byte Type { get; set; }
        public string Comment { get; set; }
    }
}
