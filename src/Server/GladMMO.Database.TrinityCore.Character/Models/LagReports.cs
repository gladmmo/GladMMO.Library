using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class LagReports
    {
        public uint ReportId { get; set; }
        public uint Guid { get; set; }
        public byte LagType { get; set; }
        public ushort MapId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public uint Latency { get; set; }
        public uint CreateTime { get; set; }
    }
}
