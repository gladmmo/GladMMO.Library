using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GameEventPrerequisite
    {
        public byte EventEntry { get; set; }
        public uint PrerequisiteEvent { get; set; }
    }
}
