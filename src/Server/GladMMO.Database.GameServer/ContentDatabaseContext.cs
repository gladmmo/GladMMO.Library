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
			optionsBuilder.UseMySql("Server=localhost;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
#endif
	}
}
