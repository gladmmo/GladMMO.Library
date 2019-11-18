using System;
using System.Collections.Generic;
using System.Text;
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

		public RequestPlayerActionBarsEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ICharacterService characterService,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IActionBarCollection actionBarCollection) 
			: base(subscriptionService)
		{
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			ActionBarCollection = actionBarCollection ?? throw new ArgumentNullException(nameof(actionBarCollection));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				CharacterActionBarInstanceModel[] barInstanceModels = await CharacterService.GetCharacterActionBarDataAsync(PlayerDetails.LocalPlayerGuid.EntityId);

				foreach (CharacterActionBarInstanceModel actionBarModel in barInstanceModels)
					ActionBarCollection.Add(actionBarModel);
			});
		}
	}
}
