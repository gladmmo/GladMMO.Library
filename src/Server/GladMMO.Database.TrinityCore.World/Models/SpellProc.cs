using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellProc
    {
        public int SpellId { get; set; }
        public byte SchoolMask { get; set; }
        public ushort SpellFamilyName { get; set; }
        public uint SpellFamilyMask0 { get; set; }
        public uint SpellFamilyMask1 { get; set; }
        public uint SpellFamilyMask2 { get; set; }
        public uint ProcFlags { get; set; }
        public uint SpellTypeMask { get; set; }
        public uint SpellPhaseMask { get; set; }
        public uint HitMask { get; set; }
        public uint AttributesMask { get; set; }
        public uint DisableEffectsMask { get; set; }
        public float ProcsPerMinute { get; set; }
        public float Chance { get; set; }
        public uint Cooldown { get; set; }
        public byte Charges { get; set; }
    }
}
