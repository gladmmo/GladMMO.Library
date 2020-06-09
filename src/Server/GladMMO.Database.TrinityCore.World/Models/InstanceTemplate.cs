using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class InstanceTemplate
    {
        public ushort Map { get; set; }
        public ushort Parent { get; set; }
        public string Script { get; set; }
        public byte AllowMount { get; set; }
    }
}
