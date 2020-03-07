using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class PvpstatsPlayers
    {
        public ulong BattlegroundId { get; set; }
        public uint CharacterGuid { get; set; }
        public ulong Winner { get; set; }
        public uint ScoreKillingBlows { get; set; }
        public uint ScoreDeaths { get; set; }
        public uint ScoreHonorableKills { get; set; }
        public uint ScoreBonusHonor { get; set; }
        public uint ScoreDamageDone { get; set; }
        public uint ScoreHealingDone { get; set; }
        public uint Attr1 { get; set; }
        public uint Attr2 { get; set; }
        public uint Attr3 { get; set; }
        public uint Attr4 { get; set; }
        public uint Attr5 { get; set; }
    }
}
