using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class BroadcastTextLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string Text { get; set; }
        public string Text1 { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
