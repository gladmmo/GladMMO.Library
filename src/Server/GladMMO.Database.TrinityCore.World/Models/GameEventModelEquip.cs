using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class GameEventModelEquip
    {
        public sbyte EventEntry { get; set; }
        public uint Guid { get; set; }
        public uint Modelid { get; set; }
        public byte EquipmentId { get; set; }
    }
}
