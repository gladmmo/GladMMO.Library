using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PoolTemplate
    {
        public uint Entry { get; set; }
        public uint MaxLimit { get; set; }
        public string Description { get; set; }
    }
}
