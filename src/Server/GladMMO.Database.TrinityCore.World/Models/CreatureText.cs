using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CreatureText
    {
        public uint CreatureId { get; set; }
        public byte GroupId { get; set; }
        public byte Id { get; set; }
        public string Text { get; set; }
        public byte Type { get; set; }
        public sbyte Language { get; set; }
        public float Probability { get; set; }
        public uint Emote { get; set; }
        public uint Duration { get; set; }
        public uint Sound { get; set; }
        public int BroadcastTextId { get; set; }
        public byte TextRange { get; set; }
        public string Comment { get; set; }
    }
}
