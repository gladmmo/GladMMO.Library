using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class QuestPoolTemplate
    {
        public uint PoolId { get; set; }
        public uint NumActive { get; set; }
        public string Description { get; set; }
    }
}
