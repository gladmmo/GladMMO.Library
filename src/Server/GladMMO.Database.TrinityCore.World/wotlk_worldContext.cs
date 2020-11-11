using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GladMMO
{
    public partial class wotlk_worldContext : DbContext
    {
        public wotlk_worldContext()
        {
        }

        public wotlk_worldContext(DbContextOptions<wotlk_worldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessRequirement> AccessRequirement { get; set; }
        public virtual DbSet<AchievementCriteriaData> AchievementCriteriaData { get; set; }
        public virtual DbSet<AchievementDbc> AchievementDbc { get; set; }
        public virtual DbSet<AchievementReward> AchievementReward { get; set; }
        public virtual DbSet<AchievementRewardLocale> AchievementRewardLocale { get; set; }
        public virtual DbSet<AreatriggerInvolvedrelation> AreatriggerInvolvedrelation { get; set; }
        public virtual DbSet<AreatriggerScripts> AreatriggerScripts { get; set; }
        public virtual DbSet<AreatriggerTavern> AreatriggerTavern { get; set; }
        public virtual DbSet<AreatriggerTeleport> AreatriggerTeleport { get; set; }
        public virtual DbSet<BattlefieldTemplate> BattlefieldTemplate { get; set; }
        public virtual DbSet<BattlegroundTemplate> BattlegroundTemplate { get; set; }
        public virtual DbSet<BattlemasterEntry> BattlemasterEntry { get; set; }
        public virtual DbSet<BroadcastText> BroadcastText { get; set; }
        public virtual DbSet<BroadcastTextLocale> BroadcastTextLocale { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Conditions> Conditions { get; set; }
        public virtual DbSet<Creature> Creature { get; set; }
        public virtual DbSet<CreatureAddon> CreatureAddon { get; set; }
        public virtual DbSet<CreatureClasslevelstats> CreatureClasslevelstats { get; set; }
        public virtual DbSet<CreatureDefaultTrainer> CreatureDefaultTrainer { get; set; }
        public virtual DbSet<CreatureEquipTemplate> CreatureEquipTemplate { get; set; }
        public virtual DbSet<CreatureFormations> CreatureFormations { get; set; }
        public virtual DbSet<CreatureLootTemplate> CreatureLootTemplate { get; set; }
        public virtual DbSet<CreatureModelInfo> CreatureModelInfo { get; set; }
        public virtual DbSet<CreatureMovementOverride> CreatureMovementOverride { get; set; }
        public virtual DbSet<CreatureOnkillReputation> CreatureOnkillReputation { get; set; }
        public virtual DbSet<CreatureQuestender> CreatureQuestender { get; set; }
        public virtual DbSet<CreatureQuestitem> CreatureQuestitem { get; set; }
        public virtual DbSet<CreatureQueststarter> CreatureQueststarter { get; set; }
        public virtual DbSet<CreatureSummonGroups> CreatureSummonGroups { get; set; }
        public virtual DbSet<CreatureTemplate> CreatureTemplate { get; set; }
        public virtual DbSet<CreatureTemplateAddon> CreatureTemplateAddon { get; set; }
        public virtual DbSet<CreatureTemplateLocale> CreatureTemplateLocale { get; set; }
        public virtual DbSet<CreatureTemplateMovement> CreatureTemplateMovement { get; set; }
        public virtual DbSet<CreatureTemplateResistance> CreatureTemplateResistance { get; set; }
        public virtual DbSet<CreatureTemplateSpell> CreatureTemplateSpell { get; set; }
        public virtual DbSet<CreatureText> CreatureText { get; set; }
        public virtual DbSet<CreatureTextLocale> CreatureTextLocale { get; set; }
        public virtual DbSet<Disables> Disables { get; set; }
        public virtual DbSet<DisenchantLootTemplate> DisenchantLootTemplate { get; set; }
        public virtual DbSet<EventScripts> EventScripts { get; set; }
        public virtual DbSet<ExplorationBasexp> ExplorationBasexp { get; set; }
        public virtual DbSet<FishingLootTemplate> FishingLootTemplate { get; set; }
        public virtual DbSet<GameEvent> GameEvent { get; set; }
        public virtual DbSet<GameEventArenaSeasons> GameEventArenaSeasons { get; set; }
        public virtual DbSet<GameEventBattlegroundHoliday> GameEventBattlegroundHoliday { get; set; }
        public virtual DbSet<GameEventCondition> GameEventCondition { get; set; }
        public virtual DbSet<GameEventCreature> GameEventCreature { get; set; }
        public virtual DbSet<GameEventCreatureQuest> GameEventCreatureQuest { get; set; }
        public virtual DbSet<GameEventGameobject> GameEventGameobject { get; set; }
        public virtual DbSet<GameEventGameobjectQuest> GameEventGameobjectQuest { get; set; }
        public virtual DbSet<GameEventModelEquip> GameEventModelEquip { get; set; }
        public virtual DbSet<GameEventNpcVendor> GameEventNpcVendor { get; set; }
        public virtual DbSet<GameEventNpcflag> GameEventNpcflag { get; set; }
        public virtual DbSet<GameEventPool> GameEventPool { get; set; }
        public virtual DbSet<GameEventPrerequisite> GameEventPrerequisite { get; set; }
        public virtual DbSet<GameEventQuestCondition> GameEventQuestCondition { get; set; }
        public virtual DbSet<GameEventSeasonalQuestrelation> GameEventSeasonalQuestrelation { get; set; }
        public virtual DbSet<GameTele> GameTele { get; set; }
        public virtual DbSet<GameWeather> GameWeather { get; set; }
        public virtual DbSet<Gameobject> Gameobject { get; set; }
        public virtual DbSet<GameobjectAddon> GameobjectAddon { get; set; }
        public virtual DbSet<GameobjectLootTemplate> GameobjectLootTemplate { get; set; }
        public virtual DbSet<GameobjectOverrides> GameobjectOverrides { get; set; }
        public virtual DbSet<GameobjectQuestender> GameobjectQuestender { get; set; }
        public virtual DbSet<GameobjectQuestitem> GameobjectQuestitem { get; set; }
        public virtual DbSet<GameobjectQueststarter> GameobjectQueststarter { get; set; }
        public virtual DbSet<GameobjectTemplate> GameobjectTemplate { get; set; }
        public virtual DbSet<GameobjectTemplateAddon> GameobjectTemplateAddon { get; set; }
        public virtual DbSet<GameobjectTemplateLocale> GameobjectTemplateLocale { get; set; }
        public virtual DbSet<GossipMenu> GossipMenu { get; set; }
        public virtual DbSet<GossipMenuOption> GossipMenuOption { get; set; }
        public virtual DbSet<GossipMenuOptionLocale> GossipMenuOptionLocale { get; set; }
        public virtual DbSet<GraveyardZone> GraveyardZone { get; set; }
        public virtual DbSet<HolidayDates> HolidayDates { get; set; }
        public virtual DbSet<InstanceEncounters> InstanceEncounters { get; set; }
        public virtual DbSet<InstanceSpawnGroups> InstanceSpawnGroups { get; set; }
        public virtual DbSet<InstanceTemplate> InstanceTemplate { get; set; }
        public virtual DbSet<ItemEnchantmentTemplate> ItemEnchantmentTemplate { get; set; }
        public virtual DbSet<ItemLootTemplate> ItemLootTemplate { get; set; }
        public virtual DbSet<ItemSetNames> ItemSetNames { get; set; }
        public virtual DbSet<ItemSetNamesLocale> ItemSetNamesLocale { get; set; }
        public virtual DbSet<ItemTemplate> ItemTemplate { get; set; }
        public virtual DbSet<ItemTemplateLocale> ItemTemplateLocale { get; set; }
        public virtual DbSet<LfgDungeonRewards> LfgDungeonRewards { get; set; }
        public virtual DbSet<LfgDungeonTemplate> LfgDungeonTemplate { get; set; }
        public virtual DbSet<LinkedRespawn> LinkedRespawn { get; set; }
        public virtual DbSet<MailLevelReward> MailLevelReward { get; set; }
        public virtual DbSet<MailLootTemplate> MailLootTemplate { get; set; }
        public virtual DbSet<MillingLootTemplate> MillingLootTemplate { get; set; }
        public virtual DbSet<NpcSpellclickSpells> NpcSpellclickSpells { get; set; }
        public virtual DbSet<NpcText> NpcText { get; set; }
        public virtual DbSet<NpcTextLocale> NpcTextLocale { get; set; }
        public virtual DbSet<NpcVendor> NpcVendor { get; set; }
        public virtual DbSet<OutdoorpvpTemplate> OutdoorpvpTemplate { get; set; }
        public virtual DbSet<PageText> PageText { get; set; }
        public virtual DbSet<PageTextLocale> PageTextLocale { get; set; }
        public virtual DbSet<PetLevelstats> PetLevelstats { get; set; }
        public virtual DbSet<PetNameGeneration> PetNameGeneration { get; set; }
        public virtual DbSet<PickpocketingLootTemplate> PickpocketingLootTemplate { get; set; }
        public virtual DbSet<PlayerClasslevelstats> PlayerClasslevelstats { get; set; }
        public virtual DbSet<PlayerFactionchangeAchievement> PlayerFactionchangeAchievement { get; set; }
        public virtual DbSet<PlayerFactionchangeItems> PlayerFactionchangeItems { get; set; }
        public virtual DbSet<PlayerFactionchangeQuests> PlayerFactionchangeQuests { get; set; }
        public virtual DbSet<PlayerFactionchangeReputations> PlayerFactionchangeReputations { get; set; }
        public virtual DbSet<PlayerFactionchangeSpells> PlayerFactionchangeSpells { get; set; }
        public virtual DbSet<PlayerFactionchangeTitles> PlayerFactionchangeTitles { get; set; }
        public virtual DbSet<PlayerLevelstats> PlayerLevelstats { get; set; }
        public virtual DbSet<PlayerTotemModel> PlayerTotemModel { get; set; }
        public virtual DbSet<PlayerXpForLevel> PlayerXpForLevel { get; set; }
        public virtual DbSet<Playercreateinfo> Playercreateinfo { get; set; }
        public virtual DbSet<PlayercreateinfoAction> PlayercreateinfoAction { get; set; }
        public virtual DbSet<PlayercreateinfoCastSpell> PlayercreateinfoCastSpell { get; set; }
        public virtual DbSet<PlayercreateinfoItem> PlayercreateinfoItem { get; set; }
        public virtual DbSet<PlayercreateinfoSkills> PlayercreateinfoSkills { get; set; }
        public virtual DbSet<PlayercreateinfoSpellCustom> PlayercreateinfoSpellCustom { get; set; }
        public virtual DbSet<PointsOfInterest> PointsOfInterest { get; set; }
        public virtual DbSet<PointsOfInterestLocale> PointsOfInterestLocale { get; set; }
        public virtual DbSet<PoolMembers> PoolMembers { get; set; }
        public virtual DbSet<PoolTemplate> PoolTemplate { get; set; }
        public virtual DbSet<ProspectingLootTemplate> ProspectingLootTemplate { get; set; }
        public virtual DbSet<QuestDetails> QuestDetails { get; set; }
        public virtual DbSet<QuestGreeting> QuestGreeting { get; set; }
        public virtual DbSet<QuestGreetingLocale> QuestGreetingLocale { get; set; }
        public virtual DbSet<QuestMailSender> QuestMailSender { get; set; }
        public virtual DbSet<QuestOfferReward> QuestOfferReward { get; set; }
        public virtual DbSet<QuestOfferRewardLocale> QuestOfferRewardLocale { get; set; }
        public virtual DbSet<QuestPoi> QuestPoi { get; set; }
        public virtual DbSet<QuestPoiPoints> QuestPoiPoints { get; set; }
        public virtual DbSet<QuestPoolMembers> QuestPoolMembers { get; set; }
        public virtual DbSet<QuestPoolTemplate> QuestPoolTemplate { get; set; }
        public virtual DbSet<QuestRequestItems> QuestRequestItems { get; set; }
        public virtual DbSet<QuestRequestItemsLocale> QuestRequestItemsLocale { get; set; }
        public virtual DbSet<QuestTemplate> QuestTemplate { get; set; }
        public virtual DbSet<QuestTemplateAddon> QuestTemplateAddon { get; set; }
        public virtual DbSet<QuestTemplateLocale> QuestTemplateLocale { get; set; }
        public virtual DbSet<ReferenceLootTemplate> ReferenceLootTemplate { get; set; }
        public virtual DbSet<ReputationRewardRate> ReputationRewardRate { get; set; }
        public virtual DbSet<ReputationSpilloverTemplate> ReputationSpilloverTemplate { get; set; }
        public virtual DbSet<ScriptSplineChainMeta> ScriptSplineChainMeta { get; set; }
        public virtual DbSet<ScriptSplineChainWaypoints> ScriptSplineChainWaypoints { get; set; }
        public virtual DbSet<ScriptWaypoint> ScriptWaypoint { get; set; }
        public virtual DbSet<SkillDiscoveryTemplate> SkillDiscoveryTemplate { get; set; }
        public virtual DbSet<SkillExtraItemTemplate> SkillExtraItemTemplate { get; set; }
        public virtual DbSet<SkillFishingBaseLevel> SkillFishingBaseLevel { get; set; }
        public virtual DbSet<SkillPerfectItemTemplate> SkillPerfectItemTemplate { get; set; }
        public virtual DbSet<SkinningLootTemplate> SkinningLootTemplate { get; set; }
        public virtual DbSet<SmartScripts> SmartScripts { get; set; }
        public virtual DbSet<SpawnGroup> SpawnGroup { get; set; }
        public virtual DbSet<SpawnGroupTemplate> SpawnGroupTemplate { get; set; }
        public virtual DbSet<SpellArea> SpellArea { get; set; }
        public virtual DbSet<SpellBonusData> SpellBonusData { get; set; }
        public virtual DbSet<SpellCustomAttr> SpellCustomAttr { get; set; }
        public virtual DbSet<SpellDbc> SpellDbc { get; set; }
        public virtual DbSet<SpellEnchantProcData> SpellEnchantProcData { get; set; }
        public virtual DbSet<SpellGroup> SpellGroup { get; set; }
        public virtual DbSet<SpellGroupStackRules> SpellGroupStackRules { get; set; }
        public virtual DbSet<SpellLearnSpell> SpellLearnSpell { get; set; }
        public virtual DbSet<SpellLinkedSpell> SpellLinkedSpell { get; set; }
        public virtual DbSet<SpellLootTemplate> SpellLootTemplate { get; set; }
        public virtual DbSet<SpellPetAuras> SpellPetAuras { get; set; }
        public virtual DbSet<SpellProc> SpellProc { get; set; }
        public virtual DbSet<SpellRanks> SpellRanks { get; set; }
        public virtual DbSet<SpellRequired> SpellRequired { get; set; }
        public virtual DbSet<SpellScriptNames> SpellScriptNames { get; set; }
        public virtual DbSet<SpellScripts> SpellScripts { get; set; }
        public virtual DbSet<SpellTargetPosition> SpellTargetPosition { get; set; }
        public virtual DbSet<SpellThreat> SpellThreat { get; set; }
        public virtual DbSet<SpelldifficultyDbc> SpelldifficultyDbc { get; set; }
        public virtual DbSet<Trainer> Trainer { get; set; }
        public virtual DbSet<TrainerLocale> TrainerLocale { get; set; }
        public virtual DbSet<TrainerSpell> TrainerSpell { get; set; }
        public virtual DbSet<Transports> Transports { get; set; }
        public virtual DbSet<TrinityString> TrinityString { get; set; }
        public virtual DbSet<Updates> Updates { get; set; }
        public virtual DbSet<UpdatesInclude> UpdatesInclude { get; set; }
        public virtual DbSet<VehicleAccessory> VehicleAccessory { get; set; }
        public virtual DbSet<VehicleSeatAddon> VehicleSeatAddon { get; set; }
        public virtual DbSet<VehicleTemplateAccessory> VehicleTemplateAccessory { get; set; }
        public virtual DbSet<Version> Version { get; set; }
        public virtual DbSet<VwConditionsWithLabels> VwConditionsWithLabels { get; set; }
        public virtual DbSet<VwDisablesWithLabels> VwDisablesWithLabels { get; set; }
        public virtual DbSet<VwSmartScriptsWithLabels> VwSmartScriptsWithLabels { get; set; }
        public virtual DbSet<WardenChecks> WardenChecks { get; set; }
        public virtual DbSet<WaypointData> WaypointData { get; set; }
        public virtual DbSet<WaypointScripts> WaypointScripts { get; set; }
        public virtual DbSet<Waypoints> Waypoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessRequirement>(entity =>
            {
                entity.HasKey(e => new { e.MapId, e.Difficulty })
                    .HasName("PRIMARY");

                entity.ToTable("access_requirement");

                entity.HasComment("Access Requirements");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompletedAchievement)
                    .HasColumnName("completed_achievement")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item2)
                    .HasColumnName("item2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemLevel)
                    .HasColumnName("item_level")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.LevelMax)
                    .HasColumnName("level_max")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LevelMin)
                    .HasColumnName("level_min")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.QuestDoneA)
                    .HasColumnName("quest_done_A")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.QuestDoneH)
                    .HasColumnName("quest_done_H")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.QuestFailedText)
                    .HasColumnName("quest_failed_text")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<AchievementCriteriaData>(entity =>
            {
                entity.HasKey(e => new { e.CriteriaId, e.Type })
                    .HasName("PRIMARY");

                entity.ToTable("achievement_criteria_data");

                entity.HasComment("Achievment system");

                entity.Property(e => e.CriteriaId)
                    .HasColumnName("criteria_id")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Value1)
                    .HasColumnName("value1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Value2)
                    .HasColumnName("value2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<AchievementDbc>(entity =>
            {
                entity.ToTable("achievement_dbc");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapID")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RefAchievement)
                    .HasColumnName("refAchievement")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RequiredFaction)
                    .HasColumnName("requiredFaction")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");
            });

            modelBuilder.Entity<AchievementReward>(entity =>
            {
                entity.ToTable("achievement_reward");

                entity.HasComment("Loot System");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Body)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MailTemplateId)
                    .HasColumnName("MailTemplateID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sender)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TitleA)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TitleH)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<AchievementRewardLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("achievement_reward_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Body)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Subject)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<AreatriggerInvolvedrelation>(entity =>
            {
                entity.ToTable("areatrigger_involvedrelation");

                entity.HasComment("Trigger System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<AreatriggerScripts>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("areatrigger_scripts");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<AreatriggerTavern>(entity =>
            {
                entity.ToTable("areatrigger_tavern");

                entity.HasComment("Trigger System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifier");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<AreatriggerTeleport>(entity =>
            {
                entity.ToTable("areatrigger_teleport");

                entity.HasComment("Trigger System");

                entity.HasIndex(e => e.Name)
                    .HasName("name");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TargetMap)
                    .HasColumnName("target_map")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.TargetOrientation).HasColumnName("target_orientation");

                entity.Property(e => e.TargetPositionX).HasColumnName("target_position_x");

                entity.Property(e => e.TargetPositionY).HasColumnName("target_position_y");

                entity.Property(e => e.TargetPositionZ).HasColumnName("target_position_z");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<BattlefieldTemplate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("battlefield_template");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TypeId).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<BattlegroundTemplate>(entity =>
            {
                entity.ToTable("battleground_template");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AllianceStartLoc).HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("char(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HordeStartLoc).HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.MaxLvl).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.MaxPlayersPerTeam).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MinLvl).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.MinPlayersPerTeam).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Weight)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<BattlemasterEntry>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("battlemaster_entry");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Entry of a creature");

                entity.Property(e => e.BgTemplate)
                    .HasColumnName("bg_template")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Battleground template id");
            });

            modelBuilder.Entity<BroadcastText>(entity =>
            {
                entity.ToTable("broadcast_text");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteDelay1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteDelay2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteDelay3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteId1)
                    .HasColumnName("EmoteID1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteId2)
                    .HasColumnName("EmoteID2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmoteId3)
                    .HasColumnName("EmoteID3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EmotesId)
                    .HasColumnName("EmotesID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Flags)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("LanguageID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SoundEntriesId)
                    .HasColumnName("SoundEntriesID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Text)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text1)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<BroadcastTextLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("broadcast_text_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text1)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("command");

                entity.HasComment("Chat System");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Help)
                    .HasColumnName("help")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Conditions>(entity =>
            {
                entity.HasKey(e => new { e.SourceTypeOrReferenceId, e.SourceGroup, e.SourceEntry, e.SourceId, e.ElseGroup, e.ConditionTypeOrReference, e.ConditionTarget, e.ConditionValue1, e.ConditionValue2, e.ConditionValue3 })
                    .HasName("PRIMARY");

                entity.ToTable("conditions");

                entity.HasComment("Condition System");

                entity.Property(e => e.SourceTypeOrReferenceId)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceGroup)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceEntry)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceId).HasColumnType("int(11)");

                entity.Property(e => e.ElseGroup)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionTypeOrReference)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionTarget).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ConditionValue1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionValue2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionValue3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ErrorTextId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ErrorType)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NegativeCondition).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Creature>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("creature");

                entity.HasComment("Creature System");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_id");

                entity.HasIndex(e => e.Map)
                    .HasName("idx_map");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.AreaId)
                    .HasColumnName("areaId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Area Identifier");

                entity.Property(e => e.Curhealth)
                    .HasColumnName("curhealth")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Curmana)
                    .HasColumnName("curmana")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Currentwaypoint)
                    .HasColumnName("currentwaypoint")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Dynamicflags)
                    .HasColumnName("dynamicflags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EquipmentId)
                    .HasColumnName("equipment_id")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Creature Identifier");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.Modelid)
                    .HasColumnName("modelid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MovementType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Npcflag)
                    .HasColumnName("npcflag")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PhaseMask)
                    .HasColumnName("phaseMask")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.ScriptName)
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpawnMask)
                    .HasColumnName("spawnMask")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Spawntimesecs)
                    .HasColumnName("spawntimesecs")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'120'");

                entity.Property(e => e.UnitFlags)
                    .HasColumnName("unit_flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.WanderDistance).HasColumnName("wander_distance");

                entity.Property(e => e.ZoneId)
                    .HasColumnName("zoneId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Zone Identifier");
            });

            modelBuilder.Entity<CreatureAddon>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("creature_addon");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Auras)
                    .HasColumnName("auras")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Bytes1)
                    .HasColumnName("bytes1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bytes2)
                    .HasColumnName("bytes2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Emote)
                    .HasColumnName("emote")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Mount)
                    .HasColumnName("mount")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PathId)
                    .HasColumnName("path_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VisibilityDistanceType)
                    .HasColumnName("visibilityDistanceType")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CreatureClasslevelstats>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.Class })
                    .HasName("PRIMARY");

                entity.ToTable("creature_classlevelstats");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Attackpower)
                    .HasColumnName("attackpower")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Basearmor)
                    .HasColumnName("basearmor")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Basehp0)
                    .HasColumnName("basehp0")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Basehp1)
                    .HasColumnName("basehp1")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Basehp2)
                    .HasColumnName("basehp2")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Basemana)
                    .HasColumnName("basemana")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DamageBase).HasColumnName("damage_base");

                entity.Property(e => e.DamageExp1).HasColumnName("damage_exp1");

                entity.Property(e => e.DamageExp2).HasColumnName("damage_exp2");

                entity.Property(e => e.Rangedattackpower)
                    .HasColumnName("rangedattackpower")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<CreatureDefaultTrainer>(entity =>
            {
                entity.HasKey(e => e.CreatureId)
                    .HasName("PRIMARY");

                entity.ToTable("creature_default_trainer");

                entity.Property(e => e.CreatureId).HasColumnType("int(11) unsigned");

                entity.Property(e => e.TrainerId).HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<CreatureEquipTemplate>(entity =>
            {
                entity.HasKey(e => new { e.CreatureId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("creature_equip_template");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("CreatureID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ItemId1)
                    .HasColumnName("ItemID1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemId2)
                    .HasColumnName("ItemID2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemId3)
                    .HasColumnName("ItemID3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureFormations>(entity =>
            {
                entity.HasKey(e => e.MemberGuid)
                    .HasName("PRIMARY");

                entity.ToTable("creature_formations");

                entity.Property(e => e.MemberGuid)
                    .HasColumnName("memberGUID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Angle)
                    .HasColumnName("angle")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.Dist)
                    .HasColumnName("dist")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.GroupAi)
                    .HasColumnName("groupAI")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LeaderGuid)
                    .HasColumnName("leaderGUID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Point1)
                    .HasColumnName("point_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Point2)
                    .HasColumnName("point_2")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<CreatureLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("creature_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureModelInfo>(entity =>
            {
                entity.HasKey(e => e.DisplayId)
                    .HasName("PRIMARY");

                entity.ToTable("creature_model_info");

                entity.HasComment("Creature System (Model related info)");

                entity.Property(e => e.DisplayId)
                    .HasColumnName("DisplayID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DisplayIdOtherGender)
                    .HasColumnName("DisplayID_Other_Gender")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Gender)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'");
            });

            modelBuilder.Entity<CreatureMovementOverride>(entity =>
            {
                entity.HasKey(e => e.SpawnId)
                    .HasName("PRIMARY");

                entity.ToTable("creature_movement_override");

                entity.Property(e => e.SpawnId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Chase).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flight).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Ground)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Random).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rooted).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Swim)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<CreatureOnkillReputation>(entity =>
            {
                entity.HasKey(e => e.CreatureId)
                    .HasName("PRIMARY");

                entity.ToTable("creature_onkill_reputation");

                entity.HasComment("Creature OnKill Reputation gain");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("creature_id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Creature Identifier");

                entity.Property(e => e.IsTeamAward1).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsTeamAward2).HasColumnType("tinyint(4)");

                entity.Property(e => e.MaxStanding1).HasColumnType("tinyint(4)");

                entity.Property(e => e.MaxStanding2).HasColumnType("tinyint(4)");

                entity.Property(e => e.RewOnKillRepFaction1).HasColumnType("smallint(6)");

                entity.Property(e => e.RewOnKillRepFaction2).HasColumnType("smallint(6)");

                entity.Property(e => e.RewOnKillRepValue1)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewOnKillRepValue2)
                    .HasColumnType("mediumint(9)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TeamDependent).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CreatureQuestender>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("creature_questender");

                entity.HasComment("Creature System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<CreatureQuestitem>(entity =>
            {
                entity.HasKey(e => new { e.CreatureEntry, e.Idx })
                    .HasName("PRIMARY");

                entity.ToTable("creature_questitem");

                entity.Property(e => e.CreatureEntry).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Idx).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ItemId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<CreatureQueststarter>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("creature_queststarter");

                entity.HasComment("Creature System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<CreatureSummonGroups>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("creature_summon_groups");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("groupId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.SummonTime)
                    .HasColumnName("summonTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SummonType)
                    .HasColumnName("summonType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SummonerId)
                    .HasColumnName("summonerId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SummonerType)
                    .HasColumnName("summonerType")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CreatureTemplate>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("creature_template");

                entity.HasComment("Creature System");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_name");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ainame)
                    .IsRequired()
                    .HasColumnName("AIName")
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ArmorModifier).HasDefaultValueSql("'1'");

                entity.Property(e => e.BaseAttackTime).HasColumnType("int(10) unsigned");

                entity.Property(e => e.BaseVariance).HasDefaultValueSql("'1'");

                entity.Property(e => e.DamageModifier).HasDefaultValueSql("'1'");

                entity.Property(e => e.DifficultyEntry1)
                    .HasColumnName("difficulty_entry_1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DifficultyEntry2)
                    .HasColumnName("difficulty_entry_2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DifficultyEntry3)
                    .HasColumnName("difficulty_entry_3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Dmgschool)
                    .HasColumnName("dmgschool")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Dynamicflags)
                    .HasColumnName("dynamicflags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Exp)
                    .HasColumnName("exp")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.ExperienceModifier).HasDefaultValueSql("'1'");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Family)
                    .HasColumnName("family")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.FlagsExtra)
                    .HasColumnName("flags_extra")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GossipMenuId)
                    .HasColumnName("gossip_menu_id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HealthModifier).HasDefaultValueSql("'1'");

                entity.Property(e => e.HoverHeight).HasDefaultValueSql("'1'");

                entity.Property(e => e.IconName)
                    .HasColumnType("char(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.KillCredit1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.KillCredit2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Lootid)
                    .HasColumnName("lootid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ManaModifier).HasDefaultValueSql("'1'");

                entity.Property(e => e.Maxgold)
                    .HasColumnName("maxgold")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Maxlevel)
                    .HasColumnName("maxlevel")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MechanicImmuneMask)
                    .HasColumnName("mechanic_immune_mask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Mingold)
                    .HasColumnName("mingold")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Minlevel)
                    .HasColumnName("minlevel")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Modelid1)
                    .HasColumnName("modelid1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Modelid2)
                    .HasColumnName("modelid2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Modelid3)
                    .HasColumnName("modelid3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Modelid4)
                    .HasColumnName("modelid4")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MovementId)
                    .HasColumnName("movementId")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.MovementType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("char(100)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Npcflag)
                    .HasColumnName("npcflag")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PetSpellDataId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pickpocketloot)
                    .HasColumnName("pickpocketloot")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RacialLeader).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RangeAttackTime).HasColumnType("int(10) unsigned");

                entity.Property(e => e.RangeVariance).HasDefaultValueSql("'1'");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RegenHealth)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Scale)
                    .HasColumnName("scale")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Skinloot)
                    .HasColumnName("skinloot")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SpeedRun)
                    .HasColumnName("speed_run")
                    .HasDefaultValueSql("'1.14286'")
                    .HasComment("Result of 8.0/7.0, most common value");

                entity.Property(e => e.SpeedWalk)
                    .HasColumnName("speed_walk")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Result of 2.5/2.5, most common value");

                entity.Property(e => e.SpellSchoolImmuneMask)
                    .HasColumnName("spell_school_immune_mask")
                    .HasColumnType("int(3) unsigned");

                entity.Property(e => e.Subname)
                    .HasColumnName("subname")
                    .HasColumnType("char(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TypeFlags)
                    .HasColumnName("type_flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UnitClass)
                    .HasColumnName("unit_class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.UnitFlags)
                    .HasColumnName("unit_flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UnitFlags2)
                    .HasColumnName("unit_flags2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VehicleId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureTemplateAddon>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("creature_template_addon");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Auras)
                    .HasColumnName("auras")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Bytes1)
                    .HasColumnName("bytes1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bytes2)
                    .HasColumnName("bytes2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Emote)
                    .HasColumnName("emote")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mount)
                    .HasColumnName("mount")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PathId)
                    .HasColumnName("path_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.VisibilityDistanceType)
                    .HasColumnName("visibilityDistanceType")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CreatureTemplateLocale>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("creature_template_locale");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureTemplateMovement>(entity =>
            {
                entity.HasKey(e => e.CreatureId)
                    .HasName("PRIMARY");

                entity.ToTable("creature_template_movement");

                entity.Property(e => e.CreatureId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Chase).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flight).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Ground)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Random).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rooted).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Swim)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<CreatureTemplateResistance>(entity =>
            {
                entity.HasKey(e => new { e.CreatureId, e.School })
                    .HasName("PRIMARY");

                entity.ToTable("creature_template_resistance");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("CreatureID")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.School).HasColumnType("tinyint(6) unsigned");

                entity.Property(e => e.Resistance).HasColumnType("smallint(6)");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureTemplateSpell>(entity =>
            {
                entity.HasKey(e => new { e.CreatureId, e.Index })
                    .HasName("PRIMARY");

                entity.ToTable("creature_template_spell");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("CreatureID")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Index).HasColumnType("tinyint(6) unsigned");

                entity.Property(e => e.Spell).HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CreatureText>(entity =>
            {
                entity.HasKey(e => new { e.CreatureId, e.GroupId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("creature_text");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("CreatureID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BroadcastTextId)
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Duration)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Emote)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Language).HasColumnType("tinyint(3)");

                entity.Property(e => e.Probability).HasColumnType("float unsigned");

                entity.Property(e => e.Sound)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Text)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TextRange).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Type).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CreatureTextLocale>(entity =>
            {
                entity.HasKey(e => new { e.CreatureId, e.GroupId, e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("creature_text_locale");

                entity.Property(e => e.CreatureId)
                    .HasColumnName("CreatureID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Disables>(entity =>
            {
                entity.HasKey(e => new { e.SourceType, e.Entry })
                    .HasName("PRIMARY");

                entity.ToTable("disables");

                entity.Property(e => e.SourceType)
                    .HasColumnName("sourceType")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("smallint(5)");

                entity.Property(e => e.Params0)
                    .IsRequired()
                    .HasColumnName("params_0")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Params1)
                    .IsRequired()
                    .HasColumnName("params_1")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DisenchantLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("disenchant_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<EventScripts>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("event_scripts");

                entity.Property(e => e.Command)
                    .HasColumnName("command")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dataint)
                    .HasColumnName("dataint")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Datalong)
                    .HasColumnName("datalong")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Datalong2)
                    .HasColumnName("datalong2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.O).HasColumnName("o");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<ExplorationBasexp>(entity =>
            {
                entity.HasKey(e => e.Level)
                    .HasName("PRIMARY");

                entity.ToTable("exploration_basexp");

                entity.HasComment("Exploration System");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Basexp)
                    .HasColumnName("basexp")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<FishingLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("fishing_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameEvent>(entity =>
            {
                entity.HasKey(e => e.EventEntry)
                    .HasName("PRIMARY");

                entity.ToTable("game_event");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");

                entity.Property(e => e.Announce)
                    .HasColumnName("announce")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'")
                    .HasComment("0 dont announce, 1 announce, 2 value from config");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasComment("Description of the event displayed in console")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("timestamp")
                    .HasComment("Absolute end date, the event will never start after");

                entity.Property(e => e.Holiday)
                    .HasColumnName("holiday")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Client side holiday id");

                entity.Property(e => e.HolidayStage)
                    .HasColumnName("holidayStage")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'2592000'")
                    .HasComment("Length in minutes of the event");

                entity.Property(e => e.Occurence)
                    .HasColumnName("occurence")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'5184000'")
                    .HasComment("Delay in minutes between occurences of the event");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("timestamp")
                    .HasComment("Absolute start date, the event will never start before");

                entity.Property(e => e.WorldEvent)
                    .HasColumnName("world_event")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("0 if normal event, 1 if world event");
            });

            modelBuilder.Entity<GameEventArenaSeasons>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_event_arena_seasons");

                entity.HasIndex(e => new { e.Season, e.EventEntry })
                    .HasName("season")
                    .IsUnique();

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");

                entity.Property(e => e.Season)
                    .HasColumnName("season")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Arena season number");
            });

            modelBuilder.Entity<GameEventBattlegroundHoliday>(entity =>
            {
                entity.HasKey(e => e.EventEntry)
                    .HasName("PRIMARY");

                entity.ToTable("game_event_battleground_holiday");

                entity.Property(e => e.EventEntry)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("game_event EventEntry identifier");

                entity.Property(e => e.BattlegroundId)
                    .HasColumnName("BattlegroundID")
                    .HasColumnType("int(3) unsigned");
            });

            modelBuilder.Entity<GameEventCondition>(entity =>
            {
                entity.HasKey(e => new { e.EventEntry, e.ConditionId })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_condition");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");

                entity.Property(e => e.ConditionId)
                    .HasColumnName("condition_id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(25)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DoneWorldStateField)
                    .HasColumnName("done_world_state_field")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MaxWorldStateField)
                    .HasColumnName("max_world_state_field")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ReqNum)
                    .HasColumnName("req_num")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameEventCreature>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.EventEntry })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_creature");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Entry of the game event. Put negative entry to remove during event.");
            });

            modelBuilder.Entity<GameEventCreatureQuest>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_creature_quest");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event.");
            });

            modelBuilder.Entity<GameEventGameobject>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.EventEntry })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_gameobject");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Entry of the game event. Put negative entry to remove during event.");
            });

            modelBuilder.Entity<GameEventGameobjectQuest>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest, e.EventEntry })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_gameobject_quest");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");
            });

            modelBuilder.Entity<GameEventModelEquip>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("game_event_model_equip");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EquipmentId)
                    .HasColumnName("equipment_id")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Entry of the game event.");

                entity.Property(e => e.Modelid)
                    .HasColumnName("modelid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameEventNpcVendor>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_npc_vendor");

                entity.HasIndex(e => e.Slot)
                    .HasName("slot");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Entry of the game event.");

                entity.Property(e => e.ExtendedCost)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Incrtime)
                    .HasColumnName("incrtime")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Maxcount)
                    .HasColumnName("maxcount")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Slot)
                    .HasColumnName("slot")
                    .HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<GameEventNpcflag>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.EventEntry })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_npcflag");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");

                entity.Property(e => e.Npcflag)
                    .HasColumnName("npcflag")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GameEventPool>(entity =>
            {
                entity.HasKey(e => e.PoolEntry)
                    .HasName("PRIMARY");

                entity.ToTable("game_event_pool");

                entity.Property(e => e.PoolEntry)
                    .HasColumnName("pool_entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Id of the pool");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Entry of the game event. Put negative entry to remove during event.");
            });

            modelBuilder.Entity<GameEventPrerequisite>(entity =>
            {
                entity.HasKey(e => new { e.EventEntry, e.PrerequisiteEvent })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_prerequisite");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event");

                entity.Property(e => e.PrerequisiteEvent)
                    .HasColumnName("prerequisite_event")
                    .HasColumnType("mediumint(8) unsigned");
            });

            modelBuilder.Entity<GameEventQuestCondition>(entity =>
            {
                entity.HasKey(e => e.Quest)
                    .HasName("PRIMARY");

                entity.ToTable("game_event_quest_condition");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionId)
                    .HasColumnName("condition_id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Entry of the game event.");

                entity.Property(e => e.Num)
                    .HasColumnName("num")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameEventSeasonalQuestrelation>(entity =>
            {
                entity.HasKey(e => new { e.QuestId, e.EventEntry })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_seasonal_questrelation");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.QuestId)
                    .HasName("idx_quest");

                entity.Property(e => e.QuestId)
                    .HasColumnName("questId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Entry of the game event");
            });

            modelBuilder.Entity<GameTele>(entity =>
            {
                entity.ToTable("game_tele");

                entity.HasComment("Tele Command");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");
            });

            modelBuilder.Entity<GameWeather>(entity =>
            {
                entity.HasKey(e => e.Zone)
                    .HasName("PRIMARY");

                entity.ToTable("game_weather");

                entity.HasComment("Weather System");

                entity.Property(e => e.Zone)
                    .HasColumnName("zone")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FallRainChance)
                    .HasColumnName("fall_rain_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.FallSnowChance)
                    .HasColumnName("fall_snow_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.FallStormChance)
                    .HasColumnName("fall_storm_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpringRainChance)
                    .HasColumnName("spring_rain_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.SpringSnowChance)
                    .HasColumnName("spring_snow_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.SpringStormChance)
                    .HasColumnName("spring_storm_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.SummerRainChance)
                    .HasColumnName("summer_rain_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.SummerSnowChance)
                    .HasColumnName("summer_snow_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.SummerStormChance)
                    .HasColumnName("summer_storm_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.WinterRainChance)
                    .HasColumnName("winter_rain_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.WinterSnowChance)
                    .HasColumnName("winter_snow_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");

                entity.Property(e => e.WinterStormChance)
                    .HasColumnName("winter_storm_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'25'");
            });

            modelBuilder.Entity<Gameobject>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("gameobject");

                entity.HasComment("Gameobject System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Animprogress)
                    .HasColumnName("animprogress")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.AreaId)
                    .HasColumnName("areaId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Area Identifier");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Gameobject Identifier");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PhaseMask)
                    .HasColumnName("phaseMask")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.Rotation0).HasColumnName("rotation0");

                entity.Property(e => e.Rotation1).HasColumnName("rotation1");

                entity.Property(e => e.Rotation2).HasColumnName("rotation2");

                entity.Property(e => e.Rotation3).HasColumnName("rotation3");

                entity.Property(e => e.ScriptName)
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpawnMask)
                    .HasColumnName("spawnMask")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Spawntimesecs)
                    .HasColumnName("spawntimesecs")
                    .HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ZoneId)
                    .HasColumnName("zoneId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Zone Identifier");
            });

            modelBuilder.Entity<GameobjectAddon>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_addon");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InvisibilityType)
                    .HasColumnName("invisibilityType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.InvisibilityValue)
                    .HasColumnName("invisibilityValue")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ParentRotation0).HasColumnName("parent_rotation0");

                entity.Property(e => e.ParentRotation1).HasColumnName("parent_rotation1");

                entity.Property(e => e.ParentRotation2).HasColumnName("parent_rotation2");

                entity.Property(e => e.ParentRotation3)
                    .HasColumnName("parent_rotation3")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<GameobjectLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameobjectOverrides>(entity =>
            {
                entity.HasKey(e => e.SpawnId)
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_overrides");

                entity.Property(e => e.SpawnId)
                    .HasColumnName("spawnId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GameobjectQuestender>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_questender");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<GameobjectQuestitem>(entity =>
            {
                entity.HasKey(e => new { e.GameObjectEntry, e.Idx })
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_questitem");

                entity.Property(e => e.GameObjectEntry).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Idx).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ItemId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<GameobjectQueststarter>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_queststarter");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<GameobjectTemplate>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_template");

                entity.HasComment("Gameobject System");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_name");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ainame)
                    .IsRequired()
                    .HasColumnName("AIName")
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CastBarCaption)
                    .IsRequired()
                    .HasColumnName("castBarCaption")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Data0).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data1).HasColumnType("int(11)");

                entity.Property(e => e.Data10).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data11).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data12).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data13).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data14).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data15).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data16).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data17).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data18).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data19).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data20).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data21).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data22).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data23).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data4).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data5).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data6).HasColumnType("int(11)");

                entity.Property(e => e.Data7).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data8).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data9).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DisplayId)
                    .HasColumnName("displayId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IconName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Unk1)
                    .IsRequired()
                    .HasColumnName("unk1")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameobjectTemplateAddon>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_template_addon");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Artkit0)
                    .HasColumnName("artkit0")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Artkit1)
                    .HasColumnName("artkit1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Artkit2)
                    .HasColumnName("artkit2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Artkit3)
                    .HasColumnName("artkit3")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxgold)
                    .HasColumnName("maxgold")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mingold)
                    .HasColumnName("mingold")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameobjectTemplateLocale>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("gameobject_template_locale");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CastBarCaption)
                    .HasColumnName("castBarCaption")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GossipMenu>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.TextId })
                    .HasName("PRIMARY");

                entity.ToTable("gossip_menu");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.TextId)
                    .HasColumnName("TextID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<GossipMenuOption>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.OptionId })
                    .HasName("PRIMARY");

                entity.ToTable("gossip_menu_option");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.OptionId)
                    .HasColumnName("OptionID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ActionMenuId)
                    .HasColumnName("ActionMenuID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionPoiId)
                    .HasColumnName("ActionPoiID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BoxBroadcastTextId)
                    .HasColumnName("BoxBroadcastTextID")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BoxCoded).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BoxMoney).HasColumnType("int(10) unsigned");

                entity.Property(e => e.BoxText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OptionBroadcastTextId)
                    .HasColumnName("OptionBroadcastTextID")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OptionIcon)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OptionNpcFlag).HasColumnType("int(10) unsigned");

                entity.Property(e => e.OptionText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OptionType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<GossipMenuOptionLocale>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.OptionId, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("gossip_menu_option_locale");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasColumnType("smallint(6) unsigned");

                entity.Property(e => e.OptionId)
                    .HasColumnName("OptionID")
                    .HasColumnType("smallint(6) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BoxText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OptionText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<GraveyardZone>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.GhostZone })
                    .HasName("PRIMARY");

                entity.ToTable("graveyard_zone");

                entity.HasComment("Trigger System");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GhostZone)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Faction).HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<HolidayDates>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.DateId })
                    .HasName("PRIMARY");

                entity.ToTable("holiday_dates");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DateId)
                    .HasColumnName("date_id")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.DateValue)
                    .HasColumnName("date_value")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HolidayDuration)
                    .HasColumnName("holiday_duration")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<InstanceEncounters>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("instance_encounters");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Unique entry from DungeonEncounter.dbc");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreditEntry)
                    .HasColumnName("creditEntry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreditType)
                    .HasColumnName("creditType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LastEncounterDungeon)
                    .HasColumnName("lastEncounterDungeon")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("If not 0, LfgDungeon.dbc entry for the instance it is last encounter in");
            });

            modelBuilder.Entity<InstanceSpawnGroups>(entity =>
            {
                entity.HasKey(e => new { e.InstanceMapId, e.BossStateId, e.SpawnGroupId, e.BossStates })
                    .HasName("PRIMARY");

                entity.ToTable("instance_spawn_groups");

                entity.Property(e => e.InstanceMapId)
                    .HasColumnName("instanceMapId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.BossStateId)
                    .HasColumnName("bossStateId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SpawnGroupId)
                    .HasColumnName("spawnGroupId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BossStates)
                    .HasColumnName("bossStates")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<InstanceTemplate>(entity =>
            {
                entity.HasKey(e => e.Map)
                    .HasName("PRIMARY");

                entity.ToTable("instance_template");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.AllowMount)
                    .HasColumnName("allowMount")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Parent)
                    .HasColumnName("parent")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Script)
                    .IsRequired()
                    .HasColumnName("script")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<ItemEnchantmentTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Ench })
                    .HasName("PRIMARY");

                entity.ToTable("item_enchantment_template");

                entity.HasComment("Item Random Enchantment System");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ench)
                    .HasColumnName("ench")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance)
                    .HasColumnName("chance")
                    .HasColumnType("float unsigned");
            });

            modelBuilder.Entity<ItemLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("item_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ItemSetNames>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("item_set_names");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.InventoryType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ItemSetNamesLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("item_set_names_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ItemTemplate>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("item_template");

                entity.HasComment("Item System");

                entity.HasIndex(e => e.Class)
                    .HasName("items_index");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_name");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AllowableClass)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.AllowableRace)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.AmmoType)
                    .HasColumnName("ammo_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ArcaneRes)
                    .HasColumnName("arcane_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Armor)
                    .HasColumnName("armor")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.BagFamily)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Block)
                    .HasColumnName("block")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Bonding)
                    .HasColumnName("bonding")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BuyCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.BuyPrice).HasColumnType("bigint(20)");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ContainerSlots).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1000'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DisenchantId)
                    .HasColumnName("DisenchantID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Displayid)
                    .HasColumnName("displayid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DmgMax1).HasColumnName("dmg_max1");

                entity.Property(e => e.DmgMax2).HasColumnName("dmg_max2");

                entity.Property(e => e.DmgMin1).HasColumnName("dmg_min1");

                entity.Property(e => e.DmgMin2).HasColumnName("dmg_min2");

                entity.Property(e => e.DmgType1)
                    .HasColumnName("dmg_type1")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.DmgType2)
                    .HasColumnName("dmg_type2")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FireRes)
                    .HasColumnName("fire_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.FlagsCustom)
                    .HasColumnName("flagsCustom")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.FlagsExtra).HasColumnType("int(10) unsigned");

                entity.Property(e => e.FoodType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.FrostRes)
                    .HasColumnName("frost_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.GemProperties)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HolidayId).HasColumnType("int(11) unsigned");

                entity.Property(e => e.HolyRes)
                    .HasColumnName("holy_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.InventoryType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ItemLevel).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ItemLimitCategory).HasColumnType("smallint(6)");

                entity.Property(e => e.Itemset)
                    .HasColumnName("itemset")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("LanguageID")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lockid)
                    .HasColumnName("lockid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Map).HasColumnType("smallint(6)");

                entity.Property(e => e.Material).HasColumnType("tinyint(4)");

                entity.Property(e => e.MaxDurability).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MaxMoneyLoot)
                    .HasColumnName("maxMoneyLoot")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxcount)
                    .HasColumnName("maxcount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinMoneyLoot)
                    .HasColumnName("minMoneyLoot")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NatureRes)
                    .HasColumnName("nature_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.PageMaterial).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.PageText)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Quality).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RandomProperty)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RandomSuffix)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredCityRank)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredDisenchantSkill)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.RequiredLevel).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RequiredReputationFaction).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredReputationRank).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredSkill).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredSkillRank).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Requiredhonorrank)
                    .HasColumnName("requiredhonorrank")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Requiredspell)
                    .HasColumnName("requiredspell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ScalingStatDistribution).HasColumnType("smallint(6)");

                entity.Property(e => e.ScalingStatValue).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SellPrice).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ShadowRes)
                    .HasColumnName("shadow_res")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Sheath)
                    .HasColumnName("sheath")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SocketBonus)
                    .HasColumnName("socketBonus")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SocketColor1)
                    .HasColumnName("socketColor_1")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SocketColor2)
                    .HasColumnName("socketColor_2")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SocketColor3)
                    .HasColumnName("socketColor_3")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SocketContent1)
                    .HasColumnName("socketContent_1")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SocketContent2)
                    .HasColumnName("socketContent_2")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SocketContent3)
                    .HasColumnName("socketContent_3")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SoundOverrideSubclass)
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcategory1)
                    .HasColumnName("spellcategory_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spellcategory2)
                    .HasColumnName("spellcategory_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spellcategory3)
                    .HasColumnName("spellcategory_3")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spellcategory4)
                    .HasColumnName("spellcategory_4")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spellcategory5)
                    .HasColumnName("spellcategory_5")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spellcategorycooldown1)
                    .HasColumnName("spellcategorycooldown_1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcategorycooldown2)
                    .HasColumnName("spellcategorycooldown_2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcategorycooldown3)
                    .HasColumnName("spellcategorycooldown_3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcategorycooldown4)
                    .HasColumnName("spellcategorycooldown_4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcategorycooldown5)
                    .HasColumnName("spellcategorycooldown_5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcharges1)
                    .HasColumnName("spellcharges_1")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Spellcharges2)
                    .HasColumnName("spellcharges_2")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Spellcharges3)
                    .HasColumnName("spellcharges_3")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Spellcharges4)
                    .HasColumnName("spellcharges_4")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Spellcharges5)
                    .HasColumnName("spellcharges_5")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Spellcooldown1)
                    .HasColumnName("spellcooldown_1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcooldown2)
                    .HasColumnName("spellcooldown_2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcooldown3)
                    .HasColumnName("spellcooldown_3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcooldown4)
                    .HasColumnName("spellcooldown_4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellcooldown5)
                    .HasColumnName("spellcooldown_5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Spellid1)
                    .HasColumnName("spellid_1")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Spellid2)
                    .HasColumnName("spellid_2")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Spellid3)
                    .HasColumnName("spellid_3")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Spellid4)
                    .HasColumnName("spellid_4")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Spellid5)
                    .HasColumnName("spellid_5")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SpellppmRate1).HasColumnName("spellppmRate_1");

                entity.Property(e => e.SpellppmRate2).HasColumnName("spellppmRate_2");

                entity.Property(e => e.SpellppmRate3).HasColumnName("spellppmRate_3");

                entity.Property(e => e.SpellppmRate4).HasColumnName("spellppmRate_4");

                entity.Property(e => e.SpellppmRate5).HasColumnName("spellppmRate_5");

                entity.Property(e => e.Spelltrigger1)
                    .HasColumnName("spelltrigger_1")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spelltrigger2)
                    .HasColumnName("spelltrigger_2")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spelltrigger3)
                    .HasColumnName("spelltrigger_3")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spelltrigger4)
                    .HasColumnName("spelltrigger_4")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spelltrigger5)
                    .HasColumnName("spelltrigger_5")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Stackable)
                    .HasColumnName("stackable")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Startquest)
                    .HasColumnName("startquest")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.StatType1)
                    .HasColumnName("stat_type1")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType10)
                    .HasColumnName("stat_type10")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType2)
                    .HasColumnName("stat_type2")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType3)
                    .HasColumnName("stat_type3")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType4)
                    .HasColumnName("stat_type4")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType5)
                    .HasColumnName("stat_type5")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType6)
                    .HasColumnName("stat_type6")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType7)
                    .HasColumnName("stat_type7")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType8)
                    .HasColumnName("stat_type8")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatType9)
                    .HasColumnName("stat_type9")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StatValue1)
                    .HasColumnName("stat_value1")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue10)
                    .HasColumnName("stat_value10")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue2)
                    .HasColumnName("stat_value2")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue3)
                    .HasColumnName("stat_value3")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue4)
                    .HasColumnName("stat_value4")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue5)
                    .HasColumnName("stat_value5")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue6)
                    .HasColumnName("stat_value6")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue7)
                    .HasColumnName("stat_value7")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue8)
                    .HasColumnName("stat_value8")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatValue9)
                    .HasColumnName("stat_value9")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.StatsCount).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Subclass)
                    .HasColumnName("subclass")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TotemCategory)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ItemTemplateLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("item_template_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<LfgDungeonRewards>(entity =>
            {
                entity.HasKey(e => new { e.DungeonId, e.MaxLevel })
                    .HasName("PRIMARY");

                entity.ToTable("lfg_dungeon_rewards");

                entity.Property(e => e.DungeonId)
                    .HasColumnName("dungeonId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Dungeon entry from dbc");

                entity.Property(e => e.MaxLevel)
                    .HasColumnName("maxLevel")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Max level at which this reward is rewarded");

                entity.Property(e => e.FirstQuestId)
                    .HasColumnName("firstQuestId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest id with rewards for first dungeon this day");

                entity.Property(e => e.OtherQuestId)
                    .HasColumnName("otherQuestId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest id with rewards for Nth dungeon this day");
            });

            modelBuilder.Entity<LfgDungeonTemplate>(entity =>
            {
                entity.HasKey(e => e.DungeonId)
                    .HasName("PRIMARY");

                entity.ToTable("lfg_dungeon_template");

                entity.Property(e => e.DungeonId)
                    .HasColumnName("dungeonId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Unique id from LFGDungeons.dbc");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<LinkedRespawn>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.LinkType })
                    .HasName("PRIMARY");

                entity.ToTable("linked_respawn");

                entity.HasComment("Creature Respawn Link System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("dependent creature");

                entity.Property(e => e.LinkType)
                    .HasColumnName("linkType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LinkedGuid)
                    .HasColumnName("linkedGuid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("master creature");
            });

            modelBuilder.Entity<MailLevelReward>(entity =>
            {
                entity.HasKey(e => new { e.Level, e.RaceMask })
                    .HasName("PRIMARY");

                entity.ToTable("mail_level_reward");

                entity.HasComment("Mail System");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RaceMask)
                    .HasColumnName("raceMask")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MailTemplateId)
                    .HasColumnName("mailTemplateId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SenderEntry)
                    .HasColumnName("senderEntry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<MailLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("mail_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<MillingLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("milling_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<NpcSpellclickSpells>(entity =>
            {
                entity.HasKey(e => new { e.NpcEntry, e.SpellId })
                    .HasName("PRIMARY");

                entity.ToTable("npc_spellclick_spells");

                entity.Property(e => e.NpcEntry)
                    .HasColumnName("npc_entry")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("reference to creature_template");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spell_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("spell which should be casted ");

                entity.Property(e => e.CastFlags)
                    .HasColumnName("cast_flags")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("first bit defines caster: 1=player, 0=creature; second bit defines target, same mapping as caster bit");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("relation with summoner: 0-no 1-friendly 2-raid 3-party player can click");
            });

            modelBuilder.Entity<NpcText>(entity =>
            {
                entity.ToTable("npc_text");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId0)
                    .HasColumnName("BroadcastTextID0")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId1)
                    .HasColumnName("BroadcastTextID1")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId2)
                    .HasColumnName("BroadcastTextID2")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId3)
                    .HasColumnName("BroadcastTextID3")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId4)
                    .HasColumnName("BroadcastTextID4")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId5)
                    .HasColumnName("BroadcastTextID5")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId6)
                    .HasColumnName("BroadcastTextID6")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BroadcastTextId7)
                    .HasColumnName("BroadcastTextID7")
                    .HasColumnType("mediumint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Emote00)
                    .HasColumnName("Emote0_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote01)
                    .HasColumnName("Emote0_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote02)
                    .HasColumnName("Emote0_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote10)
                    .HasColumnName("Emote1_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote11)
                    .HasColumnName("Emote1_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote12)
                    .HasColumnName("Emote1_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote20)
                    .HasColumnName("Emote2_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote21)
                    .HasColumnName("Emote2_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote22)
                    .HasColumnName("Emote2_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote30)
                    .HasColumnName("Emote3_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote31)
                    .HasColumnName("Emote3_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote32)
                    .HasColumnName("Emote3_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote40)
                    .HasColumnName("Emote4_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote41)
                    .HasColumnName("Emote4_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote42)
                    .HasColumnName("Emote4_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote50)
                    .HasColumnName("Emote5_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote51)
                    .HasColumnName("Emote5_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote52)
                    .HasColumnName("Emote5_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote60)
                    .HasColumnName("Emote6_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote61)
                    .HasColumnName("Emote6_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote62)
                    .HasColumnName("Emote6_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote70)
                    .HasColumnName("Emote7_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote71)
                    .HasColumnName("Emote7_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote72)
                    .HasColumnName("Emote7_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay00)
                    .HasColumnName("EmoteDelay0_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay01)
                    .HasColumnName("EmoteDelay0_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay02)
                    .HasColumnName("EmoteDelay0_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay10)
                    .HasColumnName("EmoteDelay1_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay11)
                    .HasColumnName("EmoteDelay1_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay12)
                    .HasColumnName("EmoteDelay1_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay20)
                    .HasColumnName("EmoteDelay2_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay21)
                    .HasColumnName("EmoteDelay2_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay22)
                    .HasColumnName("EmoteDelay2_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay30)
                    .HasColumnName("EmoteDelay3_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay31)
                    .HasColumnName("EmoteDelay3_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay32)
                    .HasColumnName("EmoteDelay3_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay40)
                    .HasColumnName("EmoteDelay4_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay41)
                    .HasColumnName("EmoteDelay4_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay42)
                    .HasColumnName("EmoteDelay4_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay50)
                    .HasColumnName("EmoteDelay5_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay51)
                    .HasColumnName("EmoteDelay5_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay52)
                    .HasColumnName("EmoteDelay5_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay60)
                    .HasColumnName("EmoteDelay6_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay61)
                    .HasColumnName("EmoteDelay6_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay62)
                    .HasColumnName("EmoteDelay6_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay70)
                    .HasColumnName("EmoteDelay7_0")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay71)
                    .HasColumnName("EmoteDelay7_1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay72)
                    .HasColumnName("EmoteDelay7_2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Lang0)
                    .HasColumnName("lang0")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang1)
                    .HasColumnName("lang1")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang2)
                    .HasColumnName("lang2")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang3)
                    .HasColumnName("lang3")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang4)
                    .HasColumnName("lang4")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang5)
                    .HasColumnName("lang5")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang6)
                    .HasColumnName("lang6")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Lang7)
                    .HasColumnName("lang7")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Text00)
                    .HasColumnName("text0_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text01)
                    .HasColumnName("text0_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text10)
                    .HasColumnName("text1_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text11)
                    .HasColumnName("text1_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text20)
                    .HasColumnName("text2_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text21)
                    .HasColumnName("text2_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text30)
                    .HasColumnName("text3_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text31)
                    .HasColumnName("text3_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text40)
                    .HasColumnName("text4_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text41)
                    .HasColumnName("text4_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text50)
                    .HasColumnName("text5_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text51)
                    .HasColumnName("text5_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text60)
                    .HasColumnName("text6_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text61)
                    .HasColumnName("text6_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text70)
                    .HasColumnName("text7_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text71)
                    .HasColumnName("text7_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<NpcTextLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("npc_text_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text00)
                    .HasColumnName("Text0_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text01)
                    .HasColumnName("Text0_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text10)
                    .HasColumnName("Text1_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text11)
                    .HasColumnName("Text1_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text20)
                    .HasColumnName("Text2_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text21)
                    .HasColumnName("Text2_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text30)
                    .HasColumnName("Text3_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text31)
                    .HasColumnName("Text3_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text40)
                    .HasColumnName("Text4_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text41)
                    .HasColumnName("Text4_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text50)
                    .HasColumnName("Text5_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text51)
                    .HasColumnName("Text5_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text60)
                    .HasColumnName("Text6_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text61)
                    .HasColumnName("Text6_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text70)
                    .HasColumnName("Text7_0")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text71)
                    .HasColumnName("Text7_1")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<NpcVendor>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item, e.ExtendedCost })
                    .HasName("PRIMARY");

                entity.ToTable("npc_vendor");

                entity.HasComment("Npc System");

                entity.HasIndex(e => e.Slot)
                    .HasName("slot");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ExtendedCost)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Incrtime)
                    .HasColumnName("incrtime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxcount)
                    .HasColumnName("maxcount")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Slot)
                    .HasColumnName("slot")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<OutdoorpvpTemplate>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PRIMARY");

                entity.ToTable("outdoorpvp_template");

                entity.HasComment("OutdoorPvP Templates");

                entity.Property(e => e.TypeId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<PageText>(entity =>
            {
                entity.ToTable("page_text");

                entity.HasComment("Item System");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NextPageId)
                    .HasColumnName("NextPageID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PageTextLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("page_text_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PetLevelstats>(entity =>
            {
                entity.HasKey(e => new { e.CreatureEntry, e.Level })
                    .HasName("PRIMARY");

                entity.ToTable("pet_levelstats");

                entity.HasComment("Stores pet levels stats.");

                entity.Property(e => e.CreatureEntry)
                    .HasColumnName("creature_entry")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Agi)
                    .HasColumnName("agi")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Armor)
                    .HasColumnName("armor")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Hp)
                    .HasColumnName("hp")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Inte)
                    .HasColumnName("inte")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Mana)
                    .HasColumnName("mana")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MaxDmg)
                    .HasColumnName("max_dmg")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MinDmg)
                    .HasColumnName("min_dmg")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Spi)
                    .HasColumnName("spi")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Sta)
                    .HasColumnName("sta")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Str)
                    .HasColumnName("str")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<PetNameGeneration>(entity =>
            {
                entity.ToTable("pet_name_generation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Half)
                    .HasColumnName("half")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasColumnName("word")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<PickpocketingLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("pickpocketing_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PlayerClasslevelstats>(entity =>
            {
                entity.HasKey(e => new { e.Class, e.Level })
                    .HasName("PRIMARY");

                entity.ToTable("player_classlevelstats");

                entity.HasComment("Stores levels stats.");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Basehp)
                    .HasColumnName("basehp")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Basemana)
                    .HasColumnName("basemana")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeAchievement>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_achievement");

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeItems>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_items");

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CommentA)
                    .HasColumnName("commentA")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentH)
                    .HasColumnName("commentH")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RaceA)
                    .HasColumnName("race_A")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RaceH)
                    .HasColumnName("race_H")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeQuests>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_quests");

                entity.HasIndex(e => e.AllianceId)
                    .HasName("alliance_uniq")
                    .IsUnique();

                entity.HasIndex(e => e.HordeId)
                    .HasName("horde_uniq")
                    .IsUnique();

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeReputations>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_reputations");

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeSpells>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_spells");

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerFactionchangeTitles>(entity =>
            {
                entity.HasKey(e => new { e.AllianceId, e.HordeId })
                    .HasName("PRIMARY");

                entity.ToTable("player_factionchange_titles");

                entity.Property(e => e.AllianceId)
                    .HasColumnName("alliance_id")
                    .HasColumnType("int(8)");

                entity.Property(e => e.HordeId)
                    .HasColumnName("horde_id")
                    .HasColumnType("int(8)");
            });

            modelBuilder.Entity<PlayerLevelstats>(entity =>
            {
                entity.HasKey(e => new { e.Race, e.Class, e.Level })
                    .HasName("PRIMARY");

                entity.ToTable("player_levelstats");

                entity.HasComment("Stores levels stats.");

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Agi)
                    .HasColumnName("agi")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Inte)
                    .HasColumnName("inte")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Spi)
                    .HasColumnName("spi")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Sta)
                    .HasColumnName("sta")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Str)
                    .HasColumnName("str")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<PlayerTotemModel>(entity =>
            {
                entity.HasKey(e => new { e.TotemSlot, e.RaceId })
                    .HasName("PRIMARY");

                entity.ToTable("player_totem_model");

                entity.Property(e => e.TotemSlot).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RaceId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.DisplayId).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PlayerXpForLevel>(entity =>
            {
                entity.HasKey(e => e.Level)
                    .HasName("PRIMARY");

                entity.ToTable("player_xp_for_level");

                entity.Property(e => e.Level).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Experience).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Playercreateinfo>(entity =>
            {
                entity.HasKey(e => new { e.Race, e.Class })
                    .HasName("PRIMARY");

                entity.ToTable("playercreateinfo");

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.Zone)
                    .HasColumnName("zone")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PlayercreateinfoAction>(entity =>
            {
                entity.HasKey(e => new { e.Race, e.Class, e.Button })
                    .HasName("PRIMARY");

                entity.ToTable("playercreateinfo_action");

                entity.HasIndex(e => new { e.Race, e.Class })
                    .HasName("playercreateinfo_race_class_index");

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Button)
                    .HasColumnName("button")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<PlayercreateinfoCastSpell>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("playercreateinfo_cast_spell");

                entity.Property(e => e.ClassMask)
                    .HasColumnName("classMask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RaceMask)
                    .HasColumnName("raceMask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PlayercreateinfoItem>(entity =>
            {
                entity.HasKey(e => new { e.Race, e.Class, e.Itemid })
                    .HasName("PRIMARY");

                entity.ToTable("playercreateinfo_item");

                entity.HasIndex(e => new { e.Race, e.Class })
                    .HasName("playercreateinfo_race_class_index");

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<PlayercreateinfoSkills>(entity =>
            {
                entity.HasKey(e => new { e.RaceMask, e.ClassMask, e.Skill })
                    .HasName("PRIMARY");

                entity.ToTable("playercreateinfo_skills");

                entity.Property(e => e.RaceMask)
                    .HasColumnName("raceMask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ClassMask)
                    .HasColumnName("classMask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Skill)
                    .HasColumnName("skill")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<PlayercreateinfoSpellCustom>(entity =>
            {
                entity.HasKey(e => new { e.Racemask, e.Classmask, e.Spell })
                    .HasName("PRIMARY");

                entity.ToTable("playercreateinfo_spell_custom");

                entity.Property(e => e.Racemask)
                    .HasColumnName("racemask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Classmask)
                    .HasColumnName("classmask")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spell)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Note)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<PointsOfInterest>(entity =>
            {
                entity.ToTable("points_of_interest");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Flags)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Icon)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Importance)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PointsOfInterestLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("points_of_interest_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PoolMembers>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.SpawnId })
                    .HasName("PRIMARY");

                entity.ToTable("pool_members");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint(10) unsigned");

                entity.Property(e => e.SpawnId)
                    .HasColumnName("spawnId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Chance).HasColumnName("chance");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PoolSpawnId)
                    .HasColumnName("poolSpawnId")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PoolTemplate>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("pool_template");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Pool entry");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MaxLimit)
                    .HasColumnName("max_limit")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Max number of objects (0) is no limit");
            });

            modelBuilder.Entity<ProspectingLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("prospecting_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<QuestDetails>(entity =>
            {
                entity.ToTable("quest_details");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Emote1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay4).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<QuestGreeting>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Type })
                    .HasName("PRIMARY");

                entity.ToTable("quest_greeting");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Type).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.GreetEmoteDelay).HasColumnType("int(10) unsigned");

                entity.Property(e => e.GreetEmoteType).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Greeting)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<QuestGreetingLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Type, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("quest_greeting_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Type).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Greeting)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<QuestMailSender>(entity =>
            {
                entity.HasKey(e => e.QuestId)
                    .HasName("PRIMARY");

                entity.ToTable("quest_mail_sender");

                entity.Property(e => e.QuestId).HasColumnType("int(5) unsigned");

                entity.Property(e => e.RewardMailSenderEntry).HasColumnType("int(5) unsigned");
            });

            modelBuilder.Entity<QuestOfferReward>(entity =>
            {
                entity.ToTable("quest_offer_reward");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Emote1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Emote4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteDelay1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmoteDelay4).HasColumnType("int(10) unsigned");

                entity.Property(e => e.RewardText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<QuestOfferRewardLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("quest_offer_reward_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RewardText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<QuestPoi>(entity =>
            {
                entity.HasKey(e => new { e.QuestId, e.Id })
                    .HasName("PRIMARY");

                entity.ToTable("quest_poi");

                entity.HasIndex(e => new { e.QuestId, e.Id })
                    .HasName("idx");

                entity.Property(e => e.QuestId)
                    .HasColumnName("QuestID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Flags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Floor).HasColumnType("int(10) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("MapID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ObjectiveIndex).HasColumnType("int(11)");

                entity.Property(e => e.Priority).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.WorldMapAreaId).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<QuestPoiPoints>(entity =>
            {
                entity.HasKey(e => new { e.QuestId, e.Idx1, e.Idx2 })
                    .HasName("PRIMARY");

                entity.ToTable("quest_poi_points");

                entity.HasIndex(e => new { e.QuestId, e.Idx1 })
                    .HasName("questId_id");

                entity.Property(e => e.QuestId)
                    .HasColumnName("QuestID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Idx1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Idx2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.X).HasColumnType("int(11)");

                entity.Property(e => e.Y).HasColumnType("int(11)");
            });

            modelBuilder.Entity<QuestPoolMembers>(entity =>
            {
                entity.HasKey(e => e.QuestId)
                    .HasName("PRIMARY");

                entity.ToTable("quest_pool_members");

                entity.Property(e => e.QuestId)
                    .HasColumnName("questId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PoolId)
                    .HasColumnName("poolId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PoolIndex)
                    .HasColumnName("poolIndex")
                    .HasColumnType("tinyint(2) unsigned")
                    .HasComment("Multiple quests with the same index will always spawn together!");
            });

            modelBuilder.Entity<QuestPoolTemplate>(entity =>
            {
                entity.HasKey(e => e.PoolId)
                    .HasName("PRIMARY");

                entity.ToTable("quest_pool_template");

                entity.Property(e => e.PoolId)
                    .HasColumnName("poolId")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NumActive)
                    .HasColumnName("numActive")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Number of indices to have active at any time");
            });

            modelBuilder.Entity<QuestRequestItems>(entity =>
            {
                entity.ToTable("quest_request_items");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CompletionText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EmoteOnComplete).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EmoteOnIncomplete).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(5)");
            });

            modelBuilder.Entity<QuestRequestItemsLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("quest_request_items_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompletionText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild).HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<QuestTemplate>(entity =>
            {
                entity.ToTable("quest_template");

                entity.HasComment("Quest System");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AllowableRaces).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.AreaDescription)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Flags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ItemDrop1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemDrop2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemDrop3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemDrop4)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ItemDropQuantity1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ItemDropQuantity2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ItemDropQuantity3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ItemDropQuantity4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.LogDescription)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LogTitle)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MinLevel).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ObjectiveText1)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText2)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText3)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText4)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Poicontinent)
                    .HasColumnName("POIContinent")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Poipriority)
                    .HasColumnName("POIPriority")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Poix).HasColumnName("POIx");

                entity.Property(e => e.Poiy).HasColumnName("POIy");

                entity.Property(e => e.QuestCompletionLog)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestDescription)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestInfoId)
                    .HasColumnName("QuestInfoID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.QuestLevel)
                    .HasColumnType("smallint(3)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestSortId)
                    .HasColumnName("QuestSortID")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.QuestType)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.RequiredFactionId1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredFactionId2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredFactionValue1)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredFactionValue2)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemCount1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemCount2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemCount3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemCount4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemCount5).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemCount6).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredItemId1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemId2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemId3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemId4)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemId5)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredItemId6)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredNpcOrGo1)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredNpcOrGo2)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredNpcOrGo3)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredNpcOrGo4)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredNpcOrGoCount1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredNpcOrGoCount2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredNpcOrGoCount3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredNpcOrGoCount4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredPlayerKills).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RewardAmount1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardAmount2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardAmount3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardAmount4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardArenaPoints).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardBonusMoney).HasColumnType("int(10) unsigned");

                entity.Property(e => e.RewardChoiceItemId1)
                    .HasColumnName("RewardChoiceItemID1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemId2)
                    .HasColumnName("RewardChoiceItemID2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemId3)
                    .HasColumnName("RewardChoiceItemID3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemId4)
                    .HasColumnName("RewardChoiceItemID4")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemId5)
                    .HasColumnName("RewardChoiceItemID5")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemId6)
                    .HasColumnName("RewardChoiceItemID6")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardChoiceItemQuantity1).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardChoiceItemQuantity2).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardChoiceItemQuantity3).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardChoiceItemQuantity4).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardChoiceItemQuantity5).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardChoiceItemQuantity6).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardDisplaySpell)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionId1)
                    .HasColumnName("RewardFactionID1")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction id from Faction.dbc in this case");

                entity.Property(e => e.RewardFactionId2)
                    .HasColumnName("RewardFactionID2")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction id from Faction.dbc in this case");

                entity.Property(e => e.RewardFactionId3)
                    .HasColumnName("RewardFactionID3")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction id from Faction.dbc in this case");

                entity.Property(e => e.RewardFactionId4)
                    .HasColumnName("RewardFactionID4")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction id from Faction.dbc in this case");

                entity.Property(e => e.RewardFactionId5)
                    .HasColumnName("RewardFactionID5")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction id from Faction.dbc in this case");

                entity.Property(e => e.RewardFactionOverride1)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionOverride2)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionOverride3)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionOverride4)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionOverride5)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionValue1)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionValue2)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionValue3)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionValue4)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardFactionValue5)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardHonor).HasColumnType("int(11)");

                entity.Property(e => e.RewardItem1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardItem2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardItem3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardItem4)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardMoney).HasColumnType("int(11)");

                entity.Property(e => e.RewardNextQuest)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RewardSpell).HasColumnType("int(11)");

                entity.Property(e => e.RewardTalents).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RewardTitle).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RewardXpdifficulty)
                    .HasColumnName("RewardXPDifficulty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StartItem)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SuggestedGroupNum).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TimeAllowed).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Unknown0).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<QuestTemplateAddon>(entity =>
            {
                entity.ToTable("quest_template_addon");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AllowableClasses).HasColumnType("int(10) unsigned");

                entity.Property(e => e.BreadcrumbForQuestId)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ExclusiveGroup)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MaxLevel).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.NextQuestId)
                    .HasColumnName("NextQuestID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PrevQuestId)
                    .HasColumnName("PrevQuestID")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ProvidedItemCount).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RequiredMaxRepFaction).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredMaxRepValue)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredMinRepFaction).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredMinRepValue)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RequiredSkillId)
                    .HasColumnName("RequiredSkillID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RequiredSkillPoints).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.RewardMailDelay).HasColumnType("int(10) unsigned");

                entity.Property(e => e.RewardMailTemplateId)
                    .HasColumnName("RewardMailTemplateID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceSpellId)
                    .HasColumnName("SourceSpellID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SpecialFlags).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<QuestTemplateLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("quest_template_locale");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompletedText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Details)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EndText)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText1)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText2)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText3)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObjectiveText4)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Objectives)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ReferenceLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("reference_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ReputationRewardRate>(entity =>
            {
                entity.HasKey(e => e.Faction)
                    .HasName("PRIMARY");

                entity.ToTable("reputation_reward_rate");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CreatureRate)
                    .HasColumnName("creature_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestDailyRate)
                    .HasColumnName("quest_daily_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestMonthlyRate)
                    .HasColumnName("quest_monthly_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestRate)
                    .HasColumnName("quest_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestRepeatableRate)
                    .HasColumnName("quest_repeatable_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.QuestWeeklyRate)
                    .HasColumnName("quest_weekly_rate")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SpellRate)
                    .HasColumnName("spell_rate")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<ReputationSpilloverTemplate>(entity =>
            {
                entity.HasKey(e => e.Faction)
                    .HasName("PRIMARY");

                entity.ToTable("reputation_spillover_template");

                entity.HasComment("Reputation spillover reputation gain");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction entry");

                entity.Property(e => e.Faction1)
                    .HasColumnName("faction1")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("faction to give spillover for");

                entity.Property(e => e.Faction2)
                    .HasColumnName("faction2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Faction3)
                    .HasColumnName("faction3")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Faction4)
                    .HasColumnName("faction4")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Rank1)
                    .HasColumnName("rank_1")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("max rank,above this will not give any spillover");

                entity.Property(e => e.Rank2)
                    .HasColumnName("rank_2")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rank3)
                    .HasColumnName("rank_3")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rank4)
                    .HasColumnName("rank_4")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rate1)
                    .HasColumnName("rate_1")
                    .HasComment("the given rep points * rate");

                entity.Property(e => e.Rate2).HasColumnName("rate_2");

                entity.Property(e => e.Rate3).HasColumnName("rate_3");

                entity.Property(e => e.Rate4).HasColumnName("rate_4");
            });

            modelBuilder.Entity<ScriptSplineChainMeta>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.ChainId, e.SplineId })
                    .HasName("PRIMARY");

                entity.ToTable("script_spline_chain_meta");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ChainId)
                    .HasColumnName("chainId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SplineId)
                    .HasColumnName("splineId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ExpectedDuration)
                    .HasColumnName("expectedDuration")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MsUntilNext)
                    .HasColumnName("msUntilNext")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Velocity)
                    .HasColumnName("velocity")
                    .HasColumnType("float unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ScriptSplineChainWaypoints>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.ChainId, e.SplineId, e.WpId })
                    .HasName("PRIMARY");

                entity.ToTable("script_spline_chain_waypoints");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ChainId)
                    .HasColumnName("chainId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SplineId)
                    .HasColumnName("splineId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.WpId)
                    .HasColumnName("wpId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<ScriptWaypoint>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Pointid })
                    .HasName("PRIMARY");

                entity.ToTable("script_waypoint");

                entity.HasComment("Script Creature waypoints");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("creature_template entry");

                entity.Property(e => e.Pointid)
                    .HasColumnName("pointid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LocationX).HasColumnName("location_x");

                entity.Property(e => e.LocationY).HasColumnName("location_y");

                entity.Property(e => e.LocationZ).HasColumnName("location_z");

                entity.Property(e => e.PointComment)
                    .HasColumnName("point_comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Waittime)
                    .HasColumnName("waittime")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("waittime in millisecs");
            });

            modelBuilder.Entity<SkillDiscoveryTemplate>(entity =>
            {
                entity.HasKey(e => new { e.SpellId, e.ReqSpell })
                    .HasName("PRIMARY");

                entity.ToTable("skill_discovery_template");

                entity.HasComment("Skill Discovery System");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spellId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("SpellId of the discoverable spell");

                entity.Property(e => e.ReqSpell)
                    .HasColumnName("reqSpell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("spell requirement");

                entity.Property(e => e.Chance)
                    .HasColumnName("chance")
                    .HasComment("chance to discover");

                entity.Property(e => e.ReqSkillValue)
                    .HasColumnName("reqSkillValue")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("skill points requirement");
            });

            modelBuilder.Entity<SkillExtraItemTemplate>(entity =>
            {
                entity.HasKey(e => e.SpellId)
                    .HasName("PRIMARY");

                entity.ToTable("skill_extra_item_template");

                entity.HasComment("Skill Specialization System");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spellId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("SpellId of the item creation spell");

                entity.Property(e => e.AdditionalCreateChance)
                    .HasColumnName("additionalCreateChance")
                    .HasComment("chance to create add");

                entity.Property(e => e.AdditionalMaxNum)
                    .HasColumnName("additionalMaxNum")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("max num of adds");

                entity.Property(e => e.RequiredSpecialization)
                    .HasColumnName("requiredSpecialization")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Specialization spell id");
            });

            modelBuilder.Entity<SkillFishingBaseLevel>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("skill_fishing_base_level");

                entity.HasComment("Fishing system");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Area identifier");

                entity.Property(e => e.Skill)
                    .HasColumnName("skill")
                    .HasColumnType("smallint(6)")
                    .HasComment("Base skill level requirement");
            });

            modelBuilder.Entity<SkillPerfectItemTemplate>(entity =>
            {
                entity.HasKey(e => e.SpellId)
                    .HasName("PRIMARY");

                entity.ToTable("skill_perfect_item_template");

                entity.HasComment("Crafting Perfection System");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spellId")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("SpellId of the item creation spell");

                entity.Property(e => e.PerfectCreateChance)
                    .HasColumnName("perfectCreateChance")
                    .HasComment("chance to create the perfect item instead");

                entity.Property(e => e.PerfectItemType)
                    .HasColumnName("perfectItemType")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("perfect item type to create instead");

                entity.Property(e => e.RequiredSpecialization)
                    .HasColumnName("requiredSpecialization")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Specialization spell id");
            });

            modelBuilder.Entity<SkinningLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("skinning_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<SmartScripts>(entity =>
            {
                entity.HasKey(e => new { e.Entryorguid, e.SourceType, e.Id, e.Link })
                    .HasName("PRIMARY");

                entity.ToTable("smart_scripts");

                entity.Property(e => e.Entryorguid)
                    .HasColumnName("entryorguid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SourceType)
                    .HasColumnName("source_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.ActionParam1)
                    .HasColumnName("action_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam2)
                    .HasColumnName("action_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam3)
                    .HasColumnName("action_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam4)
                    .HasColumnName("action_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam5)
                    .HasColumnName("action_param5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam6)
                    .HasColumnName("action_param6")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionType)
                    .HasColumnName("action_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasComment("Event Comment")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EventChance)
                    .HasColumnName("event_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.EventFlags)
                    .HasColumnName("event_flags")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EventParam1)
                    .HasColumnName("event_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam2)
                    .HasColumnName("event_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam3)
                    .HasColumnName("event_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam4)
                    .HasColumnName("event_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam5)
                    .HasColumnName("event_param5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventPhaseMask)
                    .HasColumnName("event_phase_mask")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TargetO).HasColumnName("target_o");

                entity.Property(e => e.TargetParam1)
                    .HasColumnName("target_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam2)
                    .HasColumnName("target_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam3)
                    .HasColumnName("target_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam4)
                    .HasColumnName("target_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetType)
                    .HasColumnName("target_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TargetX).HasColumnName("target_x");

                entity.Property(e => e.TargetY).HasColumnName("target_y");

                entity.Property(e => e.TargetZ).HasColumnName("target_z");
            });

            modelBuilder.Entity<SpawnGroup>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.SpawnType, e.SpawnId })
                    .HasName("PRIMARY");

                entity.ToTable("spawn_group");

                entity.Property(e => e.GroupId)
                    .HasColumnName("groupId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpawnType)
                    .HasColumnName("spawnType")
                    .HasColumnType("tinyint(10) unsigned");

                entity.Property(e => e.SpawnId)
                    .HasColumnName("spawnId")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpawnGroupTemplate>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PRIMARY");

                entity.ToTable("spawn_group_template");

                entity.Property(e => e.GroupId)
                    .HasColumnName("groupId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GroupFlags)
                    .HasColumnName("groupFlags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("groupName")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<SpellArea>(entity =>
            {
                entity.HasKey(e => new { e.Spell, e.Area, e.QuestStart, e.AuraSpell, e.Racemask, e.Gender })
                    .HasName("PRIMARY");

                entity.ToTable("spell_area");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.QuestStart)
                    .HasColumnName("quest_start")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AuraSpell)
                    .HasColumnName("aura_spell")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Racemask)
                    .HasColumnName("racemask")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.Autocast)
                    .HasColumnName("autocast")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.QuestEnd)
                    .HasColumnName("quest_end")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.QuestEndStatus)
                    .HasColumnName("quest_end_status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'11'");

                entity.Property(e => e.QuestStartStatus)
                    .HasColumnName("quest_start_status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'64'");
            });

            modelBuilder.Entity<SpellBonusData>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("spell_bonus_data");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ApBonus).HasColumnName("ap_bonus");

                entity.Property(e => e.ApDotBonus).HasColumnName("ap_dot_bonus");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DirectBonus).HasColumnName("direct_bonus");

                entity.Property(e => e.DotBonus).HasColumnName("dot_bonus");
            });

            modelBuilder.Entity<SpellCustomAttr>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("spell_custom_attr");

                entity.HasComment("SpellInfo custom attributes");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("spell id");

                entity.Property(e => e.Attributes)
                    .HasColumnName("attributes")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("SpellCustomAttributes");
            });

            modelBuilder.Entity<SpellDbc>(entity =>
            {
                entity.ToTable("spell_dbc");

                entity.HasComment("Custom spell.dbc entries");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AreaGroupId).HasColumnType("int(11)");

                entity.Property(e => e.Attributes).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx4).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx5).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx6).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesEx7).HasColumnType("int(10) unsigned");

                entity.Property(e => e.AuraInterruptFlags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.BaseLevel).HasColumnType("int(10) unsigned");

                entity.Property(e => e.CastingTimeIndex)
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Dispel).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DmgClass).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DurationIndex).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Effect1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Effect2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Effect3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectAmplitude1).HasColumnType("int(11)");

                entity.Property(e => e.EffectAmplitude2).HasColumnType("int(11)");

                entity.Property(e => e.EffectAmplitude3).HasColumnType("int(11)");

                entity.Property(e => e.EffectApplyAuraName1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectApplyAuraName2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectApplyAuraName3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectBasePoints1).HasColumnType("int(11)");

                entity.Property(e => e.EffectBasePoints2).HasColumnType("int(11)");

                entity.Property(e => e.EffectBasePoints3).HasColumnType("int(11)");

                entity.Property(e => e.EffectDieSides1).HasColumnType("int(11)");

                entity.Property(e => e.EffectDieSides2).HasColumnType("int(11)");

                entity.Property(e => e.EffectDieSides3).HasColumnType("int(11)");

                entity.Property(e => e.EffectImplicitTargetA1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectImplicitTargetA2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectImplicitTargetA3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectImplicitTargetB1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectImplicitTargetB2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectImplicitTargetB3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectItemType1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectItemType2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectItemType3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectMechanic1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectMechanic2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectMechanic3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectMiscValue1).HasColumnType("int(11)");

                entity.Property(e => e.EffectMiscValue2).HasColumnType("int(11)");

                entity.Property(e => e.EffectMiscValue3).HasColumnType("int(11)");

                entity.Property(e => e.EffectMiscValueB1).HasColumnType("int(11)");

                entity.Property(e => e.EffectMiscValueB2).HasColumnType("int(11)");

                entity.Property(e => e.EffectMiscValueB3).HasColumnType("int(11)");

                entity.Property(e => e.EffectRadiusIndex1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectRadiusIndex2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectRadiusIndex3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskA1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskA2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskA3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskB1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskB2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskB3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskC1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskC2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectSpellClassMaskC3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectTriggerSpell1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectTriggerSpell2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffectTriggerSpell3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.EquippedItemClass)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.EquippedItemInventoryTypeMask).HasColumnType("int(11)");

                entity.Property(e => e.EquippedItemSubClassMask).HasColumnType("int(11)");

                entity.Property(e => e.MaxAffectedTargets).HasColumnType("int(10) unsigned");

                entity.Property(e => e.MaxLevel).HasColumnType("int(10) unsigned");

                entity.Property(e => e.MaxTargetLevel).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Mechanic).HasColumnType("int(10) unsigned");

                entity.Property(e => e.PreventionType).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ProcChance).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ProcCharges).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ProcFlags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.RangeIndex)
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.SchoolMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyFlags1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyFlags2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyFlags3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyName).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellLevel).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StackAmount).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Stances).HasColumnType("int(10) unsigned");

                entity.Property(e => e.StancesNot).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Targets).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpellEnchantProcData>(entity =>
            {
                entity.HasKey(e => e.EnchantId)
                    .HasName("PRIMARY");

                entity.ToTable("spell_enchant_proc_data");

                entity.HasComment("Spell enchant proc data");

                entity.Property(e => e.EnchantId)
                    .HasColumnName("EnchantID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttributesMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.HitMask).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpellGroup>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.SpellId })
                    .HasName("PRIMARY");

                entity.ToTable("spell_group");

                entity.HasComment("Spell System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spell_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpellGroupStackRules>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PRIMARY");

                entity.ToTable("spell_group_stack_rules");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.StackRule)
                    .HasColumnName("stack_rule")
                    .HasColumnType("tinyint(3)");
            });

            modelBuilder.Entity<SpellLearnSpell>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.SpellId })
                    .HasName("PRIMARY");

                entity.ToTable("spell_learn_spell");

                entity.HasComment("Item System");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SpellId)
                    .HasColumnName("SpellID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Active)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<SpellLinkedSpell>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("spell_linked_spell");

                entity.HasComment("Spell System");

                entity.HasIndex(e => new { e.SpellTrigger, e.SpellEffect, e.Type })
                    .HasName("trigger_effect_type")
                    .IsUnique();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpellEffect)
                    .HasColumnName("spell_effect")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SpellTrigger)
                    .HasColumnName("spell_trigger")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<SpellLootTemplate>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Item })
                    .HasName("PRIMARY");

                entity.ToTable("spell_loot_template");

                entity.HasComment("Loot System");

                entity.Property(e => e.Entry)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Item)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chance).HasDefaultValueSql("'100'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootMode)
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaxCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MinCount)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Reference)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<SpellPetAuras>(entity =>
            {
                entity.HasKey(e => new { e.Spell, e.EffectId, e.Pet })
                    .HasName("PRIMARY");

                entity.ToTable("spell_pet_auras");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasComment("dummy spell id");

                entity.Property(e => e.EffectId)
                    .HasColumnName("effectId")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Pet)
                    .HasColumnName("pet")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("pet id; 0 = all");

                entity.Property(e => e.Aura)
                    .HasColumnName("aura")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasComment("pet aura id");
            });

            modelBuilder.Entity<SpellProc>(entity =>
            {
                entity.HasKey(e => e.SpellId)
                    .HasName("PRIMARY");

                entity.ToTable("spell_proc");

                entity.Property(e => e.SpellId).HasColumnType("int(11)");

                entity.Property(e => e.AttributesMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Charges).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Cooldown).HasColumnType("int(10) unsigned");

                entity.Property(e => e.DisableEffectsMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.HitMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ProcFlags).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SchoolMask).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SpellFamilyMask0).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyMask1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyMask2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellFamilyName).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SpellPhaseMask).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellTypeMask).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpellRanks>(entity =>
            {
                entity.HasKey(e => new { e.FirstSpellId, e.Rank })
                    .HasName("PRIMARY");

                entity.ToTable("spell_ranks");

                entity.HasComment("Spell Rank Data");

                entity.HasIndex(e => e.SpellId)
                    .HasName("spell_id")
                    .IsUnique();

                entity.Property(e => e.FirstSpellId)
                    .HasColumnName("first_spell_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spell_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<SpellRequired>(entity =>
            {
                entity.HasKey(e => new { e.SpellId, e.ReqSpell })
                    .HasName("PRIMARY");

                entity.ToTable("spell_required");

                entity.HasComment("Spell Additinal Data");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spell_id")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ReqSpell)
                    .HasColumnName("req_spell")
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<SpellScriptNames>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("spell_script_names");

                entity.HasIndex(e => new { e.SpellId, e.ScriptName })
                    .HasName("spell_id")
                    .IsUnique();

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpellId)
                    .HasColumnName("spell_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SpellScripts>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("spell_scripts");

                entity.Property(e => e.Command)
                    .HasColumnName("command")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dataint)
                    .HasColumnName("dataint")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Datalong)
                    .HasColumnName("datalong")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Datalong2)
                    .HasColumnName("datalong2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EffIndex)
                    .HasColumnName("effIndex")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.O).HasColumnName("o");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<SpellTargetPosition>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.EffectIndex })
                    .HasName("PRIMARY");

                entity.ToTable("spell_target_position");

                entity.HasComment("Spell System");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Identifier");

                entity.Property(e => e.EffectIndex).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("MapID")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<SpellThreat>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("spell_threat");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ApPctMod)
                    .HasColumnName("apPctMod")
                    .HasComment("additional threat bonus from attack power");

                entity.Property(e => e.FlatMod)
                    .HasColumnName("flatMod")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PctMod)
                    .HasColumnName("pctMod")
                    .HasDefaultValueSql("'1'")
                    .HasComment("threat multiplier for damage/healing");
            });

            modelBuilder.Entity<SpelldifficultyDbc>(entity =>
            {
                entity.ToTable("spelldifficulty_dbc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Spellid0)
                    .HasColumnName("spellid0")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Spellid1)
                    .HasColumnName("spellid1")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Spellid2)
                    .HasColumnName("spellid2")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Spellid3)
                    .HasColumnName("spellid3")
                    .HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("trainer");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Greeting)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Requirement)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(2) unsigned")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<TrainerLocale>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Locale })
                    .HasName("PRIMARY");

                entity.ToTable("trainer_locale");

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("varchar(4)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GreetingLang)
                    .HasColumnName("Greeting_lang")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<TrainerSpell>(entity =>
            {
                entity.HasKey(e => new { e.TrainerId, e.SpellId })
                    .HasName("PRIMARY");

                entity.ToTable("trainer_spell");

                entity.Property(e => e.TrainerId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellId).HasColumnType("int(10) unsigned");

                entity.Property(e => e.MoneyCost).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReqAbility1).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReqAbility2).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReqAbility3).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReqLevel).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ReqSkillLine).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReqSkillRank).HasColumnType("int(10) unsigned");

                entity.Property(e => e.VerifiedBuild)
                    .HasColumnType("smallint(5)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Transports>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("transports");

                entity.HasComment("Transports");

                entity.HasIndex(e => e.Entry)
                    .HasName("idx_entry")
                    .IsUnique();

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TrinityString>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("trinity_string");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ContentDefault)
                    .IsRequired()
                    .HasColumnName("content_default")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc1)
                    .HasColumnName("content_loc1")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc2)
                    .HasColumnName("content_loc2")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc3)
                    .HasColumnName("content_loc3")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc4)
                    .HasColumnName("content_loc4")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc5)
                    .HasColumnName("content_loc5")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc6)
                    .HasColumnName("content_loc6")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc7)
                    .HasColumnName("content_loc7")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContentLoc8)
                    .HasColumnName("content_loc8")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Updates>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("updates");

                entity.HasComment("List of all applied updates in this database.");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(200)")
                    .HasComment("filename with extension of the update.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Hash)
                    .HasColumnName("hash")
                    .HasColumnType("char(40)")
                    .HasDefaultValueSql("''")
                    .HasComment("sha1 hash of the sql file.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Speed)
                    .HasColumnName("speed")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("time the query takes to apply in ms.");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("enum('RELEASED','ARCHIVED')")
                    .HasDefaultValueSql("'RELEASED'")
                    .HasComment("defines if an update is released or archived.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("timestamp when the query was applied.");
            });

            modelBuilder.Entity<UpdatesInclude>(entity =>
            {
                entity.HasKey(e => e.Path)
                    .HasName("PRIMARY");

                entity.ToTable("updates_include");

                entity.HasComment("List of directories where we want to include sql updates.");

                entity.Property(e => e.Path)
                    .HasColumnName("path")
                    .HasColumnType("varchar(200)")
                    .HasComment("directory to include. $ means relative to the source directory.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("enum('RELEASED','ARCHIVED')")
                    .HasDefaultValueSql("'RELEASED'")
                    .HasComment("defines if the directory contains released or archived updates.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<VehicleAccessory>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.SeatId })
                    .HasName("PRIMARY");

                entity.ToTable("vehicle_accessory");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SeatId)
                    .HasColumnName("seat_id")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.AccessoryEntry)
                    .HasColumnName("accessory_entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Minion)
                    .HasColumnName("minion")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Summontimer)
                    .HasColumnName("summontimer")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'30000'")
                    .HasComment("timer, only relevant for certain summontypes");

                entity.Property(e => e.Summontype)
                    .HasColumnName("summontype")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'6'")
                    .HasComment("see enum TempSummonType");
            });

            modelBuilder.Entity<VehicleSeatAddon>(entity =>
            {
                entity.HasKey(e => e.SeatEntry)
                    .HasName("PRIMARY");

                entity.ToTable("vehicle_seat_addon");

                entity.Property(e => e.SeatEntry)
                    .HasColumnType("int(4) unsigned")
                    .HasComment("VehicleSeatEntry.dbc identifier");

                entity.Property(e => e.ExitParamO).HasDefaultValueSql("'0'");

                entity.Property(e => e.ExitParamValue).HasDefaultValueSql("'0'");

                entity.Property(e => e.ExitParamX).HasDefaultValueSql("'0'");

                entity.Property(e => e.ExitParamY).HasDefaultValueSql("'0'");

                entity.Property(e => e.ExitParamZ).HasDefaultValueSql("'0'");

                entity.Property(e => e.SeatOrientation)
                    .HasDefaultValueSql("'0'")
                    .HasComment("Seat Orientation override value");
            });

            modelBuilder.Entity<VehicleTemplateAccessory>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.SeatId })
                    .HasName("PRIMARY");

                entity.ToTable("vehicle_template_accessory");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SeatId)
                    .HasColumnName("seat_id")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.AccessoryEntry)
                    .HasColumnName("accessory_entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Minion)
                    .HasColumnName("minion")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Summontimer)
                    .HasColumnName("summontimer")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'30000'")
                    .HasComment("timer, only relevant for certain summontypes");

                entity.Property(e => e.Summontype)
                    .HasColumnName("summontype")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'6'")
                    .HasComment("see enum TempSummonType");
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.HasKey(e => e.CoreVersion)
                    .HasName("PRIMARY");

                entity.ToTable("version");

                entity.HasComment("Version Notes");

                entity.Property(e => e.CoreVersion)
                    .HasColumnName("core_version")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasComment("Core revision dumped at startup.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CacheId)
                    .HasColumnName("cache_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CoreRevision)
                    .HasColumnName("core_revision")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DbVersion)
                    .HasColumnName("db_version")
                    .HasColumnType("varchar(120)")
                    .HasComment("Version of world DB.")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<VwConditionsWithLabels>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_conditions_with_labels");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ConditionTarget).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ConditionTypeOrReference)
                    .IsRequired()
                    .HasColumnType("varchar(34)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ConditionValue1)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionValue2)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ConditionValue3)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ElseGroup)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ErrorTextId)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ErrorType)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NegativeCondition).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasColumnType("char(64)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SourceEntry)
                    .HasColumnType("mediumint(8)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceGroup)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceId).HasColumnType("int(11)");

                entity.Property(e => e.SourceTypeOrReferenceId)
                    .IsRequired()
                    .HasColumnType("varchar(49)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<VwDisablesWithLabels>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_disables_with_labels");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("smallint(5)");

                entity.Property(e => e.Params0)
                    .IsRequired()
                    .HasColumnName("params_0")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Params1)
                    .IsRequired()
                    .HasColumnName("params_1")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SourceType)
                    .IsRequired()
                    .HasColumnName("sourceType")
                    .HasColumnType("varchar(33)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<VwSmartScriptsWithLabels>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_smart_scripts_with_labels");

                entity.Property(e => e.ActionParam1)
                    .HasColumnName("action_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam2)
                    .HasColumnName("action_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam3)
                    .HasColumnName("action_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam4)
                    .HasColumnName("action_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam5)
                    .HasColumnName("action_param5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionParam6)
                    .HasColumnName("action_param6")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnName("action_type")
                    .HasColumnType("varchar(47)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasComment("Event Comment")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Entryorguid)
                    .HasColumnName("entryorguid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EventChance)
                    .HasColumnName("event_chance")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.EventFlags)
                    .HasColumnName("event_flags")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EventParam1)
                    .HasColumnName("event_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam2)
                    .HasColumnName("event_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam3)
                    .HasColumnName("event_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam4)
                    .HasColumnName("event_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventParam5)
                    .HasColumnName("event_param5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EventPhaseMask)
                    .HasColumnName("event_phase_mask")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasColumnName("event_type")
                    .HasColumnType("varchar(35)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SourceType)
                    .HasColumnName("source_type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TargetO).HasColumnName("target_o");

                entity.Property(e => e.TargetParam1)
                    .HasColumnName("target_param1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam2)
                    .HasColumnName("target_param2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam3)
                    .HasColumnName("target_param3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetParam4)
                    .HasColumnName("target_param4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TargetType)
                    .IsRequired()
                    .HasColumnName("target_type")
                    .HasColumnType("varchar(41)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TargetX).HasColumnName("target_x");

                entity.Property(e => e.TargetY).HasColumnName("target_y");

                entity.Property(e => e.TargetZ).HasColumnName("target_z");
            });

            modelBuilder.Entity<WardenChecks>(entity =>
            {
                entity.ToTable("warden_checks");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasMaxLength(24)
                    .IsFixedLength();

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Result)
                    .HasColumnName("result")
                    .HasMaxLength(24);

                entity.Property(e => e.Str)
                    .HasColumnName("str")
                    .HasColumnType("varchar(170)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<WaypointData>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Point })
                    .HasName("PRIMARY");

                entity.ToTable("waypoint_data");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Creature GUID");

                entity.Property(e => e.Point)
                    .HasColumnName("point")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionChance)
                    .HasColumnName("action_chance")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MoveType)
                    .HasColumnName("move_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.Wpguid)
                    .HasColumnName("wpguid")
                    .HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<WaypointScripts>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("waypoint_scripts");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Command)
                    .HasColumnName("command")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dataint)
                    .HasColumnName("dataint")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Datalong)
                    .HasColumnName("datalong")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Datalong2)
                    .HasColumnName("datalong2")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.O).HasColumnName("o");

                entity.Property(e => e.X).HasColumnName("x");

                entity.Property(e => e.Y).HasColumnName("y");

                entity.Property(e => e.Z).HasColumnName("z");
            });

            modelBuilder.Entity<Waypoints>(entity =>
            {
                entity.HasKey(e => new { e.Entry, e.Pointid })
                    .HasName("PRIMARY");

                entity.ToTable("waypoints");

                entity.HasComment("Creature waypoints");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pointid)
                    .HasColumnName("pointid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Delay)
                    .HasColumnName("delay")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PointComment)
                    .HasColumnName("point_comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
