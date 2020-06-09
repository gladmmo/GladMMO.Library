using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class SpellPetAuras
    {
        public uint Spell { get; set; }
        public byte EffectId { get; set; }
        public uint Pet { get; set; }
        public uint Aura { get; set; }
    }
}
