using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterSkills
    {
        public uint Guid { get; set; }
        public ushort Skill { get; set; }
        public ushort Value { get; set; }
        public ushort Max { get; set; }
    }
}
