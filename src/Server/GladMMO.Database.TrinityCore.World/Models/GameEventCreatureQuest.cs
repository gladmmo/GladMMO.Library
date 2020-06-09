using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameEventCreatureQuest
    {
        public byte EventEntry { get; set; }
        public uint Id { get; set; }
        public uint Quest { get; set; }
    }
}
