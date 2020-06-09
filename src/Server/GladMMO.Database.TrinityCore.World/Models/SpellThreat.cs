using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpellThreat
    {
        public uint Entry { get; set; }
        public int? FlatMod { get; set; }
        public float PctMod { get; set; }
        public float ApPctMod { get; set; }
    }
}
