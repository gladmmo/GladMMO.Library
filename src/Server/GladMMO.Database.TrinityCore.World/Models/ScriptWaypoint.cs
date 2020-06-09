using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class ScriptWaypoint
    {
        public uint Entry { get; set; }
        public uint Pointid { get; set; }
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float LocationZ { get; set; }
        public uint Waittime { get; set; }
        public string PointComment { get; set; }
    }
}
