using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PetNameGeneration
    {
        public uint Id { get; set; }
        public string Word { get; set; }
        public uint Entry { get; set; }
        public byte Half { get; set; }
    }
}
