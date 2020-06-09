using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class NpcSpellclickSpells
    {
        public uint NpcEntry { get; set; }
        public uint SpellId { get; set; }
        public byte CastFlags { get; set; }
        public ushort UserType { get; set; }
    }
}
