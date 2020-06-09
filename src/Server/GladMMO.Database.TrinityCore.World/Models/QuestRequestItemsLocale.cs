using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestRequestItemsLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string CompletionText { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
