﻿using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class LinkedRespawn
    {
        public uint Guid { get; set; }
        public uint LinkedGuid { get; set; }
        public byte LinkType { get; set; }
    }
}