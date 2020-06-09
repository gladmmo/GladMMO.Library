using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PlayerTotemModel
    {
        public byte TotemSlot { get; set; }
        public byte RaceId { get; set; }
        public uint DisplayId { get; set; }
    }
}
