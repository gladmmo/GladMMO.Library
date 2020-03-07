using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class PetSpellCooldown
    {
        public uint Guid { get; set; }
        public uint Spell { get; set; }
        public uint Time { get; set; }
        public uint CategoryId { get; set; }
        public uint CategoryEnd { get; set; }
    }
}
