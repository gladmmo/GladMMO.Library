using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestPoolMembers
    {
        public uint QuestId { get; set; }
        public uint PoolId { get; set; }
        public byte PoolIndex { get; set; }
        public string Description { get; set; }
    }
}
