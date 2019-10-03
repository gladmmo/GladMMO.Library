using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using Unitysync.Async;

namespace GladMMO
{
	//TODO: Refactor and combine with Creatures. They share a lot of code.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class RequestStaticGameObjectSpawnsEventListener : BaseSingleEventListenerInitializable<IServerStartingEventSubscribable>
	{
		private IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> EntityCreationRequester { get; }

		private IFactoryCreatable<NetworkEntityGuid, GameObjectInstanceModel> GameObjectGuidFactory { get; }

		private ILog Logger { get; }

		private IGameObjectDataService GameObjectDataService { get; }

		public RequestStaticGameObjectSpawnsEventListener(IServerStartingEventSubscribable subscriptionService,
			[NotNull] IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> entityCreationRequester,
			[NotNull] IFactoryCreatable<NetworkEntityGuid, GameObjectInstanceModel> gameObjectGuidFactory,
			[NotNull] IGameObjectDataService gameObjectDataService,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			EntityCreationRequester = entityCreationRequester ?? throw new ArgumentNullException(nameof(entityCreationRequester));
			GameObjectGuidFactory = gameObjectGuidFactory ?? throw new ArgumentNullException(nameof(gameObjectGuidFactory));
			GameObjectDataService = gameObjectDataService ?? throw new ArgumentNullException(nameof(gameObjectDataService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//TODO: We should simplify the ability to have async handlers.
			//TODO: Don't hardcode the test world id.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				await GameObjectDataService.LoadDataAsync();

				ProcessGameObjectEntries(GameObjectDataService.GameObjectInstanceMappable);
			});
		}

		private void ProcessGameObjectEntries([NotNull] IEnumerable<GameObjectInstanceModel> instanceEntries)
		{
			if (instanceEntries == null) throw new ArgumentNullException(nameof(instanceEntries));

			foreach (var entry in instanceEntries)
			{
				NetworkEntityGuid guid = GameObjectGuidFactory.Create(entry);

				if (Logger.IsInfoEnabled)
					Logger.Info($"Creating GameObject: {guid}");

				EntityCreationRequester.PublishEvent(this, new EntityCreationRequestedEventArgs(guid));
			}
		}
	}
}
