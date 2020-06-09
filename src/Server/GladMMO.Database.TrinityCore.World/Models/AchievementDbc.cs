using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class AchievementDbc
    {
        public uint Id { get; set; }
        public int RequiredFaction { get; set; }
        public int MapId { get; set; }
        public uint Points { get; set; }
        public uint Flags { get; set; }
        public uint Count { get; set; }
        public uint RefAchievement { get; set; }
    }
}
