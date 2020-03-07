using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterSocial
    {
        public uint Guid { get; set; }
        public uint Friend { get; set; }
        public byte Flags { get; set; }
        public string Note { get; set; }
    }
}
