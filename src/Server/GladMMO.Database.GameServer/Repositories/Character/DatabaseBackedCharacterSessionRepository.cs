using System; using FreecraftCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace GladMMO
{
	
	public sealed class DatabaseBackedCharacterSessionRepository : ICharacterSessionRepository
	{
		private CharacterDatabaseContext Context { get; }

		//TODO: Move over to generic crud for crud methods. only delete is using it atm
		private IGenericRepositoryCrudable<int, CharacterSessionModel> GenericCrudService { get; }

		private ILogger<DatabaseBackedCharacterSessionRepository> Logger { get; }

		/// <inheritdoc />
		public DatabaseBackedCharacterSessionRepository(CharacterDatabaseContext context,
			[NotNull] ILogger<DatabaseBackedCharacterSessionRepository> logger)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GenericCrudService = new GeneralGenericCrudRepositoryProvider<int, CharacterSessionModel>(context.CharacterSessions, context);
		}

		/// <inheritdoc />
		public async Task<bool> ContainsAsync(int key)
		{
			CheckAndThrowKey(key);

			return null != await Context
				.CharacterSessions
				.FindAsync(key);
		}

		/// <inheritdoc />
		public async Task<bool> TryCreateAsync(CharacterSessionModel model)
		{
			if(model == null) throw new ArgumentNullException(nameof(model));

			//TODO: look into transactions/locks
			await Context
				.CharacterSessions
				.AddAsync(model);
			
			//TODO: This can throw due to race condition
			int rowChangedCount = await Context.SaveChangesAsync();

			return rowChangedCount != 0;
		}

		/// <inheritdoc />
		public Task<CharacterSessionModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			CheckAndThrowKey(key);

			return GenericCrudService.RetrieveAsync(key, includeNavigationProperties);
		}

		private static void CheckAndThrowKey(int key)
		{
			if(key < 0) throw new ArgumentOutOfRangeException(nameof(key));
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericCrudService.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> AccountHasActiveSession(int accountId)
		{
			CheckAndThrowKey(accountId);

			return Context
				.ClaimedSession
				.AnyAsync(cs => cs.Session.CharacterEntry.AccountId == accountId);
		}

		/// <inheritdoc />
		public async Task<bool> CharacterHasActiveSession(int characterId)
		{
			return await Context.ClaimedSession.FindAsync(characterId) != null;
		}

		/// <inheritdoc />
		public async Task<bool> TryClaimUnclaimedSession(int accountId, int characterId)
		{
			//TODO: Pomelo MySql doesn't support out values on stored proc see: https://github.com/mysql-net/MySqlConnector/issues/231
			/*MySqlParameter parameter = new MySqlParameter(@"@result", false)
			{
				Direction = ParameterDirection.Output
			};

			await Context.Database
				.ExecuteSqlCommandAsync($@"CALL create_claimed_session({accountId},{characterId},@result)", parameter);

			return (bool)parameter.Value;*/

			try
			{
				await Context.Database
					.ExecuteSqlCommandAsync($@"CALL create_claimed_session({accountId},{characterId})");

				return true;
			}
			catch(Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to claim session for AccountId:CharacterId {accountId}:{characterId} Reason: {e.Message}");

				//TODO: Log
				return false;
			}
		}

		/// <inheritdoc />
		public async Task<bool> TryDeleteClaimedSession(int characterId)
		{
			return await new GeneralGenericCrudRepositoryProvider<int, ClaimedSessionsModel>(Context.ClaimedSession, Context)
				.TryDeleteAsync(characterId)
				.ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public Task<ClaimedSessionsModel> RetrieveClaimedSessionByAccountId(int accountId)
		{
			//Just assume we have one, they should check before calling.
			return Context.ClaimedSession
				.Include(s => s.Session)
				.ThenInclude(s => s.CharacterEntry)
				.FirstAsync(s => s.Session.CharacterEntry.AccountId == accountId);
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterSessionModel model)
		{
			GeneralGenericCrudRepositoryProvider<int, CharacterSessionModel> crudProvider = new GeneralGenericCrudRepositoryProvider<int, CharacterSessionModel>(Context.CharacterSessions, Context);

			return crudProvider.UpdateAsync(key, model);
		}
	}
}
