using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class LoadCharacterCreationSceneEventListener : ButtonClickedEventListener<ICreateNewCharacterButtonClickedEventSubscribable>
	{
		public LoadCharacterCreationSceneEventListener(ICreateNewCharacterButtonClickedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			//When the button is clicked we should disable interaction
			//then we can load the scene async.
			//We should probably disabled the enter-world button too, but we don't have exclusive control over it.
			args.Button.IsInteractable = false;

			//Don't load it async, because then they may click on Enter World or some wacky
			//stuff and who knows what will happen.
			SceneManager.LoadScene(GladMMOClientConstants.CHARACTER_CREATION_SCENE_NAME);
		}
	}
}
