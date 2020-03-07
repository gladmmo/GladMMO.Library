using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Petition
    {
        public uint Ownerguid { get; set; }
        public uint? Petitionguid { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
    }
}
