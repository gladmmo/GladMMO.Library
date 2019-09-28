using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using Unitysync.Async;

namespace GladMMO
{
	//TODO: Refactor and combine with GameObjects. They share a lot of code.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class RequestStaticCreatureSpawnsEventListener : BaseSingleEventListenerInitializable<IServerStartingEventSubscribable>
	{
		private ICreatureDataServiceClient CreatureContentDataClient { get; }

		private IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> EntityCreationRequester { get; }

		private IFactoryCreatable<NetworkEntityGuid, CreatureInstanceModel> CreatureGuidFactory { get; }

		private ILog Logger { get; }

		private IEntityGuidMappable<CreatureTemplateModel> CreatureTemplateMappable { get; }

		private IEntityGuidMappable<CreatureInstanceModel> CreatureInstanceMappable { get; }

		private WorldConfiguration WorldConfiguration { get; }

		public RequestStaticCreatureSpawnsEventListener(IServerStartingEventSubscribable subscriptionService,
			[NotNull] ICreatureDataServiceClient creatureContentDataClient,
			[NotNull] IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs> entityCreationRequester,
			[NotNull] IFactoryCreatable<NetworkEntityGuid, CreatureInstanceModel> creatureGuidFactory,
			[NotNull] ILog logger, [NotNull] IEntityGuidMappable<CreatureTemplateModel> creatureTemplateMappable,
			[NotNull] IEntityGuidMappable<CreatureInstanceModel> creatureInstanceMappable, WorldConfiguration worldConfiguration)
			: base(subscriptionService)
		{
			CreatureContentDataClient = creatureContentDataClient ?? throw new ArgumentNullException(nameof(creatureContentDataClient));
			EntityCreationRequester = entityCreationRequester ?? throw new ArgumentNullException(nameof(entityCreationRequester));
			CreatureGuidFactory = creatureGuidFactory ?? throw new ArgumentNullException(nameof(creatureGuidFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CreatureTemplateMappable = creatureTemplateMappable ?? throw new ArgumentNullException(nameof(creatureTemplateMappable));
			CreatureInstanceMappable = creatureInstanceMappable ?? throw new ArgumentNullException(nameof(creatureInstanceMappable));
			WorldConfiguration = worldConfiguration;
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//TODO: We should simplify the ability to have async handlers.
			//TODO: Don't hardcode the test world id.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<ObjectEntryCollectionModel<CreatureInstanceModel>, ContentEntryCollectionResponseCode > entriesByWorld = await CreatureContentDataClient.GetCreatureEntriesByWorld(WorldConfiguration.WorldId);
				ResponseModel<ObjectEntryCollectionModel<CreatureTemplateModel>, ContentEntryCollectionResponseCode> templatesByWorld = await CreatureContentDataClient.GetCreatureTemplatesByWorld(WorldConfiguration.WorldId);

				if (entriesByWorld.isSuccessful && templatesByWorld.isSuccessful)
				{
					AddCreatureTemplates(templatesByWorld.Result.Entries);
					AddCreatureInstances(entriesByWorld.Result.Entries);

					ProcessCreatureEntries(entriesByWorld.Result.Entries);
				}
				else
				{
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Didn't properly query Creature Data. This may not actually be an error. Instance Reason: {entriesByWorld.ResultCode} Template Reason: {templatesByWorld.ResultCode}");
				}
			});
		}

		private void AddCreatureInstances([NotNull] IReadOnlyCollection<CreatureInstanceModel> resultEntries)
		{
			if (resultEntries == null) throw new ArgumentNullException(nameof(resultEntries));

			foreach (var instance in resultEntries)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing Creature Instance: {instance.Guid}");

				CreatureInstanceMappable.Add(instance.Guid, instance);
			}
		}

		private void AddCreatureTemplates([NotNull] IReadOnlyCollection<CreatureTemplateModel> resultTemplates)
		{
			if (resultTemplates == null) throw new ArgumentNullException(nameof(resultTemplates));

			foreach (var template in resultTemplates)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing Creature Template: {template.TemplateId} Name: {template.CreatureName}");

				CreatureTemplateMappable.Add(NetworkEntityGuid.Empty, template);
			}
		}

		private void ProcessCreatureEntries([NotNull] IReadOnlyCollection<CreatureInstanceModel> instanceEntries)
		{
			if (instanceEntries == null) throw new ArgumentNullException(nameof(instanceEntries));

			foreach (var entry in instanceEntries)
			{
				NetworkEntityGuid guid = CreatureGuidFactory.Create(entry);
				EntityCreationRequester.PublishEvent(this, new EntityCreationRequestedEventArgs(guid));
			}
		}
	}
}
