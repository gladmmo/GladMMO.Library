using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class ItemSetNamesLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
