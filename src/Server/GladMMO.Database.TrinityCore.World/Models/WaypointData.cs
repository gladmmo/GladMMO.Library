using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class WaypointData
    {
        public uint Id { get; set; }
        public uint Point { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Orientation { get; set; }
        public uint Delay { get; set; }
        public int MoveType { get; set; }
        public int Action { get; set; }
        public short ActionChance { get; set; }
        public uint Wpguid { get; set; }
    }
}
