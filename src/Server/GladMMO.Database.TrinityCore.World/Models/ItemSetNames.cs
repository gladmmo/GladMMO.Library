using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class ItemSetNames
    {
        public uint Entry { get; set; }
        public string Name { get; set; }
        public byte InventoryType { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
