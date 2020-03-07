using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterInventory
    {
        public uint Guid { get; set; }
        public uint Bag { get; set; }
        public byte Slot { get; set; }
        public uint Item { get; set; }
    }
}
