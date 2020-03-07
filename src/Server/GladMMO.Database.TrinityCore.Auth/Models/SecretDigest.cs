using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.Auth.Models
{
    public partial class SecretDigest
    {
        public uint Id { get; set; }
        public string Digest { get; set; }
    }
}
