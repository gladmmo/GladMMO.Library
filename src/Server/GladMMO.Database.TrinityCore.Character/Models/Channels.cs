using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Channels
    {
        public string Name { get; set; }
        public uint Team { get; set; }
        public byte Announce { get; set; }
        public byte Ownership { get; set; }
        public string Password { get; set; }
        public string BannedList { get; set; }
        public uint LastUsed { get; set; }
    }
}
