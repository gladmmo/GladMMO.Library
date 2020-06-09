using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureTextLocale
    {
        public uint CreatureId { get; set; }
        public byte GroupId { get; set; }
        public byte Id { get; set; }
        public string Locale { get; set; }
        public string Text { get; set; }
    }
}
