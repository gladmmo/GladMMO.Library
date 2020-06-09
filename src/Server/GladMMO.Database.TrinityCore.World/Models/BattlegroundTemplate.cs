using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class BattlegroundTemplate
    {
        public uint Id { get; set; }
        public ushort MinPlayersPerTeam { get; set; }
        public ushort MaxPlayersPerTeam { get; set; }
        public byte MinLvl { get; set; }
        public byte MaxLvl { get; set; }
        public uint AllianceStartLoc { get; set; }
        public float AllianceStartO { get; set; }
        public uint HordeStartLoc { get; set; }
        public float HordeStartO { get; set; }
        public float StartMaxDist { get; set; }
        public byte Weight { get; set; }
        public string ScriptName { get; set; }
        public string Comment { get; set; }
    }
}
