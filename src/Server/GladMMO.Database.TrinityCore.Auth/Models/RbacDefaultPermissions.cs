using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class RbacDefaultPermissions
    {
        public uint SecId { get; set; }
        public uint PermissionId { get; set; }
        public int RealmId { get; set; }

        public virtual RbacPermissions Permission { get; set; }
    }
}
