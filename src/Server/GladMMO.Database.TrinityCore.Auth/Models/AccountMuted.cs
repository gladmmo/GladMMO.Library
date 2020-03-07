using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class AccountMuted
    {
        public uint Guid { get; set; }
        public uint Mutedate { get; set; }
        public uint Mutetime { get; set; }
        public string Mutedby { get; set; }
        public string Mutereason { get; set; }
    }
}
