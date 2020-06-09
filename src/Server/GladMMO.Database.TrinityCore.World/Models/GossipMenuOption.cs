using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GossipMenuOption
    {
        public ushort MenuId { get; set; }
        public ushort OptionId { get; set; }
        public uint OptionIcon { get; set; }
        public string OptionText { get; set; }
        public int OptionBroadcastTextId { get; set; }
        public byte OptionType { get; set; }
        public uint OptionNpcFlag { get; set; }
        public uint ActionMenuId { get; set; }
        public uint ActionPoiId { get; set; }
        public byte BoxCoded { get; set; }
        public uint BoxMoney { get; set; }
        public string BoxText { get; set; }
        public int BoxBroadcastTextId { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
