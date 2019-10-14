using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IGeneralErrorEncounteredEventPublisher
	{

	}

	//I need a better way to specify "all scenes" lol.
	[AdditionalRegisterationAs(typeof(IEventPublisher<IGeneralErrorEncounteredEventListener, GeneralErrorEncounteredEventArgs>))]
	[AdditionalRegisterationAs(typeof(IGeneralErrorEncounteredEventPublisher))]
	[AdditionalRegisterationAs(typeof(IGeneralErrorEncounteredEventListener))]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GeneralErrorEncounteredEventPublisher : IEventPublisher<IGeneralErrorEncounteredEventListener, GeneralErrorEncounteredEventArgs>, IGeneralErrorEncounteredEventListener
	{
		public event EventHandler<GeneralErrorEncounteredEventArgs> OnErrorEncountered;

		public void PublishEvent(object sender, GeneralErrorEncounteredEventArgs eventArgs)
		{
			OnErrorEncountered?.Invoke(sender, eventArgs);
		}
	}
}
