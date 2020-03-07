using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class AccountInstanceTimes
    {
        public uint AccountId { get; set; }
        public uint InstanceId { get; set; }
        public ulong ReleaseTime { get; set; }
    }
}
