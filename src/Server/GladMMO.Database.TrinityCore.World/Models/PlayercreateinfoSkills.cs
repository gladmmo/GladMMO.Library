using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PlayercreateinfoSkills
    {
        public uint RaceMask { get; set; }
        public uint ClassMask { get; set; }
        public ushort Skill { get; set; }
        public ushort Rank { get; set; }
        public string Comment { get; set; }
    }
}
