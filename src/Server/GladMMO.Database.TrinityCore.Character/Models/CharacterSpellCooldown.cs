using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterSpellCooldown
    {
        public uint Guid { get; set; }
        public uint Spell { get; set; }
        public uint Item { get; set; }
        public uint Time { get; set; }
        public uint CategoryId { get; set; }
        public uint CategoryEnd { get; set; }
    }
}
