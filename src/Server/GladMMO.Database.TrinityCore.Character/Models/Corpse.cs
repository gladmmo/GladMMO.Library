using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Corpse
    {
        public uint Guid { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Orientation { get; set; }
        public ushort MapId { get; set; }
        public uint PhaseMask { get; set; }
        public uint DisplayId { get; set; }
        public string ItemCache { get; set; }
        public uint Bytes1 { get; set; }
        public uint Bytes2 { get; set; }
        public uint GuildId { get; set; }
        public byte Flags { get; set; }
        public byte DynFlags { get; set; }
        public uint Time { get; set; }
        public byte CorpseType { get; set; }
        public uint InstanceId { get; set; }
    }
}
