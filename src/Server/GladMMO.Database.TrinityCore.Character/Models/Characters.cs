using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Characters
    {
        public uint Guid { get; set; }
        public uint Account { get; set; }
        public string Name { get; set; }
        public byte Race { get; set; }
        public byte Class { get; set; }
        public byte Gender { get; set; }
        public byte Level { get; set; }
        public uint Xp { get; set; }
        public uint Money { get; set; }
        public byte Skin { get; set; }
        public byte Face { get; set; }
        public byte HairStyle { get; set; }
        public byte HairColor { get; set; }
        public byte FacialStyle { get; set; }
        public byte BankSlots { get; set; }
        public byte RestState { get; set; }
        public uint PlayerFlags { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public ushort Map { get; set; }
        public uint InstanceId { get; set; }
        public byte InstanceModeMask { get; set; }
        public float Orientation { get; set; }
        public string Taximask { get; set; }
        public byte Online { get; set; }
        public byte Cinematic { get; set; }
        public uint Totaltime { get; set; }
        public uint Leveltime { get; set; }
        public uint LogoutTime { get; set; }
        public byte IsLogoutResting { get; set; }
        public float RestBonus { get; set; }
        public uint ResettalentsCost { get; set; }
        public uint ResettalentsTime { get; set; }
        public float TransX { get; set; }
        public float TransY { get; set; }
        public float TransZ { get; set; }
        public float TransO { get; set; }
        public uint Transguid { get; set; }
        public ushort ExtraFlags { get; set; }
        public byte StableSlots { get; set; }
        public ushort AtLogin { get; set; }
        public ushort Zone { get; set; }
        public uint DeathExpireTime { get; set; }
        public string TaxiPath { get; set; }
        public uint ArenaPoints { get; set; }
        public uint TotalHonorPoints { get; set; }
        public uint TodayHonorPoints { get; set; }
        public uint YesterdayHonorPoints { get; set; }
        public uint TotalKills { get; set; }
        public ushort TodayKills { get; set; }
        public ushort YesterdayKills { get; set; }
        public uint ChosenTitle { get; set; }
        public ulong KnownCurrencies { get; set; }
        public uint WatchedFaction { get; set; }
        public byte Drunk { get; set; }
        public uint Health { get; set; }
        public uint Power1 { get; set; }
        public uint Power2 { get; set; }
        public uint Power3 { get; set; }
        public uint Power4 { get; set; }
        public uint Power5 { get; set; }
        public uint Power6 { get; set; }
        public uint Power7 { get; set; }
        public uint Latency { get; set; }
        public byte TalentGroupsCount { get; set; }
        public byte ActiveTalentGroup { get; set; }
        public string ExploredZones { get; set; }
        public string EquipmentCache { get; set; }
        public uint AmmoId { get; set; }
        public string KnownTitles { get; set; }
        public byte ActionBars { get; set; }
        public byte GrantableLevels { get; set; }
        public uint? DeleteInfosAccount { get; set; }
        public string DeleteInfosName { get; set; }
        public uint? DeleteDate { get; set; }
    }
}
