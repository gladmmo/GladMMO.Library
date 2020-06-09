using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SkillPerfectItemTemplate
    {
        public uint SpellId { get; set; }
        public uint RequiredSpecialization { get; set; }
        public float PerfectCreateChance { get; set; }
        public uint PerfectItemType { get; set; }
    }
}
