using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class LoadSelectedCharacterAvatarEventListener : BaseSingleEventListenerInitializable<ICharacterSelectionButtonClickedEventSubscribable, CharacterButtonClickedEventArgs>
	{
		private IReadonlyEntityGuidMappable<CharacterAppearanceResponse> CharacterAppearanceMappable { get; }

		private IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> AvatarLoaderFactory { get; }

		public LoadSelectedCharacterAvatarEventListener(ICharacterSelectionButtonClickedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<CharacterAppearanceResponse> characterAppearanceMappable,
			[NotNull] IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> avatarLoaderFactory) 
			: base(subscriptionService)
		{
			CharacterAppearanceMappable = characterAppearanceMappable ?? throw new ArgumentNullException(nameof(characterAppearanceMappable));
			AvatarLoaderFactory = avatarLoaderFactory ?? throw new ArgumentNullException(nameof(avatarLoaderFactory));
		}

		protected override void OnEventFired(object source, CharacterButtonClickedEventArgs args)
		{
			CharacterAppearanceResponse data = CharacterAppearanceMappable.RetrieveEntity(args.CharacterGuid);

			CustomModelLoaderCancelable loader = AvatarLoaderFactory.Create(new CustomModelLoaderCreationContext(data.AvatarModelId, args.CharacterGuid));
			loader.StartLoading();
		}
	}
}
