using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterActionBarRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterActionBarEntry>, ICharacterActionBarRepository
	{
		public DatabaseBackedCharacterActionBarRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}

		public override Task<CharacterActionBarEntry> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			if (includeNavigationProperties)
			{
				return Context.CharacterActionBars
					.Include(cab => cab.LinkedSpell)
					.FirstAsync(cab => cab.CharacterId == key);
			}
			else
			{
				return Context.CharacterActionBars
					.FirstAsync(cab => cab.CharacterId == key);
			}
		}

		public override Task<bool> ContainsAsync(int key)
		{
			return Context.CharacterActionBars
				.AnyAsync(cab => cab.CharacterId == key);
		}

		public override async Task UpdateAsync(int key, CharacterActionBarEntry model)
		{
			Context.CharacterActionBars
				.Update(model);

			await Context.SaveChangesAsync();
		}

		public override async Task<bool> TryDeleteAsync(int key)
		{
			CharacterActionBarEntry[] actionBarEntries = await Context.CharacterActionBars
				.Where(cab => cab.CharacterId == key)
				.ToArrayAsync();

			Context.CharacterActionBars
				.RemoveRange(actionBarEntries);

			await Context.SaveChangesAsync();
			return true;
		}
	}
}
