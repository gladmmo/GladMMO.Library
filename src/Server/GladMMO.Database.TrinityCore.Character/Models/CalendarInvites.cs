using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class CalendarInvites
    {
        public ulong Id { get; set; }
        public ulong Event { get; set; }
        public uint Invitee { get; set; }
        public uint Sender { get; set; }
        public byte Status { get; set; }
        public uint Statustime { get; set; }
        public byte Rank { get; set; }
        public string Text { get; set; }
    }
}
