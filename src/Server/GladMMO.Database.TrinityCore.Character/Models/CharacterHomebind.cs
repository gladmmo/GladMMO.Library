using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterHomebind
    {
        public uint Guid { get; set; }
        public ushort MapId { get; set; }
        public ushort ZoneId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
    }
}
