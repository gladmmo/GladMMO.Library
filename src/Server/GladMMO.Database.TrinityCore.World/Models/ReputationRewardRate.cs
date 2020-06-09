using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class ReputationRewardRate
    {
        public uint Faction { get; set; }
        public float QuestRate { get; set; }
        public float QuestDailyRate { get; set; }
        public float QuestWeeklyRate { get; set; }
        public float QuestMonthlyRate { get; set; }
        public float QuestRepeatableRate { get; set; }
        public float CreatureRate { get; set; }
        public float SpellRate { get; set; }
    }
}
