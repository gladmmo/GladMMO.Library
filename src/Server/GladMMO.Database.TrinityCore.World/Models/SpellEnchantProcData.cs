using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellEnchantProcData
    {
        public uint EnchantId { get; set; }
        public float Chance { get; set; }
        public float ProcsPerMinute { get; set; }
        public uint HitMask { get; set; }
        public uint AttributesMask { get; set; }
    }
}
