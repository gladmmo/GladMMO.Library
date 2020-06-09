using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class PageText
    {
        public uint Id { get; set; }
        public string Text { get; set; }
        public uint NextPageId { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
