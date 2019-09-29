using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladMMO
{
	public sealed class ContentDatabaseContext : DbContext
	{
		public DbSet<WorldEntryModel> Worlds { get; set; }

		public DbSet<AvatarEntryModel> Avatars { get; set; }

		public DbSet<CreatureModelEntryModel> CreatureModels { get; set; }

		public DbSet<CreatureTemplateEntryModel> CreatureTemplates { get; set; }

		public DbSet<CreatureEntryModel> Creatures { get; set; }

		public DbSet<GameObjectModelEntryModel> GameObjectModels { get; set; }

		public DbSet<GameObjectTemplateEntryModel> GameObjectTemplates { get; set; }

		public DbSet<GameObjectEntryModel> GameObjects { get; set; }

		public DbSet<PlayerSpawnPointEntryModel> PlayerSpawnPoints { get; set; }

		public DbSet<GameObjectWorldTeleporterEntryModel> WorldTeleporters { get; set; }

		public ContentDatabaseContext(DbContextOptions<ContentDatabaseContext> options) 
			: base(options)
		{

		}

		public ContentDatabaseContext()
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
		}
#endif
	}
}
