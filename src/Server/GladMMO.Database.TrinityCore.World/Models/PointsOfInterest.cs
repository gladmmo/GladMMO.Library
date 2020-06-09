using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class PointsOfInterest
    {
        public uint Id { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public uint Icon { get; set; }
        public uint Flags { get; set; }
        public uint Importance { get; set; }
        public string Name { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
