using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class Realmcharacters
    {
        public uint Realmid { get; set; }
        public uint Acctid { get; set; }
        public byte Numchars { get; set; }
    }
}
