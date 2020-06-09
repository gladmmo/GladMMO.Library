using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ScriptSplineChainWaypoints
    {
        public uint Entry { get; set; }
        public ushort ChainId { get; set; }
        public byte SplineId { get; set; }
        public byte WpId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
