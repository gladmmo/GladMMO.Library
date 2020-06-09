using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class Transports
    {
        public uint Guid { get; set; }
        public uint Entry { get; set; }
        public string Name { get; set; }
        public string ScriptName { get; set; }
    }
}
