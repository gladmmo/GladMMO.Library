using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CreatureAddon
    {
        public uint Guid { get; set; }
        public uint PathId { get; set; }
        public uint Mount { get; set; }
        public uint Bytes1 { get; set; }
        public uint Bytes2 { get; set; }
        public uint Emote { get; set; }
        public byte VisibilityDistanceType { get; set; }
        public string Auras { get; set; }
    }
}
