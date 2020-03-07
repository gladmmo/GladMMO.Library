using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterInstance
    {
        public uint Guid { get; set; }
        public uint Instance { get; set; }
        public byte Permanent { get; set; }
        public byte ExtendState { get; set; }
    }
}
