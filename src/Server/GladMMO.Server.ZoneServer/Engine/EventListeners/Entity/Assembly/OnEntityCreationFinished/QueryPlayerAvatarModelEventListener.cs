using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class QueryPlayerAvatarModelEventListener : PlayerCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataContainer { get; }

		private ICharacterService CharacterService { get; }

		public QueryPlayerAvatarModelEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer,
			[NotNull] ICharacterService characterService) 
			: base(subscriptionService)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			IEntityDataFieldContainer data = EntityDataContainer.RetrieveEntity(args.EntityGuid);

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				var characterAppearance = await CharacterService.GetCharacterAppearance(args.EntityGuid.EntityId);

				//Even if the character has logged off we're just setting an unused data container so it's ok.
				if(characterAppearance.isSuccessful)
					data.SetFieldValue(BaseObjectField.UNIT_FIELD_DISPLAYID, characterAppearance.Result.AvatarModelId);
			});
		}
	}
}
