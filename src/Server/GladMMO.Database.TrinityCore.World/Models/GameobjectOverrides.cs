using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameobjectOverrides
    {
        public uint SpawnId { get; set; }
        public ushort Faction { get; set; }
        public uint Flags { get; set; }
    }
}
