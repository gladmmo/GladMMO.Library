using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GladMMO
{
    public partial class wotlk_authContext : DbContext
    {
        public wotlk_authContext()
        {
        }

        public wotlk_authContext(DbContextOptions<wotlk_authContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountAccess> AccountAccess { get; set; }
        public virtual DbSet<AccountBanned> AccountBanned { get; set; }
        public virtual DbSet<AccountMuted> AccountMuted { get; set; }
        public virtual DbSet<Autobroadcast> Autobroadcast { get; set; }
        public virtual DbSet<BuildInfo> BuildInfo { get; set; }
        public virtual DbSet<IpBanned> IpBanned { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<LogsIpActions> LogsIpActions { get; set; }
        public virtual DbSet<RbacAccountPermissions> RbacAccountPermissions { get; set; }
        public virtual DbSet<RbacDefaultPermissions> RbacDefaultPermissions { get; set; }
        public virtual DbSet<RbacLinkedPermissions> RbacLinkedPermissions { get; set; }
        public virtual DbSet<RbacPermissions> RbacPermissions { get; set; }
        public virtual DbSet<Realmcharacters> Realmcharacters { get; set; }
        public virtual DbSet<Realmlist> Realmlist { get; set; }
        public virtual DbSet<SecretDigest> SecretDigest { get; set; }
        public virtual DbSet<Updates> Updates { get; set; }
        public virtual DbSet<UpdatesInclude> UpdatesInclude { get; set; }
        public virtual DbSet<Uptime> Uptime { get; set; }
        public virtual DbSet<VwLogHistory> VwLogHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasComment("Account System");

                entity.HasIndex(e => e.Username)
                    .HasName("idx_username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Identifier");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Expansion)
                    .HasColumnName("expansion")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.FailedLogins)
                    .HasColumnName("failed_logins")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Joindate)
                    .HasColumnName("joindate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastAttemptIp)
                    .IsRequired()
                    .HasColumnName("last_attempt_ip")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastIp)
                    .IsRequired()
                    .HasColumnName("last_ip")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Locale)
                    .HasColumnName("locale")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LockCountry)
                    .IsRequired()
                    .HasColumnName("lock_country")
                    .HasColumnType("varchar(2)")
                    .HasDefaultValueSql("'00'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Locked)
                    .HasColumnName("locked")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Muteby)
                    .IsRequired()
                    .HasColumnName("muteby")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mutereason)
                    .IsRequired()
                    .HasColumnName("mutereason")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mutetime)
                    .HasColumnName("mutetime")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Online)
                    .HasColumnName("online")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Os)
                    .IsRequired()
                    .HasColumnName("os")
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Recruiter)
                    .HasColumnName("recruiter")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RegMail)
                    .IsRequired()
                    .HasColumnName("reg_mail")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.SessionKeyAuth)
                    .HasColumnName("session_key_auth")
                    .HasMaxLength(40)
                    .IsFixedLength();

                entity.Property(e => e.SessionKeyBnet)
                    .HasColumnName("session_key_bnet")
                    .HasMaxLength(64);

                entity.Property(e => e.TotpSecret)
                    .HasColumnName("totp_secret")
                    .HasMaxLength(128);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Verifier)
                    .IsRequired()
                    .HasColumnName("verifier")
                    .HasMaxLength(32)
                    .IsFixedLength();
            });

            modelBuilder.Entity<AccountAccess>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.RealmId })
                    .HasName("PRIMARY");

                entity.ToTable("account_access");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RealmId)
                    .HasColumnName("RealmID")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SecurityLevel).HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<AccountBanned>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Bandate })
                    .HasName("PRIMARY");

                entity.ToTable("account_banned");

                entity.HasComment("Ban List");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account id");

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

            modelBuilder.Entity<AccountMuted>(entity =>
            {
                entity.HasKey(e => new { e.Guid, e.Mutedate })
                    .HasName("PRIMARY");

                entity.ToTable("account_muted");

                entity.HasComment("mute List");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Global Unique Identifier");

                entity.Property(e => e.Mutedate)
                    .HasColumnName("mutedate")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Mutedby)
                    .IsRequired()
                    .HasColumnName("mutedby")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mutereason)
                    .IsRequired()
                    .HasColumnName("mutereason")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mutetime)
                    .HasColumnName("mutetime")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Autobroadcast>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Realmid })
                    .HasName("PRIMARY");

                entity.ToTable("autobroadcast");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("tinyint(3) unsigned")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Realmid)
                    .HasColumnName("realmid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<BuildInfo>(entity =>
            {
                entity.HasKey(e => e.Build)
                    .HasName("PRIMARY");

                entity.ToTable("build_info");

                entity.Property(e => e.Build)
                    .HasColumnName("build")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BugfixVersion)
                    .HasColumnName("bugfixVersion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HotfixVersion)
                    .HasColumnName("hotfixVersion")
                    .HasColumnType("char(3)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Mac64AuthSeed)
                    .HasColumnName("mac64AuthSeed")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.MacChecksumSeed)
                    .HasColumnName("macChecksumSeed")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.MajorVersion)
                    .HasColumnName("majorVersion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinorVersion)
                    .HasColumnName("minorVersion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Win64AuthSeed)
                    .HasColumnName("win64AuthSeed")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.WinAuthSeed)
                    .HasColumnName("winAuthSeed")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.WinChecksumSeed)
                    .HasColumnName("winChecksumSeed")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<IpBanned>(entity =>
            {
                entity.HasKey(e => new { e.Ip, e.Bandate })
                    .HasName("PRIMARY");

                entity.ToTable("ip_banned");

                entity.HasComment("Banned IPs");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Bandate)
                    .HasColumnName("bandate")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Bannedby)
                    .IsRequired()
                    .HasColumnName("bannedby")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'[Console]'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Banreason)
                    .IsRequired()
                    .HasColumnName("banreason")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'no reason'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Unbandate)
                    .HasColumnName("unbandate")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("logs");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Realm)
                    .HasColumnName("realm")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.String)
                    .HasColumnName("string")
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<LogsIpActions>(entity =>
            {
                entity.ToTable("logs_ip_actions");

                entity.HasComment("Used to log ips of individual actions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Unique Identifier");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account ID");

                entity.Property(e => e.CharacterGuid)
                    .HasColumnName("character_guid")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Character Guid");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text")
                    .HasComment("Allows users to add a comment")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("ip")
                    .HasColumnType("varchar(15)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RealmId)
                    .HasColumnName("realm_id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Realm ID");

                entity.Property(e => e.Systemnote)
                    .HasColumnName("systemnote")
                    .HasColumnType("text")
                    .HasComment("Notes inserted by system")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("Timestamp");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Unixtime)
                    .HasColumnName("unixtime")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Unixtime");
            });

            modelBuilder.Entity<RbacAccountPermissions>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.PermissionId, e.RealmId })
                    .HasName("PRIMARY");

                entity.ToTable("rbac_account_permissions");

                entity.HasComment("Account-Permission relation");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("fk__rbac_account_roles__rbac_permissions");

                entity.Property(e => e.AccountId)
                    .HasColumnName("accountId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Account id");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permissionId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Permission id");

                entity.Property(e => e.RealmId)
                    .HasColumnName("realmId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("Realm Id, -1 means all");

                entity.Property(e => e.Granted)
                    .IsRequired()
                    .HasColumnName("granted")
                    .HasDefaultValueSql("'1'")
                    .HasComment("Granted = 1, Denied = 0");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.RbacAccountPermissions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk__rbac_account_permissions__account");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RbacAccountPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("fk__rbac_account_roles__rbac_permissions");
            });

            modelBuilder.Entity<RbacDefaultPermissions>(entity =>
            {
                entity.HasKey(e => new { e.SecId, e.PermissionId, e.RealmId })
                    .HasName("PRIMARY");

                entity.ToTable("rbac_default_permissions");

                entity.HasComment("Default permission to assign to different account security levels");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("fk__rbac_default_permissions__rbac_permissions");

                entity.Property(e => e.SecId)
                    .HasColumnName("secId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Security Level id");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permissionId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("permission id");

                entity.Property(e => e.RealmId)
                    .HasColumnName("realmId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("Realm Id, -1 means all");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RbacDefaultPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__rbac_default_permissions__rbac_permissions");
            });

            modelBuilder.Entity<RbacLinkedPermissions>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.LinkedId })
                    .HasName("PRIMARY");

                entity.ToTable("rbac_linked_permissions");

                entity.HasComment("Permission - Linked Permission relation");

                entity.HasIndex(e => e.Id)
                    .HasName("fk__rbac_linked_permissions__rbac_permissions1");

                entity.HasIndex(e => e.LinkedId)
                    .HasName("fk__rbac_linked_permissions__rbac_permissions2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Permission id");

                entity.Property(e => e.LinkedId)
                    .HasColumnName("linkedId")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Linked Permission id");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.RbacLinkedPermissionsIdNavigation)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("fk__rbac_linked_permissions__rbac_permissions1");

                entity.HasOne(d => d.Linked)
                    .WithMany(p => p.RbacLinkedPermissionsLinked)
                    .HasForeignKey(d => d.LinkedId)
                    .HasConstraintName("fk__rbac_linked_permissions__rbac_permissions2");
            });

            modelBuilder.Entity<RbacPermissions>(entity =>
            {
                entity.ToTable("rbac_permissions");

                entity.HasComment("Permission List");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned")
                    .HasComment("Permission id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasComment("Permission name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Realmcharacters>(entity =>
            {
                entity.HasKey(e => new { e.Realmid, e.Acctid })
                    .HasName("PRIMARY");

                entity.ToTable("realmcharacters");

                entity.HasComment("Realm Character Tracker");

                entity.HasIndex(e => e.Acctid)
                    .HasName("acctid");

                entity.Property(e => e.Realmid)
                    .HasColumnName("realmid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Acctid)
                    .HasColumnName("acctid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Numchars)
                    .HasColumnName("numchars")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<Realmlist>(entity =>
            {
                entity.ToTable("realmlist");

                entity.HasComment("Realm System");

                entity.HasIndex(e => e.Name)
                    .HasName("idx_name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AllowedSecurityLevel)
                    .HasColumnName("allowedSecurityLevel")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasColumnType("tinyint(3) unsigned")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.Gamebuild)
                    .HasColumnName("gamebuild")
                    .HasColumnType("int(10) unsigned")
                    .HasDefaultValueSql("'12340'");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.LocalAddress)
                    .IsRequired()
                    .HasColumnName("localAddress")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'127.0.0.1'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LocalSubnetMask)
                    .IsRequired()
                    .HasColumnName("localSubnetMask")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'255.255.255.0'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Population)
                    .HasColumnName("population")
                    .HasColumnType("float unsigned");

                entity.Property(e => e.Port)
                    .HasColumnName("port")
                    .HasColumnType("smallint(5) unsigned")
                    .HasDefaultValueSql("'8085'");

                entity.Property(e => e.Timezone)
                    .HasColumnName("timezone")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<SecretDigest>(entity =>
            {
                entity.ToTable("secret_digest");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Digest)
                    .IsRequired()
                    .HasColumnName("digest")
                    .HasColumnType("varchar(100)")
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

            modelBuilder.Entity<Uptime>(entity =>
            {
                entity.HasKey(e => new { e.Realmid, e.Starttime })
                    .HasName("PRIMARY");

                entity.ToTable("uptime");

                entity.HasComment("Uptime system");

                entity.Property(e => e.Realmid)
                    .HasColumnName("realmid")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Maxplayers)
                    .HasColumnName("maxplayers")
                    .HasColumnType("smallint(5) unsigned");

                entity.Property(e => e.Revision)
                    .IsRequired()
                    .HasColumnName("revision")
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("'Trinitycore'")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Uptime1)
                    .HasColumnName("uptime")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<VwLogHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_log_history");

                entity.Property(e => e.FirstLogged)
                    .HasColumnName("First Logged")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastLogged)
                    .HasColumnName("Last Logged")
                    .HasColumnType("datetime");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Occurrences).HasColumnType("bigint(21)");

                entity.Property(e => e.Realm)
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("''")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.String)
                    .HasColumnName("string")
                    .HasColumnType("text")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
