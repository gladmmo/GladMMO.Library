using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class WaypointScripts
    {
        public uint Id { get; set; }
        public uint Delay { get; set; }
        public uint Command { get; set; }
        public uint Datalong { get; set; }
        public uint Datalong2 { get; set; }
        public uint Dataint { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float O { get; set; }
        public int Guid { get; set; }
    }
}
