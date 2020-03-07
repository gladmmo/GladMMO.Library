using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildEventlog
    {
        public uint Guildid { get; set; }
        public uint LogGuid { get; set; }
        public byte EventType { get; set; }
        public uint PlayerGuid1 { get; set; }
        public uint PlayerGuid2 { get; set; }
        public byte NewRank { get; set; }
        public uint TimeStamp { get; set; }
    }
}
