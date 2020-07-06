using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreCharacterRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, Characters>, ITrinityCharacterRepository
	{
		private wotlk_worldContext WorldContext { get; }

		private ITrinityCharacterActionBarRepository ActionBarRepository { get; }

		public TrinityCoreCharacterRepository(wotlk_charactersContext context, [JetBrains.Annotations.NotNull] wotlk_worldContext worldContext,
			[JetBrains.Annotations.NotNull] ITrinityCharacterActionBarRepository actionBarRepository) 
			: base(context)
		{
			WorldContext = worldContext ?? throw new ArgumentNullException(nameof(worldContext));
			ActionBarRepository = actionBarRepository ?? throw new ArgumentNullException(nameof(actionBarRepository));
		}

		public async Task<string> RetrieveNameAsync(uint key)
		{
			Characters character = await Context
				.Characters
				.FindAsync(key);

			return character.Name;
		}

		public async Task<Characters> RetrieveAsync(string characterName)
		{
			Characters character = await Context
				.Characters
				.FirstAsync(c => c.Name == characterName); //TODO: Should this be normalized?

			return character;
		}

		public override async Task<bool> TryCreateAsync(Characters model)
		{
			bool creationResult = await base.TryCreateAsync(model);

			//We also need to add some DEFAULT stuff
			if (creationResult)
			{
				PlayercreateinfoAction[] actions = await WorldContext
					.PlayercreateinfoAction
					.Where(a => a.Class == model.Class && a.Race == model.Race)
					.ToArrayAsync();

				//Now we need to add their action bar, these are the default
				CharacterAction[] characterActions = actions.Select(a => new CharacterAction()
					{
						Action = a.Action,
						Button = (byte) a.Button,
						Guid = model.Guid,
						Spec = 0, //TODO: What should this be??
						Type = (byte) a.Type
					})
					.ToArray();

				await ActionBarRepository.AddAllAsync(characterActions);
			}

			return creationResult;
		}

		public async Task<bool> ContainsAsync(string characterName)
		{
			return await Context
				.Characters
				.AnyAsync(c => c.Name == characterName); //TODO: Should this be normalized?
		}

		public async Task<int[]> CharacterIdsForAccountId(int accountId)
		{
			//TODO: uint
			return await Context
				.Characters
				.Where(c => c.Account == accountId)
				.Select(c => c.Guid)
				.Cast<int>()
				.ToArrayAsync();
		}

		public async Task<bool> AccountHasActiveSession(int accountId)
		{
			uint castedAccountId = (uint)accountId;

			return await Context
				.Characters
				.AnyAsync(c => c.Account == castedAccountId && c.Online > 0);
		}

		public async Task<Characters> RetrieveClaimedSessionByAccountId(int accountId)
		{
			uint castedAccountId = (uint)accountId;

			return await Context
				.Characters
				.FirstOrDefaultAsync(c => c.Account == castedAccountId && c.Online > 0);
		}
	}
}
