using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class RbacLinkedPermissions
    {
        public uint Id { get; set; }
        public uint LinkedId { get; set; }

        public virtual RbacPermissions IdNavigation { get; set; }
        public virtual RbacPermissions Linked { get; set; }
    }
}
