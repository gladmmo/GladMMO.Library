using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladMMO
{
	public sealed class CommonGameDatabaseContext : DbContext
	{
		public DbSet<PathWaypointModel> PathWaypoints { get; set; }

		public CommonGameDatabaseContext(DbContextOptions<CommonGameDatabaseContext> options) 
			: base(options)
		{
		}

		public CommonGameDatabaseContext()
		{

		}

		//We do the below for local database creation stuff
#if DATABASE_MIGRATION || Database_Migration
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//TODO: Should I have local or also AWS setup here?
			optionsBuilder.UseMySql("Server=192.168.0.3;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}
#endif

		//Due to composite key inmemory testing we need to have this outside of the usual ifdef
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			EntityTypeBuilder<PathWaypointModel> characterEntity = modelBuilder.Entity<PathWaypointModel>();

			characterEntity
				.HasKey(p => new {p.PathId, p.PointId});
		}
	}
}
