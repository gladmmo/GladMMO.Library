using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterBattlegroundData
    {
        public uint Guid { get; set; }
        public uint InstanceId { get; set; }
        public ushort Team { get; set; }
        public float JoinX { get; set; }
        public float JoinY { get; set; }
        public float JoinZ { get; set; }
        public float JoinO { get; set; }
        public ushort JoinMapId { get; set; }
        public uint TaxiStart { get; set; }
        public uint TaxiEnd { get; set; }
        public uint MountSpell { get; set; }
    }
}
