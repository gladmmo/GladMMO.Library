using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildMember
    {
        public uint Guildid { get; set; }
        public uint Guid { get; set; }
        public byte Rank { get; set; }
        public string Pnote { get; set; }
        public string Offnote { get; set; }
    }
}
