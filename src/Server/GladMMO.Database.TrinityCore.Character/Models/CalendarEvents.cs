using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CalendarEvents
    {
        public ulong Id { get; set; }
        public uint Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public int Dungeon { get; set; }
        public uint Eventtime { get; set; }
        public uint Flags { get; set; }
        public uint Time2 { get; set; }
    }
}
