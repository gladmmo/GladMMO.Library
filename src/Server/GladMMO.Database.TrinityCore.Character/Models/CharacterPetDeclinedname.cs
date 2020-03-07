using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CharacterPetDeclinedname
    {
        public uint Id { get; set; }
        public uint Owner { get; set; }
        public string Genitive { get; set; }
        public string Dative { get; set; }
        public string Accusative { get; set; }
        public string Instrumental { get; set; }
        public string Prepositional { get; set; }
    }
}
