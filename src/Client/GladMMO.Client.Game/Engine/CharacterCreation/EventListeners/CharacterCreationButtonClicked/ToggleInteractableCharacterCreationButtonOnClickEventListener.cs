using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class ToggleInteractableCharacterCreationButtonOnClickEventListener : ButtonClickedEventListener<ICharacterCreationButtonClickedEventSubscribable>
	{
		public ToggleInteractableCharacterCreationButtonOnClickEventListener(ICharacterCreationButtonClickedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			args.Button.IsInteractable = false;
		}
	}
}
