using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class CharacterSelectionEnterWorldEnabledOnCharacterSelectionEventListener : BaseSingleEventListenerInitializable<ICharacterSelectionButtonClickedEventSubscribable, CharacterButtonClickedEventArgs>
	{
		private IUIButton EnterWorldButton { get; }

		/// <inheritdoc />
		public CharacterSelectionEnterWorldEnabledOnCharacterSelectionEventListener(ICharacterSelectionButtonClickedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.EnterWorld)] [NotNull] IUIButton enterWorldButton) 
			: base(subscriptionService)
		{
			EnterWorldButton = enterWorldButton ?? throw new ArgumentNullException(nameof(enterWorldButton));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, CharacterButtonClickedEventArgs args)
		{
			//We just set enter world true.
			EnterWorldButton.IsInteractable = true;
		}
	}
}
