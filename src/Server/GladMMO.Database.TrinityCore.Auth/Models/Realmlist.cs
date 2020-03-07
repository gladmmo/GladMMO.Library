using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Realmlist
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LocalAddress { get; set; }
        public string LocalSubnetMask { get; set; }
        public ushort Port { get; set; }
        public byte Icon { get; set; }
        public byte Flag { get; set; }
        public byte Timezone { get; set; }
        public byte AllowedSecurityLevel { get; set; }
        public float Population { get; set; }
        public uint Gamebuild { get; set; }
    }
}
