using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class VwDisablesWithLabels
    {
        public string SourceType { get; set; }
        public uint Entry { get; set; }
        public short Flags { get; set; }
        public string Params0 { get; set; }
        public string Params1 { get; set; }
        public string Comment { get; set; }
    }
}
