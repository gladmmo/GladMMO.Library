using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildRank
    {
        public uint Guildid { get; set; }
        public byte Rid { get; set; }
        public string Rname { get; set; }
        public uint Rights { get; set; }
        public uint BankMoneyPerDay { get; set; }
    }
}
