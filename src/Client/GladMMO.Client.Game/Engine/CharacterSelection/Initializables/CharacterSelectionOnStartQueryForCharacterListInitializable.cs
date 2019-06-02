using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ICharacterSelectionEntryDataChangeEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class CharacterSelectionOnStartQueryForCharacterListInitializable : IGameInitializable, ICharacterSelectionEntryDataChangeEventSubscribable
	{
		private ILog Logger { get; }

		/// <inheritdoc />
		public event EventHandler<CharacterSelectionEntryDataChangeEventArgs> OnCharacterSelectionEntryChanged;

		private ICharacterService CharacterServiceQueryable { get; }

		private IEntityNameQueryable EntityNameQueryable { get; }

		/// <inheritdoc />
		public CharacterSelectionOnStartQueryForCharacterListInitializable([NotNull] ILog logger,
			[NotNull] ICharacterService characterServiceQueryable,
			[NotNull] IEntityNameQueryable entityNameQueryable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CharacterServiceQueryable = characterServiceQueryable ?? throw new ArgumentNullException(nameof(characterServiceQueryable));
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
		}

		/// <inheritdoc />
		public async Task OnGameInitialized()
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					CharacterListResponse listResponse = await CharacterServiceQueryable.GetCharacters()
						.ConfigureAwait(false);

					//TODO: Handle errors
					foreach(var character in listResponse.CharacterIds)
					{
						var entityGuid = new NetworkEntityGuidBuilder()
							.WithId(character)
							.WithType(EntityType.Player)
							.Build();

						//Do a namequery so it's in the cache for when anything tries to get entities name.
						await EntityNameQueryable.RetrieveAsync(entityGuid)
							.ConfigureAwait(false);

						OnCharacterSelectionEntryChanged?.Invoke(this, new CharacterSelectionEntryDataChangeEventArgs(entityGuid));
					}
				}
				catch(Exception e)
				{
					Logger.Error($"Encountered Error: {e.Message}");
					throw;
				}
			});

			return;
		}
	}
}
