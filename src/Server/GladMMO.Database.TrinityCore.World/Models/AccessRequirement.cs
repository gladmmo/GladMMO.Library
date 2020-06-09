using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class AccessRequirement
    {
        public uint MapId { get; set; }
        public byte Difficulty { get; set; }
        public byte LevelMin { get; set; }
        public byte LevelMax { get; set; }
        public ushort ItemLevel { get; set; }
        public uint Item { get; set; }
        public uint Item2 { get; set; }
        public uint QuestDoneA { get; set; }
        public uint QuestDoneH { get; set; }
        public uint CompletedAchievement { get; set; }
        public string QuestFailedText { get; set; }
        public string Comment { get; set; }
    }
}
