using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class AccountAccess
    {
        public uint Id { get; set; }
        public byte Gmlevel { get; set; }
        public int RealmId { get; set; }
    }
}
