using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterAccountData
    {
        public uint Guid { get; set; }
        public byte Type { get; set; }
        public uint Time { get; set; }
        public byte[] Data { get; set; }
    }
}
