using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestTracker
    {
        public uint Id { get; set; }
        public uint CharacterGuid { get; set; }
        public DateTime QuestAcceptTime { get; set; }
        public DateTime? QuestCompleteTime { get; set; }
        public DateTime? QuestAbandonTime { get; set; }
        public bool CompletedByGm { get; set; }
        public string CoreHash { get; set; }
        public string CoreRevision { get; set; }
    }
}
