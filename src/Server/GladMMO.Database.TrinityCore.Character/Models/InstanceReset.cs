using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class InstanceReset
    {
        public ushort Mapid { get; set; }
        public byte Difficulty { get; set; }
        public ulong Resettime { get; set; }
    }
}
