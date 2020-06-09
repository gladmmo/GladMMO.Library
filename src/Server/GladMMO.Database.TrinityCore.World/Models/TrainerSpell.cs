using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class TrainerSpell
    {
        public uint TrainerId { get; set; }
        public uint SpellId { get; set; }
        public uint MoneyCost { get; set; }
        public uint ReqSkillLine { get; set; }
        public uint ReqSkillRank { get; set; }
        public uint ReqAbility1 { get; set; }
        public uint ReqAbility2 { get; set; }
        public uint ReqAbility3 { get; set; }
        public byte ReqLevel { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
