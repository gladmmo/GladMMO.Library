using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ItemRefundInstance
    {
        public uint ItemGuid { get; set; }
        public uint PlayerGuid { get; set; }
        public uint PaidMoney { get; set; }
        public ushort PaidExtendedCost { get; set; }
    }
}
