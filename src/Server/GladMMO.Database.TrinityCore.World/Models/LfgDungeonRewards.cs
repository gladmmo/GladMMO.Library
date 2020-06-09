using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class LfgDungeonRewards
    {
        public uint DungeonId { get; set; }
        public byte MaxLevel { get; set; }
        public uint FirstQuestId { get; set; }
        public uint OtherQuestId { get; set; }
    }
}
