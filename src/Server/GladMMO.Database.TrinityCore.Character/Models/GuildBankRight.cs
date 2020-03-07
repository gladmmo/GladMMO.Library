using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GuildBankRight
    {
        public uint Guildid { get; set; }
        public byte TabId { get; set; }
        public byte Rid { get; set; }
        public byte Gbright { get; set; }
        public uint SlotPerDay { get; set; }
    }
}
