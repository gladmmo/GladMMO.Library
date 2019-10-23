using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladMMO
{
	public sealed class CharacterDatabaseContext : DbContext
	{
		public DbSet<CharacterEntryModel> Characters { get; set; }

		public DbSet<CharacterSessionModel> CharacterSessions { get; set; }

		//TODO: This should not be in the character database
		public DbSet<ZoneInstanceEntryModel> ZoneEntries { get; set; }

		public DbSet<CharacterLocationModel> CharacterLocations { get; set; }

		public DbSet<ClaimedSessionsModel> ClaimedSession { get; set; }

		/// <summary>
		/// The character friend requests.
		/// </summary>
		public DbSet<CharacterFriendRelationshipModel> CharacterFriendRequests { get; set; }

		//TODO: This is getting ridiculous, we can't have everything in this database.
		public DbSet<GuildEntryModel> Guilds { get; set; }

		public DbSet<CharacterGuildMemberRelationshipModel> GuildMembers { get; set; }

		public DbSet<CharacterGroupEntryModel> Groups { get; set; }

		public DbSet<CharacterGroupMembershipModel> GroupMembers { get; set; }

		/// <summary>
		/// Set/Table for invites to <see cref="Groups"/>.
		/// </summary>
		public DbSet<CharacterGroupInviteEntryModel> GroupInvites { get; set; }

		public DbSet<CharacterAppearanceModel> CharacterAppearances { get; set; }

		public DbSet<CharacterDataModel> CharacterData { get; set; }

		public DbSet<CharacterFriendModel> CharacterFriends { get; set; }

		public CharacterDatabaseContext(DbContextOptions<CharacterDatabaseContext> options) 
			: base(options)
		{
		}

		public CharacterDatabaseContext()
		{

		}

		//We do the below for local database creation stuff
#if DATABASE_MIGRATION
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//TODO: Should I have local or also AWS setup here?
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			EntityTypeBuilder<CharacterEntryModel> characterEntity = modelBuilder.Entity<CharacterEntryModel>();

			//Use the below due to issue on Pomelo: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/252
			characterEntity
				.HasIndex(c => c.CharacterName)
				.IsUnique();

			characterEntity
				.HasIndex(c => c.AccountId)
				.IsUnique(false);

			characterEntity
				.HasAlternateKey(c => new { c.PlayFabId, c.PlayFabCharacterId });

			characterEntity
				.HasIndex(c => c.PlayFabId);

			//Sessions should enforce uniqueness on both character id and account id.
			EntityTypeBuilder<CharacterSessionModel> sessionEntity = modelBuilder.Entity<CharacterSessionModel>();

			//Use the below due to issue on Pomelo: https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/252
			sessionEntity
				.HasIndex(s => s.CharacterId)
				.IsUnique();

			//TODO: Enforce characterid and accountid match the character
			//We no longer have a uniquness constraint here so that inactive sessions
			//from multiple characters on the account can exist
			//sessionEntity.HasAlternateKey(s => s.AccountId);

			sessionEntity
				.HasOne(s => s.CharacterEntry)
				.WithOne()
				.HasForeignKey<CharacterSessionModel>(s => s.CharacterId);

			sessionEntity
				.HasOne(s => s.ZoneEntry)
				.WithMany()
				.HasForeignKey(c => c.ZoneId);

			EntityTypeBuilder<ClaimedSessionsModel> claimedSessionModel = modelBuilder.Entity<ClaimedSessionsModel>();

			claimedSessionModel
				.HasOne(s => s.Session)
				.WithOne()
				.HasForeignKey<ClaimedSessionsModel>(s => s.CharacterId);

			EntityTypeBuilder<ZoneInstanceEntryModel> zoneEntity = modelBuilder.Entity<ZoneInstanceEntryModel>();

			//This makes it so only one public IP/Port can be in the database by making the data pair unique
			zoneEntity
				.HasAlternateKey(model => new { model.ZoneServerAddress, model.ZoneServerPort });

			EntityTypeBuilder<CharacterFriendRelationshipModel> requestEntity = modelBuilder.Entity<CharacterFriendRelationshipModel>();

			requestEntity
				.HasAlternateKey(model => new { model.RequestingCharacterId, model.TargetRequestCharacterId });

			requestEntity
				.HasIndex(model => model.DirectionalUniqueness)
				.IsUnique();

			EntityTypeBuilder<GuildEntryModel> guildsEntryEntity = modelBuilder.Entity<GuildEntryModel>();

			guildsEntryEntity
				.HasAlternateKey(model => model.GuildMasterCharacterId);

			//This doesn't need to be a key since we likely won't do lookups for it.
			guildsEntryEntity
				.HasIndex(model => model.GuildName)
				.IsUnique();

			//CharacterGuildMemberRelationshipModel
			//EntityTypeBuilder<CharacterGuildMemberRelationshipModel> guildMemberEntityEntity = modelBuilder.Entity<CharacterGuildMemberRelationshipModel>();
			//guildMemberEntityEntity.

			//It's important that the leader id is unique and an alternative key
			EntityTypeBuilder<CharacterGroupEntryModel> groupsEntries = modelBuilder.Entity<CharacterGroupEntryModel>();
			groupsEntries
				.HasAlternateKey(model => model.LeaderCharacterId);

			EntityTypeBuilder<CharacterFriendModel> characterFriendsEntity = modelBuilder.Entity<CharacterFriendModel>();

			characterFriendsEntity
				.HasKey(model => new {model.CharacterId, model.FriendCharacterId});

			characterFriendsEntity
				.HasIndex(model => model.CharacterId);
		}
#endif
	}
}
