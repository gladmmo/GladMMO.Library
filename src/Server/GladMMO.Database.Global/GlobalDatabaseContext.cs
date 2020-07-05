using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class GlobalDatabaseContext : DbContext
	{
		//We do the below for local database creation stuff
#if DATABASE_MIGRATION
		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//TODO: Should I have local or also AWS setup here?
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=guardians.global;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
#endif
	}
}
