using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CreatureClasslevelstats
    {
        public byte Level { get; set; }
        public byte Class { get; set; }
        public ushort Basehp0 { get; set; }
        public ushort Basehp1 { get; set; }
        public ushort Basehp2 { get; set; }
        public ushort Basemana { get; set; }
        public ushort Basearmor { get; set; }
        public ushort Attackpower { get; set; }
        public ushort Rangedattackpower { get; set; }
        public float DamageBase { get; set; }
        public float DamageExp1 { get; set; }
        public float DamageExp2 { get; set; }
        public string Comment { get; set; }
    }
}
