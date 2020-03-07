using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterPet
    {
        public uint Id { get; set; }
        public uint Entry { get; set; }
        public uint Owner { get; set; }
        public uint? Modelid { get; set; }
        public uint CreatedBySpell { get; set; }
        public byte PetType { get; set; }
        public ushort Level { get; set; }
        public uint Exp { get; set; }
        public byte Reactstate { get; set; }
        public string Name { get; set; }
        public byte Renamed { get; set; }
        public byte Slot { get; set; }
        public uint Curhealth { get; set; }
        public uint Curmana { get; set; }
        public uint Curhappiness { get; set; }
        public uint Savetime { get; set; }
        public string Abdata { get; set; }
    }
}
