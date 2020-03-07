using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class AccountBanned
    {
        public uint Id { get; set; }
        public uint Bandate { get; set; }
        public uint Unbandate { get; set; }
        public string Bannedby { get; set; }
        public string Banreason { get; set; }
        public byte Active { get; set; }
    }
}
