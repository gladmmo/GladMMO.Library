using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class CreatureTemplate
    {
        public uint Entry { get; set; }
        public uint DifficultyEntry1 { get; set; }
        public uint DifficultyEntry2 { get; set; }
        public uint DifficultyEntry3 { get; set; }
        public uint KillCredit1 { get; set; }
        public uint KillCredit2 { get; set; }
        public uint Modelid1 { get; set; }
        public uint Modelid2 { get; set; }
        public uint Modelid3 { get; set; }
        public uint Modelid4 { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public string IconName { get; set; }
        public uint GossipMenuId { get; set; }
        public byte Minlevel { get; set; }
        public byte Maxlevel { get; set; }
        public short Exp { get; set; }
        public ushort Faction { get; set; }
        public uint Npcflag { get; set; }
        public float SpeedWalk { get; set; }
        public float SpeedRun { get; set; }
        public float Scale { get; set; }
        public byte Rank { get; set; }
        public sbyte Dmgschool { get; set; }
        public uint BaseAttackTime { get; set; }
        public uint RangeAttackTime { get; set; }
        public float BaseVariance { get; set; }
        public float RangeVariance { get; set; }
        public byte UnitClass { get; set; }
        public uint UnitFlags { get; set; }
        public uint UnitFlags2 { get; set; }
        public uint Dynamicflags { get; set; }
        public sbyte Family { get; set; }
        public byte Type { get; set; }
        public uint TypeFlags { get; set; }
        public uint Lootid { get; set; }
        public uint Pickpocketloot { get; set; }
        public uint Skinloot { get; set; }
        public uint PetSpellDataId { get; set; }
        public uint VehicleId { get; set; }
        public uint Mingold { get; set; }
        public uint Maxgold { get; set; }
        public string Ainame { get; set; }
        public byte MovementType { get; set; }
        public float HoverHeight { get; set; }
        public float HealthModifier { get; set; }
        public float ManaModifier { get; set; }
        public float ArmorModifier { get; set; }
        public float DamageModifier { get; set; }
        public float ExperienceModifier { get; set; }
        public byte RacialLeader { get; set; }
        public uint MovementId { get; set; }
        public byte RegenHealth { get; set; }
        public uint MechanicImmuneMask { get; set; }
        public uint SpellSchoolImmuneMask { get; set; }
        public uint FlagsExtra { get; set; }
        public string ScriptName { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
