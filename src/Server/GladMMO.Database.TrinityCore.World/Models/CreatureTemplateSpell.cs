using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CreatureTemplateSpell
    {
        public uint CreatureId { get; set; }
        public byte Index { get; set; }
        public uint? Spell { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
