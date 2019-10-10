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

		private IEntityGuidMappable<CharacterAppearanceResponse> CharacterAppearanceMappable { get; }

		/// <inheritdoc />
		public CharacterSelectionOnStartQueryForCharacterListInitializable([NotNull] ILog logger,
			[NotNull] ICharacterService characterServiceQueryable,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IEntityGuidMappable<CharacterAppearanceResponse> characterAppearanceMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CharacterServiceQueryable = characterServiceQueryable ?? throw new ArgumentNullException(nameof(characterServiceQueryable));
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			CharacterAppearanceMappable = characterAppearanceMappable ?? throw new ArgumentNullException(nameof(characterAppearanceMappable));
		}

		/// <inheritdoc />
		public async Task OnGameInitialized()
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

					//TODO: Optimize below awaits.
					//Do a namequery so it's in the cache for when anything tries to get entities name.
					await EntityNameQueryable.RetrieveAsync(entityGuid)
						.ConfigureAwait(false);

					var appearanceResponse = await CharacterServiceQueryable.GetCharacterAppearance(entityGuid.EntityId)
						.ConfigureAwait(false);

					//Don't throw, because we actually don't want to stop the
					//character screen from working just because we can't visually display some stuff.
					if(!appearanceResponse.isSuccessful)
						Logger.Error($"Failed to query for Character: {entityGuid.EntityId} appearance. Reason: {appearanceResponse.ResultCode}");

					//TODO: Hanlde error.
					CharacterAppearanceMappable.AddObject(entityGuid, appearanceResponse.Result);

					OnCharacterSelectionEntryChanged?.Invoke(this, new CharacterSelectionEntryDataChangeEventArgs(entityGuid));
				}
			}
			catch(Exception e)
			{
				Logger.Error($"Encountered Error: {e.Message}");
				throw;
			}
		}
	}
}
