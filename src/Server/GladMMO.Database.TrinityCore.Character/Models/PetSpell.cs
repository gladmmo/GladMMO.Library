using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class PetSpell
    {
        public uint Guid { get; set; }
        public uint Spell { get; set; }
        public byte Active { get; set; }
    }
}
