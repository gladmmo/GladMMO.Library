using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class QuestGreeting
    {
        public uint Id { get; set; }
        public byte Type { get; set; }
        public ushort GreetEmoteType { get; set; }
        public uint GreetEmoteDelay { get; set; }
        public string Greeting { get; set; }
        public short VerifiedBuild { get; set; }
    }
}
