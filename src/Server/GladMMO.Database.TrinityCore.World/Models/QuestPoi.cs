using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class QuestPoi
    {
        public uint QuestId { get; set; }
        public uint Id { get; set; }
        public int ObjectiveIndex { get; set; }
        public uint MapId { get; set; }
        public uint WorldMapAreaId { get; set; }
        public uint Floor { get; set; }
        public uint Priority { get; set; }
        public uint Flags { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
