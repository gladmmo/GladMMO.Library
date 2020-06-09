using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestRequestItems
    {
        public uint Id { get; set; }
        public ushort EmoteOnComplete { get; set; }
        public ushort EmoteOnIncomplete { get; set; }
        public string CompletionText { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
