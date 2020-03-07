using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildBankTab
    {
        public uint Guildid { get; set; }
        public byte TabId { get; set; }
        public string TabName { get; set; }
        public string TabIcon { get; set; }
        public string TabText { get; set; }
    }
}
