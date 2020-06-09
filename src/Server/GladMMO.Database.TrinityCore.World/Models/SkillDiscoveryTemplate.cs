using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SkillDiscoveryTemplate
    {
        public uint SpellId { get; set; }
        public uint ReqSpell { get; set; }
        public ushort ReqSkillValue { get; set; }
        public float Chance { get; set; }
    }
}
