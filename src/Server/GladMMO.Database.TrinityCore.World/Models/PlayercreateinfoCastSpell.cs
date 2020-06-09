using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PlayercreateinfoCastSpell
    {
        public uint RaceMask { get; set; }
        public uint ClassMask { get; set; }
        public uint Spell { get; set; }
        public string Note { get; set; }
    }
}
