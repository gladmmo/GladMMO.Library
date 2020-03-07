using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ItemInstance
    {
        public uint Guid { get; set; }
        public uint ItemEntry { get; set; }
        public uint OwnerGuid { get; set; }
        public uint CreatorGuid { get; set; }
        public uint GiftCreatorGuid { get; set; }
        public uint Count { get; set; }
        public int Duration { get; set; }
        public string Charges { get; set; }
        public uint Flags { get; set; }
        public string Enchantments { get; set; }
        public short RandomPropertyId { get; set; }
        public ushort Durability { get; set; }
        public uint PlayedTime { get; set; }
        public string Text { get; set; }
    }
}
