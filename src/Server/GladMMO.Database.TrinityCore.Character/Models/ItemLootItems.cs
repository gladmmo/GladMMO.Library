using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ItemLootItems
    {
        public uint ContainerId { get; set; }
        public uint ItemId { get; set; }
        public int ItemCount { get; set; }
        public bool FollowRules { get; set; }
        public bool Ffa { get; set; }
        public bool Blocked { get; set; }
        public bool Counted { get; set; }
        public bool UnderThreshold { get; set; }
        public bool NeedsQuest { get; set; }
        public int RndProp { get; set; }
        public int RndSuffix { get; set; }
    }
}
