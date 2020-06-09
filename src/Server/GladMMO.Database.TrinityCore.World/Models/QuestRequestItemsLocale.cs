using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class QuestRequestItemsLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string CompletionText { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
