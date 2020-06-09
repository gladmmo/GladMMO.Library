using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CreatureOnkillReputation
    {
        public uint CreatureId { get; set; }
        public short RewOnKillRepFaction1 { get; set; }
        public short RewOnKillRepFaction2 { get; set; }
        public sbyte MaxStanding1 { get; set; }
        public sbyte IsTeamAward1 { get; set; }
        public int RewOnKillRepValue1 { get; set; }
        public sbyte MaxStanding2 { get; set; }
        public sbyte IsTeamAward2 { get; set; }
        public int RewOnKillRepValue2 { get; set; }
        public byte TeamDependent { get; set; }
    }
}
