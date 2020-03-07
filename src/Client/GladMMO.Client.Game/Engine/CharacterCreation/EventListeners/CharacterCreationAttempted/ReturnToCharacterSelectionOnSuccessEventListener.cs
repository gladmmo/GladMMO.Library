using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class ReturnToCharacterSelectionOnSuccessEventListener : BaseSingleEventListenerInitializable<ICharacterCreationAttemptedEventSubscribable, CharacterCreationAttemptedEventArgs>
	{
		public IUIButton BackButton { get; }

		public ReturnToCharacterSelectionOnSuccessEventListener(ICharacterCreationAttemptedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.BackButton)] [NotNull] IUIButton backButton) 
			: base(subscriptionService)
		{
			BackButton = backButton ?? throw new ArgumentNullException(nameof(backButton));
		}

		protected override void OnEventFired(object source, CharacterCreationAttemptedEventArgs args)
		{
			if (!args.isSuccessful)
				return;

			//Just simulate a back button press.
			BackButton.SimulateClick(true);
		}
	}
}
