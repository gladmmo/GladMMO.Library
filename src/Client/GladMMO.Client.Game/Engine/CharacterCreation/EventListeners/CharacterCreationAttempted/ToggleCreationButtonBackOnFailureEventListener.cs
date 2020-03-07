using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class ToggleCreationButtonBackOnFailureEventListener : BaseSingleEventListenerInitializable<ICharacterCreationAttemptedEventSubscribable, CharacterCreationAttemptedEventArgs>
	{
		private IUIButton CreationButton { get; }

		public ToggleCreationButtonBackOnFailureEventListener(ICharacterCreationAttemptedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.CharacterCreateButton)] [NotNull]
			IUIButton creationButton)
			: base(subscriptionService)
		{
			CreationButton = creationButton ?? throw new ArgumentNullException(nameof(creationButton));
		}

		protected override void OnEventFired(object source, CharacterCreationAttemptedEventArgs args)
		{
			//Only on failure.
			if (args.isSuccessful)
				return;

			CreationButton.IsInteractable = true;
		}
	}
}
