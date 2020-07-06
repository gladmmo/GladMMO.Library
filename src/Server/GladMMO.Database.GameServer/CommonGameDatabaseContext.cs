using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GladMMO
{
	//This is from way way back, but because migrations reference it we can never ever remove it.
	[Obsolete("This is here only for legacy reasons. Never add anything to it.")]
	public sealed class CommonGameDatabaseContext : DbContext
	{
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
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}
#endif

		//Due to composite key inmemory testing we need to have this outside of the usual ifdef
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
