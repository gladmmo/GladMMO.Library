﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guardians
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
			optionsBuilder.UseMySql("Server=localhost;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			EntityTypeBuilder<PathWaypointModel> characterEntity = modelBuilder.Entity<PathWaypointModel>();

			characterEntity
				.HasKey(p => new {p.PathId, p.PointId});
		}
#endif
	}
}
