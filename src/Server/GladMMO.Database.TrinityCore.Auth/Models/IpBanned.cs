using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class IpBanned
    {
        public string Ip { get; set; }
        public uint Bandate { get; set; }
        public uint Unbandate { get; set; }
        public string Bannedby { get; set; }
        public string Banreason { get; set; }
    }
}
