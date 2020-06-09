using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class FishingLootTemplate
    {
        public uint Entry { get; set; }
        public uint Item { get; set; }
        public uint Reference { get; set; }
        public float Chance { get; set; }
        public bool QuestRequired { get; set; }
        public ushort LootMode { get; set; }
        public byte GroupId { get; set; }
        public byte MinCount { get; set; }
        public byte MaxCount { get; set; }
        public string Comment { get; set; }
    }
}
