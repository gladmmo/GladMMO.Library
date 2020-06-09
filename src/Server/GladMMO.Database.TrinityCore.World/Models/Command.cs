using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class Command
    {
        public string Name { get; set; }
        public ushort Permission { get; set; }
        public string Help { get; set; }
    }
}
