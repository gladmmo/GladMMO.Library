using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RequestPlayerActionBarsEventListener : OnLocalPlayerSpawnedEventListener
	{
		private ICharacterService CharacterService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IActionBarCollection ActionBarCollection { get; }

		private IActionBarButtonStateChangedEventPublisher ActionBarStateChangePublisher { get; }

		private ILog Logger { get; }

		public RequestPlayerActionBarsEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ICharacterService characterService,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IActionBarCollection actionBarCollection,
			[NotNull] IActionBarButtonStateChangedEventPublisher actionBarStateChangePublisher,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			ActionBarCollection = actionBarCollection ?? throw new ArgumentNullException(nameof(actionBarCollection));
			ActionBarStateChangePublisher = actionBarStateChangePublisher ?? throw new ArgumentNullException(nameof(actionBarStateChangePublisher));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					CharacterActionBarInstanceModel[] barInstanceModels = await CharacterService.GetCharacterActionBarDataAsync(PlayerDetails.LocalPlayerGuid.CurrentObjectGuid);

					foreach(CharacterActionBarInstanceModel actionBarModel in barInstanceModels)
					{
						ActionBarStateChangePublisher.PublishEvent(this, new ActionBarButtonStateChangedEventArgs(actionBarModel.BarIndex, actionBarModel.Type, actionBarModel.ActionId));
						ActionBarCollection.Add(actionBarModel);
					}
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to query ActionBars for Character: {PlayerDetails.LocalPlayerGuid.CurrentObjectGuid} Reason: {e.Message}");
					throw;
				}
			});
		}
	}
}
