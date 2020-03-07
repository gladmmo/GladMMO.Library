using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class MailItems
    {
        public uint MailId { get; set; }
        public uint ItemGuid { get; set; }
        public uint Receiver { get; set; }
    }
}
