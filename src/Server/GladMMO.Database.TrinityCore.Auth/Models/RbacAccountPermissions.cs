using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class RbacAccountPermissions
    {
        public uint AccountId { get; set; }
        public uint PermissionId { get; set; }
        public bool? Granted { get; set; }
        public int RealmId { get; set; }

        public virtual Account Account { get; set; }
        public virtual RbacPermissions Permission { get; set; }
    }
}
