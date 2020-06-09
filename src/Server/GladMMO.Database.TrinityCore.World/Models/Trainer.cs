using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Trainer
    {
        public uint Id { get; set; }
        public byte Type { get; set; }
        public uint Requirement { get; set; }
        public string Greeting { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
