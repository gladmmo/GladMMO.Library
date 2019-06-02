using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//TODO: Non-server specific data should go in a seperate database. Not the world/gameserver database.
	public sealed class NpcDatabaseContext : DbContext
	{
		public DbSet<NPCEntryModel> Entries { get; set; }

		public DbSet<NPCTemplateModel> Templates { get; set; }

		public NpcDatabaseContext(DbContextOptions<NpcDatabaseContext> options)
			: base(options)
		{

		}

		public NpcDatabaseContext()
		{

		}

#if DATABASE_MIGRATION
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//TODO: Should I have local or also AWS setup here?
			optionsBuilder.UseMySql("Server=localhost;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}
#endif
	}
}
