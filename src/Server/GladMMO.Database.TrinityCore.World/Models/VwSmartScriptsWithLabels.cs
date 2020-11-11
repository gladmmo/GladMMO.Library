using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class VwSmartScriptsWithLabels
    {
        public int Entryorguid { get; set; }
        public byte SourceType { get; set; }
        public ushort Id { get; set; }
        public ushort Link { get; set; }
        public string EventType { get; set; }
        public ushort EventPhaseMask { get; set; }
        public byte EventChance { get; set; }
        public ushort EventFlags { get; set; }
        public uint EventParam1 { get; set; }
        public uint EventParam2 { get; set; }
        public uint EventParam3 { get; set; }
        public uint EventParam4 { get; set; }
        public uint EventParam5 { get; set; }
        public string ActionType { get; set; }
        public uint ActionParam1 { get; set; }
        public uint ActionParam2 { get; set; }
        public uint ActionParam3 { get; set; }
        public uint ActionParam4 { get; set; }
        public uint ActionParam5 { get; set; }
        public uint ActionParam6 { get; set; }
        public string TargetType { get; set; }
        public uint TargetParam1 { get; set; }
        public uint TargetParam2 { get; set; }
        public uint TargetParam3 { get; set; }
        public uint TargetParam4 { get; set; }
        public float TargetX { get; set; }
        public float TargetY { get; set; }
        public float TargetZ { get; set; }
        public float TargetO { get; set; }
        public string Comment { get; set; }
    }
}
