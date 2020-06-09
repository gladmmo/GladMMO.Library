using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameobjectTemplateLocale
    {
        public uint Entry { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string CastBarCaption { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
