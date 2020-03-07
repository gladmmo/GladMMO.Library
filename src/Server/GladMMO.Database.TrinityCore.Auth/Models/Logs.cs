using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class Logs
    {
        public uint Time { get; set; }
        public uint Realm { get; set; }
        public string Type { get; set; }
        public byte Level { get; set; }
        public string String { get; set; }
    }
}
