using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GameTele
    {
        public uint Id { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public ushort Map { get; set; }
        public string Name { get; set; }
    }
}
