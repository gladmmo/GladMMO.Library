using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureTemplateResistance
    {
        public uint CreatureId { get; set; }
        public byte School { get; set; }
        public short? Resistance { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
