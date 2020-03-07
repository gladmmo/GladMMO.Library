using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class BannedAddons
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
