using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladMMO
{
	public sealed class GlobalDatabaseContext : DbContext
	{
		public DbSet<ServiceEntryModel> Services { get; set; }

		public DbSet<ServiceEndpointModel> ServiceEndpoints { get; set; }

		public GlobalDatabaseContext(DbContextOptions<GlobalDatabaseContext> options)
			: base(options)
		{

		}

		public GlobalDatabaseContext()
		{

		}

		//We do the below for local database creation stuff
#if DATABASE_MIGRATION
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//TODO: Should I have local or also AWS setup here?
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=guardians.global;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}
#endif

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetupServiceEntryModel(modelBuilder);
			SetupServiceEndpointEntryModel(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private static void SetupServiceEntryModel(ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<ServiceEntryModel> serviceEntity = modelBuilder.Entity<ServiceEntryModel>();

			//Makes the name a unique entry.
			serviceEntity
				.HasAlternateKey(s => s.ServiceName);
		}

		private static void SetupServiceEndpointEntryModel(ModelBuilder modelBuilder)
		{
			EntityTypeBuilder<ServiceEndpointModel> serviceEndpointEntity = modelBuilder.Entity<ServiceEndpointModel>();

			//Makes a service endpoint unique for a service type, locale and deployment mode.
			serviceEndpointEntity
				.HasKey(s => new { s.ServiceId, s.Locale, s.Mode });
		}
	}
}
