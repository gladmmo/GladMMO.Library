using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class InstanceSpawnGroups
    {
        public ushort InstanceMapId { get; set; }
        public byte BossStateId { get; set; }
        public byte BossStates { get; set; }
        public uint SpawnGroupId { get; set; }
        public byte Flags { get; set; }
    }
}
