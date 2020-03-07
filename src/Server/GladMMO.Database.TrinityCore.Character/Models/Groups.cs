using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Groups
    {
        public uint Guid { get; set; }
        public uint LeaderGuid { get; set; }
        public byte LootMethod { get; set; }
        public uint LooterGuid { get; set; }
        public byte LootThreshold { get; set; }
        public ulong Icon1 { get; set; }
        public ulong Icon2 { get; set; }
        public ulong Icon3 { get; set; }
        public ulong Icon4 { get; set; }
        public ulong Icon5 { get; set; }
        public ulong Icon6 { get; set; }
        public ulong Icon7 { get; set; }
        public ulong Icon8 { get; set; }
        public byte GroupType { get; set; }
        public byte Difficulty { get; set; }
        public byte RaidDifficulty { get; set; }
        public uint MasterLooterGuid { get; set; }
    }
}
