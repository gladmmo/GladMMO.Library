using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GladMMO
{
    public partial class wotlk_charactersContext : DbContext
    {
        public wotlk_charactersContext()
        {
        }

        public wotlk_charactersContext(DbContextOptions<wotlk_charactersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountData> AccountData { get; set; }
        public virtual DbSet<AccountInstanceTimes> AccountInstanceTimes { get; set; }
        public virtual DbSet<AccountTutorial> AccountTutorial { get; set; }
        public virtual DbSet<Addons> Addons { get; set; }
        public virtual DbSet<ArenaTeam> ArenaTeam { get; set; }
        public virtual DbSet<ArenaTeamMember> ArenaTeamMember { get; set; }
        public virtual DbSet<Auctionbidders> Auctionbidders { get; set; }
        public virtual DbSet<Auctionhouse> Auctionhouse { get; set; }
        public virtual DbSet<BannedAddons> BannedAddons { get; set; }
        public virtual DbSet<BattlegroundDeserters> BattlegroundDeserters { get; set; }
        public virtual DbSet<Bugreport> Bugreport { get; set; }
        public virtual DbSet<CalendarEvents> CalendarEvents { get; set; }
        public virtual DbSet<CalendarInvites> CalendarInvites { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<CharacterAccountData> CharacterAccountData { get; set; }
        public virtual DbSet<CharacterAchievement> CharacterAchievement { get; set; }
        public virtual DbSet<CharacterAchievementProgress> CharacterAchievementProgress { get; set; }
        public virtual DbSet<CharacterAction> CharacterAction { get; set; }
        public virtual DbSet<CharacterArenaStats> CharacterArenaStats { get; set; }
        public virtual DbSet<CharacterAura> CharacterAura { get; set; }
        public virtual DbSet<CharacterBanned> CharacterBanned { get; set; }
        public virtual DbSet<CharacterBattlegroundData> CharacterBattlegroundData { get; set; }
        public virtual DbSet<CharacterBattlegroundRandom> CharacterBattlegroundRandom { get; set; }
        public virtual DbSet<CharacterDeclinedname> CharacterDeclinedname { get; set; }
        public virtual DbSet<CharacterEquipmentsets> CharacterEquipmentsets { get; set; }
        public virtual DbSet<CharacterFishingsteps> CharacterFishingsteps { get; set; }
        public virtual DbSet<CharacterGifts> CharacterGifts { get; set; }
        public virtual DbSet<CharacterGlyphs> CharacterGlyphs { get; set; }
        public virtual DbSet<CharacterHomebind> CharacterHomebind { get; set; }
        public virtual DbSet<CharacterInstance> CharacterInstance { get; set; }
        public virtual DbSet<CharacterInventory> CharacterInventory { get; set; }
        public virtual DbSet<CharacterPet> CharacterPet { get; set; }
        public virtual DbSet<CharacterPetDeclinedname> CharacterPetDeclinedname { get; set; }
        public virtual DbSet<CharacterQueststatus> CharacterQueststatus { get; set; }
        public virtual DbSet<CharacterQueststatusDaily> CharacterQueststatusDaily { get; set; }
        public virtual DbSet<CharacterQueststatusMonthly> CharacterQueststatusMonthly { get; set; }
        public virtual DbSet<CharacterQueststatusRewarded> CharacterQueststatusRewarded { get; set; }
        public virtual DbSet<CharacterQueststatusSeasonal> CharacterQueststatusSeasonal { get; set; }
        public virtual DbSet<CharacterQueststatusWeekly> CharacterQueststatusWeekly { get; set; }
        public virtual DbSet<CharacterReputation> CharacterReputation { get; set; }
        public virtual DbSet<CharacterSkills> CharacterSkills { get; set; }
        public virtual DbSet<CharacterSocial> CharacterSocial { get; set; }
        public virtual DbSet<CharacterSpell> CharacterSpell { get; set; }
        public virtual DbSet<CharacterSpellCooldown> CharacterSpellCooldown { get; set; }
        public virtual DbSet<CharacterStats> CharacterStats { get; set; }
        public virtual DbSet<CharacterTalent> CharacterTalent { get; set; }
        public virtual DbSet<Characters> Characters { get; set; }
        public virtual DbSet<Corpse> Corpse { get; set; }
        public virtual DbSet<GameEventConditionSave> GameEventConditionSave { get; set; }
        public virtual DbSet<GameEventSave> GameEventSave { get; set; }
        public virtual DbSet<GmSubsurvey> GmSubsurvey { get; set; }
        public virtual DbSet<GmSurvey> GmSurvey { get; set; }
        public virtual DbSet<GmTicket> GmTicket { get; set; }
        public virtual DbSet<GroupInstance> GroupInstance { get; set; }
        public virtual DbSet<GroupMember> GroupMember { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Guild> Guild { get; set; }
        public virtual DbSet<GuildBankEventlog> GuildBankEventlog { get; set; }
        public virtual DbSet<GuildBankItem> GuildBankItem { get; set; }
        public virtual DbSet<GuildBankRight> GuildBankRight { get; set; }
        public virtual DbSet<GuildBankTab> GuildBankTab { get; set; }
        public virtual DbSet<GuildEventlog> GuildEventlog { get; set; }
        public virtual DbSet<GuildMember> GuildMember { get; set; }
        public virtual DbSet<GuildMemberWithdraw> GuildMemberWithdraw { get; set; }
        public virtual DbSet<GuildRank> GuildRank { get; set; }
        public virtual DbSet<Instance> Instance { get; set; }
        public virtual DbSet<InstanceReset> InstanceReset { get; set; }
        public virtual DbSet<ItemInstance> ItemInstance { get; set; }
        public virtual DbSet<ItemLootItems> ItemLootItems { get; set; }
        public virtual DbSet<ItemLootMoney> ItemLootMoney { get; set; }
        public virtual DbSet<ItemRefundInstance> ItemRefundInstance { get; set; }
        public virtual DbSet<ItemSoulboundTradeData> ItemSoulboundTradeData { get; set; }
        public virtual DbSet<LagReports> LagReports { get; set; }
        public virtual DbSet<LfgData> LfgData { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<MailItems> MailItems { get; set; }
        public virtual DbSet<PetAura> PetAura { get; set; }
        public virtual DbSet<PetSpell> PetSpell { get; set; }
        public virtual DbSet<PetSpellCooldown> PetSpellCooldown { get; set; }
        public virtual DbSet<Petition> Petition { get; set; }
        public virtual DbSet<PetitionSign> PetitionSign { get; set; }
        public virtual DbSet<PoolQuestSave> PoolQuestSave { get; set; }
        public virtual DbSet<PvpstatsBattlegrounds> PvpstatsBattlegrounds { get; set; }
        public virtual DbSet<PvpstatsPlayers> PvpstatsPlayers { get; set; }
        public virtual DbSet<QuestTracker> QuestTracker { get; set; }
        public virtual DbSet<ReservedName> ReservedName { get; set; }
        public virtual DbSet<Respawn> Respawn { get; set; }
        public virtual DbSet<Updates> Updates { get; set; }
        public virtual DbSet<UpdatesInclude> UpdatesInclude { get; set; }
        public virtual DbSet<WardenAction> WardenAction { get; set; }
        public virtual DbSet<Worldstates> Worldstates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountData>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.Type })
                    .HasName("PRIMARY");

                entity.ToTable("account_data");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account Identifier");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasColumnType("blob");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<AccountInstanceTimes>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.InstanceId })
                    .HasName("PRIMARY");

                entity.ToTable("account_instance_times");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instanceId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ReleaseTime)
                    .HasColumnName("releaseTime")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<AccountTutorial>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PRIMARY");

                entity.ToTable("account_tutorial");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account Identifier");

                entity.Property(e => e.Tut0)
                    .HasColumnName("tut0")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut1)
                    .HasColumnName("tut1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut2)
                    .HasColumnName("tut2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut3)
                    .HasColumnName("tut3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut4)
                    .HasColumnName("tut4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut5)
                    .HasColumnName("tut5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut6)
                    .HasColumnName("tut6")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tut7)
                    .HasColumnName("tut7")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Addons>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("addons");

                entity.HasComment("Addons");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(120)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Crc)
                    .HasColumnName("crc")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<ArenaTeam>(entity =>
            {
                entity.ToTable("arena_team");

                entity.Property(e => e.ArenaTeamId)
                    .HasColumnName("arenaTeamId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BackgroundColor)
                    .HasColumnName("backgroundColor")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BorderColor)
                    .HasColumnName("borderColor")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BorderStyle)
                    .HasColumnName("borderStyle")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.CaptainGuid)
                    .HasColumnName("captainGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmblemColor)
                    .HasColumnName("emblemColor")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmblemStyle)
                    .HasColumnName("emblemStyle")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(24)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SeasonGames)
                    .HasColumnName("seasonGames")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SeasonWins)
                    .HasColumnName("seasonWins")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.WeekGames)
                    .HasColumnName("weekGames")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.WeekWins)
                    .HasColumnName("weekWins")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<ArenaTeamMember>(entity =>
            {
                entity.HasKey(e => new { e.ArenaTeamId, e.Guid })
                    .HasName("PRIMARY");

                entity.ToTable("arena_team_member");

                entity.Property(e => e.ArenaTeamId)
                    .HasColumnName("arenaTeamId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PersonalRating)
                    .HasColumnName("personalRating")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SeasonGames)
                    .HasColumnName("seasonGames")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.SeasonWins)
                    .HasColumnName("seasonWins")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.WeekGames)
                    .HasColumnName("weekGames")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.WeekWins)
                    .HasColumnName("weekWins")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<Auctionbidders>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Bidderguid })
                    .HasName("PRIMARY");

                entity.ToTable("auctionbidders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bidderguid)
                    .HasColumnName("bidderguid")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Auctionhouse>(entity =>
            {
                entity.ToTable("auctionhouse");

                entity.HasIndex(e => e.Itemguid)
                    .HasName("item_guid")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Buyguid)
                    .HasColumnName("buyguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Buyoutprice)
                    .HasColumnName("buyoutprice")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Deposit)
                    .HasColumnName("deposit")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Houseid)
                    .HasColumnName("houseid")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'7'");

                entity.Property(e => e.Itemguid)
                    .HasColumnName("itemguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Itemowner)
                    .HasColumnName("itemowner")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Lastbid)
                    .HasColumnName("lastbid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Startbid)
                    .HasColumnName("startbid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<BannedAddons>(entity =>
            {
                entity.ToTable("banned_addons");

                entity.HasIndex(e => new { e.Name, e.Version })
                    .HasName("idx_name_ver")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<BattlegroundDeserters>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("battleground_deserters");

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("datetime")
                    .HasComment("datetime of the desertion");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("characters.guid");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("type of the desertion");
            });

            modelBuilder.Entity<Bugreport>(entity =>
            {
                entity.ToTable("bugreport");

                entity.HasComment("Debug System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Identifier");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CalendarEvents>(entity =>
            {
                entity.ToTable("calendar_events");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dungeon)
                    .HasColumnName("dungeon")
                    .HasColumnType("int(10)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Eventtime)
                    .HasColumnName("eventtime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Time2)
                    .HasColumnName("time2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(1) unsigned")
                    .HasDefaultValueSql("'4'");
            });

            modelBuilder.Entity<CalendarInvites>(entity =>
            {
                entity.ToTable("calendar_invites");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Invitee)
                    .HasColumnName("invitee")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("tinyint(1) unsigned");

                entity.Property(e => e.Sender)
                    .HasColumnName("sender")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(1) unsigned");

                entity.Property(e => e.Statustime)
                    .HasColumnName("statustime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.Team })
                    .HasName("PRIMARY");

                entity.ToTable("channels");

                entity.HasComment("Channel System");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Team)
                    .HasColumnName("team")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Announce)
                    .HasColumnName("announce")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.BannedList)
                    .HasColumnName("bannedList")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastUsed)
                    .HasColumnName("lastUsed")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Ownership)
                    .HasColumnName("ownership")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CharacterAccountData>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Type })
                    .HasName("PRIMARY");

                entity.ToTable("character_account_data");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasColumnType("blob");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterAchievement>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Achievement })
                    .HasName("PRIMARY");

                entity.ToTable("character_achievement");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Achievement)
                    .HasColumnName("achievement")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterAchievementProgress>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Criteria })
                    .HasName("PRIMARY");

                entity.ToTable("character_achievement_progress");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Criteria)
                    .HasColumnName("criteria")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Counter)
                    .HasColumnName("counter")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterAction>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spec, e.Button })
                    .HasName("PRIMARY");

                entity.ToTable("character_action");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spec)
                    .HasColumnName("spec")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Button)
                    .HasColumnName("button")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterArenaStats>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Slot })
                    .HasName("PRIMARY");

                entity.ToTable("character_arena_stats");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Slot)
                    .HasColumnName("slot")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.MatchMakerRating)
                    .HasColumnName("matchMakerRating")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<CharacterAura>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.CasterGuid, e.ItemGuid, e.Spell, e.EffectMask })
                    .HasName("PRIMARY");

                entity.ToTable("character_aura");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.CasterGuid)
                    .HasColumnName("casterGuid")
                    .HasColumnType("bigint(20) unsigned")
                    .HasComment("Full Global Unique Identifier");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("itemGuid")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EffectMask)
                    .HasColumnName("effectMask")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Amount0)
                    .HasColumnName("amount0")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount1)
                    .HasColumnName("amount1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount2)
                    .HasColumnName("amount2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApplyResilience)
                    .HasColumnName("applyResilience")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.BaseAmount0)
                    .HasColumnName("base_amount0")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BaseAmount1)
                    .HasColumnName("base_amount1")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BaseAmount2)
                    .HasColumnName("base_amount2")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CritChance).HasColumnName("critChance");

                entity.Property(e => e.MaxDuration)
                    .HasColumnName("maxDuration")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RecalculateMask)
                    .HasColumnName("recalculateMask")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RemainCharges)
                    .HasColumnName("remainCharges")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RemainTime)
                    .HasColumnName("remainTime")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StackCount)
                    .HasColumnName("stackCount")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<CharacterBanned>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Bandate })
                    .HasName("PRIMARY");

                entity.ToTable("character_banned");

                entity.HasComment("Ban List");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Bandate)
                    .HasColumnName("bandate")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Bannedby)
                    .IsRequired()
                    .HasColumnName("bannedby")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Banreason)
                    .IsRequired()
                    .HasColumnName("banreason")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Unbandate)
                    .HasColumnName("unbandate")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterBattlegroundData>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_battleground_data");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instanceId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Instance Identifier");

                entity.Property(e => e.JoinMapId)
                    .HasColumnName("joinMapId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.JoinO).HasColumnName("joinO");

                entity.Property(e => e.JoinX).HasColumnName("joinX");

                entity.Property(e => e.JoinY).HasColumnName("joinY");

                entity.Property(e => e.JoinZ).HasColumnName("joinZ");

                entity.Property(e => e.MountSpell)
                    .HasColumnName("mountSpell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TaxiEnd)
                    .HasColumnName("taxiEnd")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TaxiStart)
                    .HasColumnName("taxiStart")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Team)
                    .HasColumnName("team")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<CharacterBattlegroundRandom>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_battleground_random");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterDeclinedname>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_declinedname");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Accusative)
                    .IsRequired()
                    .HasColumnName("accusative")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dative)
                    .IsRequired()
                    .HasColumnName("dative")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Genitive)
                    .IsRequired()
                    .HasColumnName("genitive")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Instrumental)
                    .IsRequired()
                    .HasColumnName("instrumental")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Prepositional)
                    .IsRequired()
                    .HasColumnName("prepositional")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CharacterEquipmentsets>(entity =>
            {
                entity.HasKey(e => e.Setguid)
                    .HasName("PRIMARY");

                entity.ToTable("character_equipmentsets");

                entity.HasIndex(e => e.Setindex)
                    .HasName("Idx_setindex");

                entity.HasIndex(e => new { e.Guid, e.Setguid, e.Setindex })
                    .HasName("idx_set")
                    .IsUnique();

                entity.Property(e => e.Setguid)
                    .HasColumnName("setguid")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Iconname)
                    .IsRequired()
                    .HasColumnName("iconname")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IgnoreMask)
                    .HasColumnName("ignore_mask")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item0)
                    .HasColumnName("item0")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item1)
                    .HasColumnName("item1")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item10)
                    .HasColumnName("item10")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item11)
                    .HasColumnName("item11")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item12)
                    .HasColumnName("item12")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item13)
                    .HasColumnName("item13")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item14)
                    .HasColumnName("item14")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item15)
                    .HasColumnName("item15")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item16)
                    .HasColumnName("item16")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item17)
                    .HasColumnName("item17")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item18)
                    .HasColumnName("item18")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item2)
                    .HasColumnName("item2")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item3)
                    .HasColumnName("item3")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item4)
                    .HasColumnName("item4")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item5)
                    .HasColumnName("item5")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item6)
                    .HasColumnName("item6")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item7)
                    .HasColumnName("item7")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item8)
                    .HasColumnName("item8")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Item9)
                    .HasColumnName("item9")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(31)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Setindex)
                    .HasColumnName("setindex")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterFishingsteps>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_fishingsteps");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.FishingSteps)
                    .HasColumnName("fishingSteps")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterGifts>(entity =>
            {
                entity.HasKey(e => e.ItemGuid)
                    .HasName("PRIMARY");

                entity.ToTable("character_gifts");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("item_guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterGlyphs>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.TalentGroup })
                    .HasName("PRIMARY");

                entity.ToTable("character_glyphs");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TalentGroup)
                    .HasColumnName("talentGroup")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Glyph1)
                    .HasColumnName("glyph1")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Glyph2)
                    .HasColumnName("glyph2")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Glyph3)
                    .HasColumnName("glyph3")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Glyph4)
                    .HasColumnName("glyph4")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Glyph5)
                    .HasColumnName("glyph5")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Glyph6)
                    .HasColumnName("glyph6")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<CharacterHomebind>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_homebind");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.PosX).HasColumnName("posX");

                entity.Property(e => e.PosY).HasColumnName("posY");

                entity.Property(e => e.PosZ).HasColumnName("posZ");

                entity.Property(e => e.ZoneId)
                    .HasColumnName("zoneId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Zone Identifier");
            });

            modelBuilder.Entity<CharacterInstance>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Instance })
                    .HasName("PRIMARY");

                entity.ToTable("character_instance");

                entity.HasIndex(e => e.Instance)
                    .HasName("instance");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Instance)
                    .HasColumnName("instance")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ExtendState)
                    .HasColumnName("extendState")
                    .HasColumnType("tinyint(2) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Permanent)
                    .HasColumnName("permanent")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterInventory>(entity =>
            {
                entity.HasKey(e => e.Item)
                    .HasName("PRIMARY");

                entity.ToTable("character_inventory");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.HasIndex(e => new { e.Guid, e.Bag, e.Slot })
                    .HasName("guid")
                    .IsUnique();

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Item Global Unique Identifier");

                entity.Property(e => e.Bag)
                    .HasColumnName("bag")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Slot)
                    .HasColumnName("slot")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterPet>(entity =>
            {
                entity.ToTable("character_pet");

                entity.HasComment("Pet System");

                entity.HasIndex(e => e.Owner)
                    .HasName("owner");

                entity.HasIndex(e => e.Slot)
                    .HasName("idx_slot");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Abdata)
                    .HasColumnName("abdata")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatedBySpell)
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Curhappiness)
                    .HasColumnName("curhappiness")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Curhealth)
                    .HasColumnName("curhealth")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Curmana)
                    .HasColumnName("curmana")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Exp)
                    .HasColumnName("exp")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Modelid)
                    .HasColumnName("modelid")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(21)")
                    .HasDefaultValueSql("'Pet'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PetType).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Reactstate).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Renamed)
                    .HasColumnName("renamed")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Savetime)
                    .HasColumnName("savetime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Slot)
                    .HasColumnName("slot")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterPetDeclinedname>(entity =>
            {
                entity.ToTable("character_pet_declinedname");

                entity.HasIndex(e => e.Owner)
                    .HasName("owner_key");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Accusative)
                    .IsRequired()
                    .HasColumnName("accusative")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dative)
                    .IsRequired()
                    .HasColumnName("dative")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Genitive)
                    .IsRequired()
                    .HasColumnName("genitive")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Instrumental)
                    .IsRequired()
                    .HasColumnName("instrumental")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Prepositional)
                    .IsRequired()
                    .HasColumnName("prepositional")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CharacterQueststatus>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");

                entity.Property(e => e.Explored)
                    .HasColumnName("explored")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Itemcount1)
                    .HasColumnName("itemcount1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Itemcount2)
                    .HasColumnName("itemcount2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Itemcount3)
                    .HasColumnName("itemcount3")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Itemcount4)
                    .HasColumnName("itemcount4")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Itemcount5)
                    .HasColumnName("itemcount5")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Itemcount6)
                    .HasColumnName("itemcount6")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Mobcount1)
                    .HasColumnName("mobcount1")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Mobcount2)
                    .HasColumnName("mobcount2")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Mobcount3)
                    .HasColumnName("mobcount3")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Mobcount4)
                    .HasColumnName("mobcount4")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Playercount)
                    .HasColumnName("playercount")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Timer)
                    .HasColumnName("timer")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterQueststatusDaily>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus_daily");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterQueststatusMonthly>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus_monthly");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<CharacterQueststatusRewarded>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus_rewarded");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(10) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<CharacterQueststatusSeasonal>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus_seasonal");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");

                entity.Property(e => e.Event)
                    .HasColumnName("event")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Event Identifier");
            });

            modelBuilder.Entity<CharacterQueststatusWeekly>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Quest })
                    .HasName("PRIMARY");

                entity.ToTable("character_queststatus_weekly");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Guid)
                    .HasName("idx_guid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Quest)
                    .HasColumnName("quest")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Quest Identifier");
            });

            modelBuilder.Entity<CharacterReputation>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Faction })
                    .HasName("PRIMARY");

                entity.ToTable("character_reputation");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Faction)
                    .HasColumnName("faction")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Standing)
                    .HasColumnName("standing")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<CharacterSkills>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Skill })
                    .HasName("PRIMARY");

                entity.ToTable("character_skills");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Skill)
                    .HasColumnName("skill")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Max)
                    .HasColumnName("max")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<CharacterSocial>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Friend, e.Flags })
                    .HasName("PRIMARY");

                entity.ToTable("character_social");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Friend)
                    .HasName("friend");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Global Unique Identifier");

                entity.Property(e => e.Friend)
                    .HasColumnName("friend")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Friend Global Unique Identifier");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Friend Flags");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note")
                    .HasColumnType("varchar(48)")
                    .HasDefaultValueSql("''")
                    .HasComment("Friend Note")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<CharacterSpell>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spell })
                    .HasName("PRIMARY");

                entity.ToTable("character_spell");

                entity.HasComment("Player System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Spell Identifier");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Disabled)
                    .HasColumnName("disabled")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<CharacterSpellCooldown>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spell })
                    .HasName("PRIMARY");

                entity.ToTable("character_spell_cooldown");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier, Low part");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Spell Identifier");

                entity.Property(e => e.CategoryEnd)
                    .HasColumnName("categoryEnd")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Spell category Id");

                entity.Property(e => e.Item)
                    .HasColumnName("item")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Item Identifier");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterStats>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("character_stats");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier, Low part");

                entity.Property(e => e.Agility)
                    .HasColumnName("agility")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Armor)
                    .HasColumnName("armor")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AttackPower)
                    .HasColumnName("attackPower")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BlockPct)
                    .HasColumnName("blockPct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.CritPct)
                    .HasColumnName("critPct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.DodgePct)
                    .HasColumnName("dodgePct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.Intellect)
                    .HasColumnName("intellect")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxhealth)
                    .HasColumnName("maxhealth")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower1)
                    .HasColumnName("maxpower1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower2)
                    .HasColumnName("maxpower2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower3)
                    .HasColumnName("maxpower3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower4)
                    .HasColumnName("maxpower4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower5)
                    .HasColumnName("maxpower5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower6)
                    .HasColumnName("maxpower6")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxpower7)
                    .HasColumnName("maxpower7")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ParryPct)
                    .HasColumnName("parryPct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.RangedAttackPower)
                    .HasColumnName("rangedAttackPower")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RangedCritPct)
                    .HasColumnName("rangedCritPct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.ResArcane)
                    .HasColumnName("resArcane")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResFire)
                    .HasColumnName("resFire")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResFrost)
                    .HasColumnName("resFrost")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResHoly)
                    .HasColumnName("resHoly")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResNature)
                    .HasColumnName("resNature")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResShadow)
                    .HasColumnName("resShadow")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Resilience)
                    .HasColumnName("resilience")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SpellCritPct)
                    .HasColumnName("spellCritPct")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.SpellPower)
                    .HasColumnName("spellPower")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spirit)
                    .HasColumnName("spirit")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Stamina)
                    .HasColumnName("stamina")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Strength)
                    .HasColumnName("strength")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<CharacterTalent>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spell, e.TalentGroup })
                    .HasName("PRIMARY");

                entity.ToTable("character_talent");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.TalentGroup)
                    .HasColumnName("talentGroup")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Characters>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("characters");

                entity.HasComment("Player System");

                entity.HasIndex(e => e.Account)
                    .HasName("idx_account");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_name");

                entity.HasIndex(e => e.Online)
                    .HasName("idx_online");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account Identifier");

                entity.Property(e => e.ActionBars)
                    .HasColumnName("actionBars")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ActiveTalentGroup)
                    .HasColumnName("activeTalentGroup")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.AmmoId)
                    .HasColumnName("ammoId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ArenaPoints)
                    .HasColumnName("arenaPoints")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AtLogin)
                    .HasColumnName("at_login")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.BankSlots)
                    .HasColumnName("bankSlots")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ChosenTitle)
                    .HasColumnName("chosenTitle")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Cinematic)
                    .HasColumnName("cinematic")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.DeathExpireTime)
                    .HasColumnName("death_expire_time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeleteDate)
                    .HasColumnName("deleteDate")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeleteInfosAccount)
                    .HasColumnName("deleteInfos_Account")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeleteInfosName)
                    .HasColumnName("deleteInfos_Name")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Drunk)
                    .HasColumnName("drunk")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.EquipmentCache)
                    .HasColumnName("equipmentCache")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ExploredZones)
                    .HasColumnName("exploredZones")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ExtraFlags)
                    .HasColumnName("extra_flags")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Face)
                    .HasColumnName("face")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.FacialStyle)
                    .HasColumnName("facialStyle")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.GrantableLevels)
                    .HasColumnName("grantableLevels")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.HairColor)
                    .HasColumnName("hairColor")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.HairStyle)
                    .HasColumnName("hairStyle")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Health)
                    .HasColumnName("health")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instance_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InstanceModeMask)
                    .HasColumnName("instance_mode_mask")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.IsLogoutResting)
                    .HasColumnName("is_logout_resting")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.KnownCurrencies)
                    .HasColumnName("knownCurrencies")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.KnownTitles)
                    .HasColumnName("knownTitles")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Latency)
                    .HasColumnName("latency")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Leveltime)
                    .HasColumnName("leveltime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LogoutTime)
                    .HasColumnName("logout_time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(12)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Online)
                    .HasColumnName("online")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PlayerFlags)
                    .HasColumnName("playerFlags")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PositionX).HasColumnName("position_x");

                entity.Property(e => e.PositionY).HasColumnName("position_y");

                entity.Property(e => e.PositionZ).HasColumnName("position_z");

                entity.Property(e => e.Power1)
                    .HasColumnName("power1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power2)
                    .HasColumnName("power2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power3)
                    .HasColumnName("power3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power4)
                    .HasColumnName("power4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power5)
                    .HasColumnName("power5")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power6)
                    .HasColumnName("power6")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Power7)
                    .HasColumnName("power7")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ResettalentsCost)
                    .HasColumnName("resettalents_cost")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ResettalentsTime)
                    .HasColumnName("resettalents_time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RestBonus).HasColumnName("rest_bonus");

                entity.Property(e => e.RestState)
                    .HasColumnName("restState")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Skin)
                    .HasColumnName("skin")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.StableSlots)
                    .HasColumnName("stable_slots")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TalentGroupsCount)
                    .HasColumnName("talentGroupsCount")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.TaxiPath)
                    .HasColumnName("taxi_path")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Taximask)
                    .IsRequired()
                    .HasColumnName("taximask")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TodayHonorPoints)
                    .HasColumnName("todayHonorPoints")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TodayKills)
                    .HasColumnName("todayKills")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.TotalHonorPoints)
                    .HasColumnName("totalHonorPoints")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TotalKills)
                    .HasColumnName("totalKills")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Totaltime)
                    .HasColumnName("totaltime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TransO).HasColumnName("trans_o");

                entity.Property(e => e.TransX).HasColumnName("trans_x");

                entity.Property(e => e.TransY).HasColumnName("trans_y");

                entity.Property(e => e.TransZ).HasColumnName("trans_z");

                entity.Property(e => e.Transguid)
                    .HasColumnName("transguid")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.WatchedFaction)
                    .HasColumnName("watchedFaction")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Xp)
                    .HasColumnName("xp")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.YesterdayHonorPoints)
                    .HasColumnName("yesterdayHonorPoints")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.YesterdayKills)
                    .HasColumnName("yesterdayKills")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Zone)
                    .HasColumnName("zone")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<Corpse>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("corpse");

                entity.HasComment("Death System");

                entity.HasIndex(e => e.CorpseType)
                    .HasName("idx_type");

                entity.HasIndex(e => e.InstanceId)
                    .HasName("idx_instance");

                entity.HasIndex(e => e.Time)
                    .HasName("idx_time");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Global Unique Identifier");

                entity.Property(e => e.Bytes1)
                    .HasColumnName("bytes1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bytes2)
                    .HasColumnName("bytes2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CorpseType)
                    .HasColumnName("corpseType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.DisplayId)
                    .HasColumnName("displayId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DynFlags)
                    .HasColumnName("dynFlags")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.GuildId)
                    .HasColumnName("guildId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instanceId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Instance Identifier");

                entity.Property(e => e.ItemCache)
                    .IsRequired()
                    .HasColumnName("itemCache")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("smallint(5) unsigned")
                    .HasComment("Map Identifier");

                entity.Property(e => e.Orientation).HasColumnName("orientation");

                entity.Property(e => e.PhaseMask)
                    .HasColumnName("phaseMask")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.PosX).HasColumnName("posX");

                entity.Property(e => e.PosY).HasColumnName("posY");

                entity.Property(e => e.PosZ).HasColumnName("posZ");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GameEventConditionSave>(entity =>
            {
                entity.HasKey(e => new { e.EventEntry, e.ConditionId })
                    .HasName("PRIMARY");

                entity.ToTable("game_event_condition_save");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ConditionId)
                    .HasColumnName("condition_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Done)
                    .HasColumnName("done")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<GameEventSave>(entity =>
            {
                entity.HasKey(e => e.EventEntry)
                    .HasName("PRIMARY");

                entity.ToTable("game_event_save");

                entity.Property(e => e.EventEntry)
                    .HasColumnName("eventEntry")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.NextStart)
                    .HasColumnName("next_start")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<GmSubsurvey>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.QuestionId })
                    .HasName("PRIMARY");

                entity.ToTable("gm_subsurvey");

                entity.HasComment("Player System");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("surveyId")
                    .HasColumnType("int(10) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.QuestionId)
                    .HasColumnName("questionId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Answer)
                    .HasColumnName("answer")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AnswerComment)
                    .IsRequired()
                    .HasColumnName("answerComment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<GmSurvey>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("PRIMARY");

                entity.ToTable("gm_survey");

                entity.HasComment("Player System");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("surveyId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MainSurvey)
                    .HasColumnName("mainSurvey")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GmTicket>(entity =>
            {
                entity.ToTable("gm_ticket");

                entity.HasComment("Player System");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AssignedTo)
                    .HasColumnName("assignedTo")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("GUID of admin to whom ticket is assigned");

                entity.Property(e => e.ClosedBy)
                    .HasColumnName("closedBy")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Escalated)
                    .HasColumnName("escalated")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LastModifiedTime)
                    .HasColumnName("lastModifiedTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(12)")
                    .HasComment("Name of ticket creator")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NeedMoreHelp)
                    .HasColumnName("needMoreHelp")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.PlayerGuid)
                    .HasColumnName("playerGuid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier of ticket creator");

                entity.Property(e => e.PosX).HasColumnName("posX");

                entity.Property(e => e.PosY).HasColumnName("posY");

                entity.Property(e => e.PosZ).HasColumnName("posZ");

                entity.Property(e => e.ResolvedBy)
                    .HasColumnName("resolvedBy")
                    .HasColumnType("int(10)")
                    .HasComment("GUID of GM who resolved the ticket");

                entity.Property(e => e.Response)
                    .IsRequired()
                    .HasColumnName("response")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("0 open, 1 closed, 2 character deleted");

                entity.Property(e => e.Viewed)
                    .HasColumnName("viewed")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<GroupInstance>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Instance })
                    .HasName("PRIMARY");

                entity.ToTable("group_instance");

                entity.HasIndex(e => e.Instance)
                    .HasName("instance");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Instance)
                    .HasColumnName("instance")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Permanent)
                    .HasColumnName("permanent")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(e => e.MemberGuid)
                    .HasName("PRIMARY");

                entity.ToTable("group_member");

                entity.HasComment("Groups");

                entity.Property(e => e.MemberGuid)
                    .HasColumnName("memberGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MemberFlags)
                    .HasColumnName("memberFlags")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Roles)
                    .HasColumnName("roles")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Subgroup)
                    .HasColumnName("subgroup")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("groups");

                entity.HasComment("Groups");

                entity.HasIndex(e => e.LeaderGuid)
                    .HasName("leaderGuid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.GroupType)
                    .HasColumnName("groupType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Icon1)
                    .HasColumnName("icon1")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon2)
                    .HasColumnName("icon2")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon3)
                    .HasColumnName("icon3")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon4)
                    .HasColumnName("icon4")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon5)
                    .HasColumnName("icon5")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon6)
                    .HasColumnName("icon6")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon7)
                    .HasColumnName("icon7")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Icon8)
                    .HasColumnName("icon8")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LeaderGuid)
                    .HasColumnName("leaderGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LootMethod)
                    .HasColumnName("lootMethod")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LootThreshold)
                    .HasColumnName("lootThreshold")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LooterGuid)
                    .HasColumnName("looterGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MasterLooterGuid)
                    .HasColumnName("masterLooterGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RaidDifficulty)
                    .HasColumnName("raidDifficulty")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Guild>(entity =>
            {
                entity.ToTable("guild");

                entity.HasComment("Guild System");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.BackgroundColor).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BankMoney).HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BorderColor).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BorderStyle).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Createdate)
                    .HasColumnName("createdate")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmblemColor).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.EmblemStyle).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasColumnType("varchar(500)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Leaderguid)
                    .HasColumnName("leaderguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Motd)
                    .IsRequired()
                    .HasColumnName("motd")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(24)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<GuildBankEventlog>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.LogGuid, e.TabId })
                    .HasName("PRIMARY");

                entity.ToTable("guild_bank_eventlog");

                entity.HasIndex(e => e.Guildid)
                    .HasName("guildid_key");

                entity.HasIndex(e => e.LogGuid)
                    .HasName("Idx_LogGuid");

                entity.HasIndex(e => e.PlayerGuid)
                    .HasName("Idx_PlayerGuid");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Guild Identificator");

                entity.Property(e => e.LogGuid)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Log record identificator - auxiliary column");

                entity.Property(e => e.TabId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Guild bank TabId");

                entity.Property(e => e.DestTabId)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Destination Tab Id");

                entity.Property(e => e.EventType)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Event type");

                entity.Property(e => e.ItemOrMoney).HasColumnType("int(10) unsigned");

                entity.Property(e => e.ItemStackCount).HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.PlayerGuid).HasColumnType("int(10) unsigned");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Event UNIX time");
            });

            modelBuilder.Entity<GuildBankItem>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.TabId, e.SlotId })
                    .HasName("PRIMARY");

                entity.ToTable("guild_bank_item");

                entity.HasIndex(e => e.Guildid)
                    .HasName("guildid_key");

                entity.HasIndex(e => e.ItemGuid)
                    .HasName("Idx_item_guid");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TabId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SlotId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("item_guid")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GuildBankRight>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.TabId, e.Rid })
                    .HasName("PRIMARY");

                entity.ToTable("guild_bank_right");

                entity.HasIndex(e => e.Guildid)
                    .HasName("guildid_key");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TabId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Rid)
                    .HasColumnName("rid")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Gbright)
                    .HasColumnName("gbright")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.SlotPerDay).HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GuildBankTab>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.TabId })
                    .HasName("PRIMARY");

                entity.ToTable("guild_bank_tab");

                entity.HasIndex(e => e.Guildid)
                    .HasName("guildid_key");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TabId).HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.TabIcon)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TabName)
                    .IsRequired()
                    .HasColumnType("varchar(16)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TabText)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<GuildEventlog>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.LogGuid })
                    .HasName("PRIMARY");

                entity.ToTable("guild_eventlog");

                entity.HasComment("Guild Eventlog");

                entity.HasIndex(e => e.LogGuid)
                    .HasName("Idx_LogGuid");

                entity.HasIndex(e => e.PlayerGuid1)
                    .HasName("Idx_PlayerGuid1");

                entity.HasIndex(e => e.PlayerGuid2)
                    .HasName("Idx_PlayerGuid2");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Guild Identificator");

                entity.Property(e => e.LogGuid)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Log record identificator - auxiliary column");

                entity.Property(e => e.EventType)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("Event type");

                entity.Property(e => e.NewRank)
                    .HasColumnType("tinyint(3) unsigned")
                    .HasComment("New rank(in case promotion/demotion)");

                entity.Property(e => e.PlayerGuid1)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Player 1");

                entity.Property(e => e.PlayerGuid2)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Player 2");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Event UNIX time");
            });

            modelBuilder.Entity<GuildMember>(entity =>
            {
                entity.ToTable("guild_member");

                entity.HasComment("Guild System");

                entity.HasKey(e => e.Guid)
                    .HasName("guid_key");

                entity.HasIndex(e => e.Guildid)
                    .HasName("guildid_key");

                entity.HasIndex(e => new { e.Guildid, e.Rank })
                    .HasName("guildid_rank_key");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Guild Identificator");

                entity.Property(e => e.Offnote)
                    .IsRequired()
                    .HasColumnName("offnote")
                    .HasColumnType("varchar(31)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Pnote)
                    .IsRequired()
                    .HasColumnName("pnote")
                    .HasColumnType("varchar(31)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<GuildMemberWithdraw>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("guild_member_withdraw");

                entity.HasComment("Guild Member Daily Withdraws");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab0)
                    .HasColumnName("tab0")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab1)
                    .HasColumnName("tab1")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab2)
                    .HasColumnName("tab2")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab3)
                    .HasColumnName("tab3")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab4)
                    .HasColumnName("tab4")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Tab5)
                    .HasColumnName("tab5")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<GuildRank>(entity =>
            {
                entity.HasKey(e => new { e.Guildid, e.Rid })
                    .HasName("PRIMARY");

                entity.ToTable("guild_rank");

                entity.HasComment("Guild System");

                entity.HasIndex(e => e.Rid)
                    .HasName("Idx_rid");

                entity.Property(e => e.Guildid)
                    .HasColumnName("guildid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Rid)
                    .HasColumnName("rid")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.BankMoneyPerDay).HasColumnType("int(10) unsigned");

                entity.Property(e => e.Rights)
                    .HasColumnName("rights")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rname)
                    .IsRequired()
                    .HasColumnName("rname")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Instance>(entity =>
            {
                entity.ToTable("instance");

                entity.HasIndex(e => e.Difficulty)
                    .HasName("difficulty");

                entity.HasIndex(e => e.Map)
                    .HasName("map");

                entity.HasIndex(e => e.Resettime)
                    .HasName("resettime");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CompletedEncounters)
                    .HasColumnName("completedEncounters")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Map)
                    .HasColumnName("map")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Resettime)
                    .HasColumnName("resettime")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<InstanceReset>(entity =>
            {
                entity.HasKey(e => new { e.Mapid, e.Difficulty })
                    .HasName("PRIMARY");

                entity.ToTable("instance_reset");

                entity.HasIndex(e => e.Difficulty)
                    .HasName("difficulty");

                entity.Property(e => e.Mapid)
                    .HasColumnName("mapid")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Difficulty)
                    .HasColumnName("difficulty")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Resettime)
                    .HasColumnName("resettime")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<ItemInstance>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("item_instance");

                entity.HasComment("Item System");

                entity.HasIndex(e => e.OwnerGuid)
                    .HasName("idx_owner_guid");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Charges)
                    .HasColumnName("charges")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CreatorGuid)
                    .HasColumnName("creatorGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Durability)
                    .HasColumnName("durability")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Enchantments)
                    .IsRequired()
                    .HasColumnName("enchantments")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Flags)
                    .HasColumnName("flags")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GiftCreatorGuid)
                    .HasColumnName("giftCreatorGuid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ItemEntry)
                    .HasColumnName("itemEntry")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OwnerGuid)
                    .HasColumnName("owner_guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PlayedTime)
                    .HasColumnName("playedTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RandomPropertyId)
                    .HasColumnName("randomPropertyId")
                    .HasColumnType("smallint(5)");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<ItemLootItems>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("item_loot_items");

                entity.Property(e => e.Blocked).HasColumnName("blocked");

                entity.Property(e => e.ContainerId)
                    .HasColumnName("container_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("guid of container (item_instance.guid)");

                entity.Property(e => e.Counted).HasColumnName("counted");

                entity.Property(e => e.Ffa)
                    .HasColumnName("ffa")
                    .HasComment("free-for-all");

                entity.Property(e => e.FollowRules)
                    .HasColumnName("follow_rules")
                    .HasComment("follow loot rules");

                entity.Property(e => e.ItemCount)
                    .HasColumnName("item_count")
                    .HasColumnType("int(10)")
                    .HasComment("stack size");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("loot item entry (item_instance.itemEntry)");

                entity.Property(e => e.NeedsQuest)
                    .HasColumnName("needs_quest")
                    .HasComment("quest drop");

                entity.Property(e => e.RndProp)
                    .HasColumnName("rnd_prop")
                    .HasColumnType("int(10)")
                    .HasComment("random enchantment added when originally rolled");

                entity.Property(e => e.RndSuffix)
                    .HasColumnName("rnd_suffix")
                    .HasColumnType("int(10)")
                    .HasComment("random suffix added when originally rolled");

                entity.Property(e => e.UnderThreshold).HasColumnName("under_threshold");
            });

            modelBuilder.Entity<ItemLootMoney>(entity =>
            {
                entity.HasKey(e => e.ContainerId)
                    .HasName("PRIMARY");

                entity.ToTable("item_loot_money");

                entity.Property(e => e.ContainerId)
                    .HasColumnName("container_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("guid of container (item_instance.guid)");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("money loot (in copper)");
            });

            modelBuilder.Entity<ItemRefundInstance>(entity =>
            {
                entity.HasKey(e => new { e.ItemGuid, e.PlayerGuid })
                    .HasName("PRIMARY");

                entity.ToTable("item_refund_instance");

                entity.HasComment("Item Refund System");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("item_guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Item GUID");

                entity.Property(e => e.PlayerGuid)
                    .HasColumnName("player_guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Player GUID");

                entity.Property(e => e.PaidExtendedCost)
                    .HasColumnName("paidExtendedCost")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.PaidMoney)
                    .HasColumnName("paidMoney")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<ItemSoulboundTradeData>(entity =>
            {
                entity.HasKey(e => e.ItemGuid)
                    .HasName("PRIMARY");

                entity.ToTable("item_soulbound_trade_data");

                entity.HasComment("Item Refund System");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("itemGuid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Item GUID");

                entity.Property(e => e.AllowedPlayers)
                    .IsRequired()
                    .HasColumnName("allowedPlayers")
                    .HasColumnType("text")
                    .HasComment("Space separated GUID list of players who can receive this item in trade")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<LagReports>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PRIMARY");

                entity.ToTable("lag_reports");

                entity.HasComment("Player System");

                entity.Property(e => e.ReportId)
                    .HasColumnName("reportId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LagType)
                    .HasColumnName("lagType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Latency)
                    .HasColumnName("latency")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.PosX).HasColumnName("posX");

                entity.Property(e => e.PosY).HasColumnName("posY");

                entity.Property(e => e.PosZ).HasColumnName("posZ");
            });

            modelBuilder.Entity<LfgData>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PRIMARY");

                entity.ToTable("lfg_data");

                entity.HasComment("LFG Data");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Dungeon)
                    .HasColumnName("dungeon")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Mail>(entity =>
            {
                entity.ToTable("mail");

                entity.HasComment("Mail System");

                entity.HasIndex(e => e.Receiver)
                    .HasName("idx_receiver");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Identifier");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Checked)
                    .HasColumnName("checked")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DeliverTime)
                    .HasColumnName("deliver_time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ExpireTime)
                    .HasColumnName("expire_time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HasItems)
                    .HasColumnName("has_items")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.MailTemplateId)
                    .HasColumnName("mailTemplateId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.MessageType)
                    .HasColumnName("messageType")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Receiver)
                    .HasColumnName("receiver")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Global Unique Identifier");

                entity.Property(e => e.Sender)
                    .HasColumnName("sender")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Global Unique Identifier");

                entity.Property(e => e.Stationery)
                    .HasColumnName("stationery")
                    .HasColumnType("tinyint(3)")
                    .HasDefaultValueSql("'41'");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<MailItems>(entity =>
            {
                entity.HasKey(e => e.ItemGuid)
                    .HasName("PRIMARY");

                entity.ToTable("mail_items");

                entity.HasIndex(e => e.MailId)
                    .HasName("idx_mail_id");

                entity.HasIndex(e => e.Receiver)
                    .HasName("idx_receiver");

                entity.Property(e => e.ItemGuid)
                    .HasColumnName("item_guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MailId)
                    .HasColumnName("mail_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Receiver)
                    .HasColumnName("receiver")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Global Unique Identifier");
            });

            modelBuilder.Entity<PetAura>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.CasterGuid, e.Spell, e.EffectMask })
                    .HasName("PRIMARY");

                entity.ToTable("pet_aura");

                entity.HasComment("Pet System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.CasterGuid)
                    .HasColumnName("casterGuid")
                    .HasColumnType("bigint(20) unsigned")
                    .HasComment("Full Global Unique Identifier");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EffectMask)
                    .HasColumnName("effectMask")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Amount0)
                    .HasColumnName("amount0")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.Amount1)
                    .HasColumnName("amount1")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.Amount2)
                    .HasColumnName("amount2")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.ApplyResilience)
                    .HasColumnName("applyResilience")
                    .HasColumnType("tinyint(3)");

                entity.Property(e => e.BaseAmount0)
                    .HasColumnName("base_amount0")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.BaseAmount1)
                    .HasColumnName("base_amount1")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.BaseAmount2)
                    .HasColumnName("base_amount2")
                    .HasColumnType("mediumint(8)");

                entity.Property(e => e.CritChance).HasColumnName("critChance");

                entity.Property(e => e.MaxDuration)
                    .HasColumnName("maxDuration")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RecalculateMask)
                    .HasColumnName("recalculateMask")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RemainCharges)
                    .HasColumnName("remainCharges")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.RemainTime)
                    .HasColumnName("remainTime")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StackCount)
                    .HasColumnName("stackCount")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<PetSpell>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spell })
                    .HasName("PRIMARY");

                entity.ToTable("pet_spell");

                entity.HasComment("Pet System");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Spell Identifier");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<PetSpellCooldown>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Spell })
                    .HasName("PRIMARY");

                entity.ToTable("pet_spell_cooldown");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier, Low part");

                entity.Property(e => e.Spell)
                    .HasColumnName("spell")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'")
                    .HasComment("Spell Identifier");

                entity.Property(e => e.CategoryEnd)
                    .HasColumnName("categoryEnd")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("categoryId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Spell category Id");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Petition>(entity =>
            {
                entity.HasKey(e => new { e.Ownerguid, e.Type })
                    .HasName("PRIMARY");

                entity.ToTable("petition");

                entity.HasComment("Guild System");

                entity.HasIndex(e => new { e.Ownerguid, e.Petitionguid })
                    .HasName("index_ownerguid_petitionguid")
                    .IsUnique();

                entity.Property(e => e.Ownerguid)
                    .HasColumnName("ownerguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(24)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Petitionguid)
                    .HasColumnName("petitionguid")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<PetitionSign>(entity =>
            {
                entity.HasKey(e => new { e.Petitionguid, e.Playerguid })
                    .HasName("PRIMARY");

                entity.ToTable("petition_sign");

                entity.HasComment("Guild System");

                entity.HasIndex(e => e.Ownerguid)
                    .HasName("Idx_ownerguid");

                entity.HasIndex(e => e.Playerguid)
                    .HasName("Idx_playerguid");

                entity.Property(e => e.Petitionguid)
                    .HasColumnName("petitionguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Playerguid)
                    .HasColumnName("playerguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Ownerguid)
                    .HasColumnName("ownerguid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PlayerAccount)
                    .HasColumnName("player_account")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<PoolQuestSave>(entity =>
            {
                entity.HasKey(e => new { e.PoolId, e.QuestId })
                    .HasName("PRIMARY");

                entity.ToTable("pool_quest_save");

                entity.Property(e => e.PoolId)
                    .HasColumnName("pool_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.QuestId)
                    .HasColumnName("quest_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<PvpstatsBattlegrounds>(entity =>
            {
                entity.ToTable("pvpstats_battlegrounds");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.BracketId)
                    .HasColumnName("bracket_id")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.WinnerFaction)
                    .HasColumnName("winner_faction")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<PvpstatsPlayers>(entity =>
            {
                entity.HasKey(e => new { e.BattlegroundId, e.CharacterGuid })
                    .HasName("PRIMARY");

                entity.ToTable("pvpstats_players");

                entity.Property(e => e.BattlegroundId)
                    .HasColumnName("battleground_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CharacterGuid)
                    .HasColumnName("character_guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Attr1)
                    .HasColumnName("attr_1")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Attr2)
                    .HasColumnName("attr_2")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Attr3)
                    .HasColumnName("attr_3")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Attr4)
                    .HasColumnName("attr_4")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Attr5)
                    .HasColumnName("attr_5")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ScoreBonusHonor)
                    .HasColumnName("score_bonus_honor")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ScoreDamageDone)
                    .HasColumnName("score_damage_done")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ScoreDeaths)
                    .HasColumnName("score_deaths")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ScoreHealingDone)
                    .HasColumnName("score_healing_done")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ScoreHonorableKills)
                    .HasColumnName("score_honorable_kills")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.ScoreKillingBlows)
                    .HasColumnName("score_killing_blows")
                    .HasColumnType("mediumint(8) unsigned");

                entity.Property(e => e.Winner)
                    .HasColumnName("winner")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<QuestTracker>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("quest_tracker");

                entity.Property(e => e.CharacterGuid)
                    .HasColumnName("character_guid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CompletedByGm).HasColumnName("completed_by_gm");

                entity.Property(e => e.CoreHash)
                    .IsRequired()
                    .HasColumnName("core_hash")
                    .HasColumnType("varchar(120)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CoreRevision)
                    .IsRequired()
                    .HasColumnName("core_revision")
                    .HasColumnType("varchar(120)")
                    .HasDefaultValueSql("'0'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(8) unsigned")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.QuestAbandonTime)
                    .HasColumnName("quest_abandon_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuestAcceptTime)
                    .HasColumnName("quest_accept_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuestCompleteTime)
                    .HasColumnName("quest_complete_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ReservedName>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("reserved_name");

                entity.HasComment("Player Reserved Names");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(12)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Respawn>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.SpawnId, e.InstanceId })
                    .HasName("PRIMARY");

                entity.ToTable("respawn");

                entity.HasComment("Stored respawn times");

                entity.HasIndex(e => e.InstanceId)
                    .HasName("idx_instance");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("smallint(10) unsigned");

                entity.Property(e => e.SpawnId)
                    .HasColumnName("spawnId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("instanceId")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MapId)
                    .HasColumnName("mapId")
                    .HasColumnType("smallint(10) unsigned");

                entity.Property(e => e.RespawnTime)
                    .HasColumnName("respawnTime")
                    .HasColumnType("bigint(20) unsigned");
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

            modelBuilder.Entity<WardenAction>(entity =>
            {
                entity.HasKey(e => e.WardenId)
                    .HasName("PRIMARY");

                entity.ToTable("warden_action");

                entity.Property(e => e.WardenId)
                    .HasColumnName("wardenId")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Worldstates>(entity =>
            {
                entity.HasKey(e => e.Entry)
                    .HasName("PRIMARY");

                entity.ToTable("worldstates");

                entity.HasComment("Variable Saves");

                entity.Property(e => e.Entry)
                    .HasColumnName("entry")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(10) unsigned");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
