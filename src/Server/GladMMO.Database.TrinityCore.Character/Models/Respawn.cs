using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Respawn
    {
        public ushort Type { get; set; }
        public uint SpawnId { get; set; }
        public ulong RespawnTime { get; set; }
        public ushort MapId { get; set; }
        public uint InstanceId { get; set; }
    }
}
