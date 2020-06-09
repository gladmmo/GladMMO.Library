using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureQuestitem
    {
        public uint CreatureEntry { get; set; }
        public uint Idx { get; set; }
        public uint ItemId { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
