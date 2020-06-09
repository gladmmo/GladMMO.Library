using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class SpawnGroupTemplate
    {
        public uint GroupId { get; set; }
        public string GroupName { get; set; }
        public uint GroupFlags { get; set; }
    }
}
