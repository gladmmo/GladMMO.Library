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
		private IGameObjectDataServiceClient GameObjectContentDataClient { get; }

		private IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> EntityCreationRequester { get; }

		private IFactoryCreatable<NetworkEntityGuid, GameObjectInstanceModel> GameObjectGuidFactory { get; }

		private ILog Logger { get; }

		private IEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable { get; }

		private IEntityGuidMappable<GameObjectInstanceModel> GameObjectInstanceMappable { get; }

		private WorldConfiguration WorldConfiguration { get; }

		public RequestStaticGameObjectSpawnsEventListener(IServerStartingEventSubscribable subscriptionService,
			[NotNull] IGameObjectDataServiceClient gameObjectContentDataClient,
			[NotNull] IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> entityCreationRequester,
			[NotNull] IFactoryCreatable<NetworkEntityGuid, GameObjectInstanceModel> gameObjectGuidFactory,
			[NotNull] ILog logger, [NotNull] IEntityGuidMappable<GameObjectTemplateModel> gameObjectTemplateMappable,
			[NotNull] IEntityGuidMappable<GameObjectInstanceModel> gameObjectInstanceMappable, WorldConfiguration worldConfiguration)
			: base(subscriptionService)
		{
			GameObjectContentDataClient = gameObjectContentDataClient ?? throw new ArgumentNullException(nameof(gameObjectContentDataClient));
			EntityCreationRequester = entityCreationRequester ?? throw new ArgumentNullException(nameof(entityCreationRequester));
			GameObjectGuidFactory = gameObjectGuidFactory ?? throw new ArgumentNullException(nameof(gameObjectGuidFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GameObjectTemplateMappable = gameObjectTemplateMappable ?? throw new ArgumentNullException(nameof(gameObjectTemplateMappable));
			GameObjectInstanceMappable = gameObjectInstanceMappable ?? throw new ArgumentNullException(nameof(gameObjectInstanceMappable));
			WorldConfiguration = worldConfiguration;
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//TODO: We should simplify the ability to have async handlers.
			//TODO: Don't hardcode the test world id.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<ObjectEntryCollectionModel<GameObjectInstanceModel>, ContentEntryCollectionResponseCode > entriesByWorld = await GameObjectContentDataClient.GetGameObjectEntriesByWorld(WorldConfiguration.WorldId);
				ResponseModel<ObjectEntryCollectionModel<GameObjectTemplateModel>, ContentEntryCollectionResponseCode> templatesByWorld = await GameObjectContentDataClient.GetGameObjectTemplatesByWorld(WorldConfiguration.WorldId);

				if (entriesByWorld.isSuccessful && templatesByWorld.isSuccessful)
				{
					AddGameObjectTemplates(templatesByWorld.Result.Entries);
					AddGameObjectInstances(entriesByWorld.Result.Entries);

					ProcessGameObjectEntries(entriesByWorld.Result.Entries);
				}
				else
				{
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Didn't properly query GameObject Data. This may not actually be an error. Instance Reason: {entriesByWorld.ResultCode} Template Reason: {templatesByWorld.ResultCode}");
				}
			});
		}

		private void AddGameObjectInstances([NotNull] IReadOnlyCollection<GameObjectInstanceModel> resultEntries)
		{
			if (resultEntries == null) throw new ArgumentNullException(nameof(resultEntries));

			foreach (var instance in resultEntries)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing GameObject Instance: {instance.Guid}");

				GameObjectInstanceMappable.Add(instance.Guid, instance);
			}
		}

		private void AddGameObjectTemplates([NotNull] IReadOnlyCollection<GameObjectTemplateModel> resultTemplates)
		{
			if (resultTemplates == null) throw new ArgumentNullException(nameof(resultTemplates));

			foreach (var template in resultTemplates)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing GameObject Template: {template.TemplateId} Name: {template.GameObjectName}");

				GameObjectTemplateMappable.Add(NetworkEntityGuid.Empty, template);
			}
		}

		private void ProcessGameObjectEntries([NotNull] IReadOnlyCollection<GameObjectInstanceModel> instanceEntries)
		{
			if (instanceEntries == null) throw new ArgumentNullException(nameof(instanceEntries));

			foreach (var entry in instanceEntries)
			{
				NetworkEntityGuid guid = GameObjectGuidFactory.Create(entry);
				EntityCreationRequester.PublishEvent(this, new EntityCreationRequestedEventArgs(guid));
			}
		}
	}
}
