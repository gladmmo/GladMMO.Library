using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameEventNpcflag
    {
        public byte EventEntry { get; set; }
        public uint Guid { get; set; }
        public uint Npcflag { get; set; }
    }
}
