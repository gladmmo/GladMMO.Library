using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterStats
    {
        public uint Guid { get; set; }
        public uint Maxhealth { get; set; }
        public uint Maxpower1 { get; set; }
        public uint Maxpower2 { get; set; }
        public uint Maxpower3 { get; set; }
        public uint Maxpower4 { get; set; }
        public uint Maxpower5 { get; set; }
        public uint Maxpower6 { get; set; }
        public uint Maxpower7 { get; set; }
        public uint Strength { get; set; }
        public uint Agility { get; set; }
        public uint Stamina { get; set; }
        public uint Intellect { get; set; }
        public uint Spirit { get; set; }
        public uint Armor { get; set; }
        public uint ResHoly { get; set; }
        public uint ResFire { get; set; }
        public uint ResNature { get; set; }
        public uint ResFrost { get; set; }
        public uint ResShadow { get; set; }
        public uint ResArcane { get; set; }
        public float BlockPct { get; set; }
        public float DodgePct { get; set; }
        public float ParryPct { get; set; }
        public float CritPct { get; set; }
        public float RangedCritPct { get; set; }
        public float SpellCritPct { get; set; }
        public uint AttackPower { get; set; }
        public uint RangedAttackPower { get; set; }
        public uint SpellPower { get; set; }
        public uint Resilience { get; set; }
    }
}
