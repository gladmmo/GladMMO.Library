using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class WardenChecks
    {
        public ushort Id { get; set; }
        public byte? Type { get; set; }
        public string Data { get; set; }
        public string Str { get; set; }
        public uint? Address { get; set; }
        public byte? Length { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
