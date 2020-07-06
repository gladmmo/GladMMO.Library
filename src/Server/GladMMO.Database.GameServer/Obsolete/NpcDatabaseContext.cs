using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	//This is from way way back, but because migrations reference it we can never ever remove it.
	[Obsolete("This is here only for legacy reasons. Never add anything to it.")]
	internal sealed class NpcDatabaseContext : DbContext
	{
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
			optionsBuilder.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");

			base.OnConfiguring(optionsBuilder);
		}
#endif
	}
}
