using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Updates
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public string State { get; set; }
        public DateTime Timestamp { get; set; }
        public uint Speed { get; set; }
    }
}
