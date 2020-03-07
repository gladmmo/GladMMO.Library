using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GmTicket
    {
        public uint Id { get; set; }
        public byte Type { get; set; }
        public uint PlayerGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint CreateTime { get; set; }
        public ushort MapId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public uint LastModifiedTime { get; set; }
        public int ClosedBy { get; set; }
        public uint AssignedTo { get; set; }
        public string Comment { get; set; }
        public string Response { get; set; }
        public byte Completed { get; set; }
        public byte Escalated { get; set; }
        public byte Viewed { get; set; }
        public byte NeedMoreHelp { get; set; }
        public int ResolvedBy { get; set; }
    }
}
