using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterQueststatus
    {
        public uint Guid { get; set; }
        public uint Quest { get; set; }
        public byte Status { get; set; }
        public byte Explored { get; set; }
        public uint Timer { get; set; }
        public ushort Mobcount1 { get; set; }
        public ushort Mobcount2 { get; set; }
        public ushort Mobcount3 { get; set; }
        public ushort Mobcount4 { get; set; }
        public ushort Itemcount1 { get; set; }
        public ushort Itemcount2 { get; set; }
        public ushort Itemcount3 { get; set; }
        public ushort Itemcount4 { get; set; }
        public ushort Itemcount5 { get; set; }
        public ushort Itemcount6 { get; set; }
        public ushort Playercount { get; set; }
    }
}
