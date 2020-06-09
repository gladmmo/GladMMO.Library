using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestOfferRewardLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string RewardText { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
