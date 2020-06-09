using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameEventNpcVendor
    {
        public sbyte EventEntry { get; set; }
        public uint Guid { get; set; }
        public short Slot { get; set; }
        public uint Item { get; set; }
        public uint Maxcount { get; set; }
        public uint Incrtime { get; set; }
        public uint ExtendedCost { get; set; }
    }
}
