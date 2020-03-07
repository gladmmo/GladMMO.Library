using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

		private IEntityGuidMappable<CharacterDataInstance> InitialCharacterDataInstance { get; }

		/// <inheritdoc />
		public CharacterSelectionOnStartQueryForCharacterListInitializable([NotNull] ILog logger,
			[NotNull] ICharacterService characterServiceQueryable,
			[NotNull] IEntityNameQueryable entityNameQueryable,
			[NotNull] IEntityGuidMappable<CharacterAppearanceResponse> characterAppearanceMappable,
			[NotNull] IEntityGuidMappable<CharacterDataInstance> initialCharacterDataInstance)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CharacterServiceQueryable = characterServiceQueryable ?? throw new ArgumentNullException(nameof(characterServiceQueryable));
			EntityNameQueryable = entityNameQueryable ?? throw new ArgumentNullException(nameof(entityNameQueryable));
			CharacterAppearanceMappable = characterAppearanceMappable ?? throw new ArgumentNullException(nameof(characterAppearanceMappable));
			InitialCharacterDataInstance = initialCharacterDataInstance ?? throw new ArgumentNullException(nameof(initialCharacterDataInstance));
		}

		/// <inheritdoc />
		public async Task OnGameInitialized()
		{
			try
			{
				CharacterListResponse listResponse = await CharacterServiceQueryable.GetCharacters()
					.ConfigureAwaitFalse();

				//TODO: Handle errors
				foreach(var character in listResponse.CharacterIds)
				{
					var entityGuid = new ObjectGuidBuilder()
						.WithId(character)
						.WithType(EntityTypeId.TYPEID_PLAYER)
						.Build();

					//TODO: Optimize below awaits.
					//Do a namequery so it's in the cache for when anything tries to get entities name.
					await EntityNameQueryable.RetrieveAsync(entityGuid)
						.ConfigureAwaitFalse();

					var appearanceResponse = await CharacterServiceQueryable.GetCharacterAppearance(entityGuid.CurrentObjectGuid)
						.ConfigureAwaitFalse();

					var characterData = await CharacterServiceQueryable.GetCharacterData(entityGuid.CurrentObjectGuid)
						.ConfigureAwaitFalse();

					//Don't throw, because we actually don't want to stop the
					//character screen from working just because we can't visually display some stuff.
					if(!appearanceResponse.isSuccessful)
						Logger.Error($"Failed to query for Character: {entityGuid.CurrentObjectGuid} appearance. Reason: {appearanceResponse.ResultCode}");

					//TODO: Handle errors.
					CharacterAppearanceMappable.AddObject(entityGuid, appearanceResponse.Result);
					InitialCharacterDataInstance.AddObject(entityGuid, characterData.Result);

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
