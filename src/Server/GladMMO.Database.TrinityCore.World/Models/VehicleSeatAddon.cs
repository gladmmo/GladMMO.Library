using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class VehicleSeatAddon
    {
        public uint SeatEntry { get; set; }
        public float? SeatOrientation { get; set; }
        public float? ExitParamX { get; set; }
        public float? ExitParamY { get; set; }
        public float? ExitParamZ { get; set; }
        public float? ExitParamO { get; set; }
        public bool? ExitParamValue { get; set; }
    }
}
