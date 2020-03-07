using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildBankEventlog
    {
        public uint Guildid { get; set; }
        public uint LogGuid { get; set; }
        public byte TabId { get; set; }
        public byte EventType { get; set; }
        public uint PlayerGuid { get; set; }
        public uint ItemOrMoney { get; set; }
        public ushort ItemStackCount { get; set; }
        public byte DestTabId { get; set; }
        public uint TimeStamp { get; set; }
    }
}
