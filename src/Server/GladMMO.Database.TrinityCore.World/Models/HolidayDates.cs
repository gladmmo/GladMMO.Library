﻿using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class HolidayDates
    {
        public uint Id { get; set; }
        public byte DateId { get; set; }
        public uint DateValue { get; set; }
        public uint HolidayDuration { get; set; }
    }
}