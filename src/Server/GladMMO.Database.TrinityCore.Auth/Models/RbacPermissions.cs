using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class RbacPermissions
    {
        public RbacPermissions()
        {
            RbacAccountPermissions = new HashSet<RbacAccountPermissions>();
            RbacDefaultPermissions = new HashSet<RbacDefaultPermissions>();
            RbacLinkedPermissionsIdNavigation = new HashSet<RbacLinkedPermissions>();
            RbacLinkedPermissionsLinked = new HashSet<RbacLinkedPermissions>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RbacAccountPermissions> RbacAccountPermissions { get; set; }
        public virtual ICollection<RbacDefaultPermissions> RbacDefaultPermissions { get; set; }
        public virtual ICollection<RbacLinkedPermissions> RbacLinkedPermissionsIdNavigation { get; set; }
        public virtual ICollection<RbacLinkedPermissions> RbacLinkedPermissionsLinked { get; set; }
    }
}
