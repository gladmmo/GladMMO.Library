using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladMMO.SDK;

namespace GladMMO
{
	public sealed class RemoteGameObjectDataService : IGameObjectDataService
	{
		//We use these backing fields to hide the mutability.
		private IEntityGuidMappable<GameObjectTemplateModel> _GameObjectTemplateMappable { get; }

		private IEntityGuidMappable<GameObjectInstanceModel> _GameObjectInstanceMappable { get; }

		public IReadonlyEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable => _GameObjectTemplateMappable;

		public IReadonlyEntityGuidMappable<GameObjectInstanceModel> GameObjectInstanceMappable => _GameObjectInstanceMappable;

		private IGameObjectDataServiceClient DataServiceClient { get; }

		private IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel> TeleporterDataServiceClient { get; }

		private IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel> AvatarPedestalDataServiceClient { get; }

		private WorldConfiguration WorldConfiguration { get; }

		private ILog Logger { get; }

		private Dictionary<Type, IEntityGuidMappable<object>> BehaviourInstanceDataMappable { get; }

		public RemoteGameObjectDataService(
			[NotNull] IEntityGuidMappable<GameObjectTemplateModel> gameObjectTemplateMappable,
			[NotNull] IEntityGuidMappable<GameObjectInstanceModel> gameObjectInstanceMappable,
			[NotNull] IGameObjectDataServiceClient dataServiceClient,
			[NotNull] WorldConfiguration worldConfiguration,
			[NotNull] ILog logger,
			[NotNull] IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel> teleporterDataServiceClient,
			[NotNull] IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel> avatarPedestalDataServiceClient)
		{
			_GameObjectTemplateMappable = gameObjectTemplateMappable ?? throw new ArgumentNullException(nameof(gameObjectTemplateMappable));
			_GameObjectInstanceMappable = gameObjectInstanceMappable ?? throw new ArgumentNullException(nameof(gameObjectInstanceMappable));

			DataServiceClient = dataServiceClient ?? throw new ArgumentNullException(nameof(dataServiceClient));
			WorldConfiguration = worldConfiguration ?? throw new ArgumentNullException(nameof(worldConfiguration));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			TeleporterDataServiceClient = teleporterDataServiceClient ?? throw new ArgumentNullException(nameof(teleporterDataServiceClient));
			AvatarPedestalDataServiceClient = avatarPedestalDataServiceClient;

			BehaviourInstanceDataMappable = new Dictionary<Type, IEntityGuidMappable<object>>(5);
		}

		public TBehaviourType GetBehaviourInstanceData<TBehaviourType>([NotNull] NetworkEntityGuid entityGuid)
			where TBehaviourType : IGameObjectLinkable
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));

			return (TBehaviourType)BehaviourInstanceDataMappable[typeof(TBehaviourType)];
		}

		public async Task LoadDataAsync()
		{
			//TODO: Handle failure cases.
			ResponseModel<ObjectEntryCollectionModel<GameObjectInstanceModel>, ContentEntryCollectionResponseCode> entriesByWorld = await DataServiceClient.GetGameObjectEntriesByWorld(WorldConfiguration.WorldId);
			ResponseModel<ObjectEntryCollectionModel<GameObjectTemplateModel>, ContentEntryCollectionResponseCode> templatesByWorld = await DataServiceClient.GetGameObjectTemplatesByWorld(WorldConfiguration.WorldId);

			ResponseModel<ObjectEntryCollectionModel<WorldTeleporterInstanceModel>, ContentEntryCollectionResponseCode> teleportersByWorld = await TeleporterDataServiceClient.GetBehaviourEntriesByWorld(WorldConfiguration.WorldId);
			ResponseModel<ObjectEntryCollectionModel<AvatarPedestalInstanceModel>, ContentEntryCollectionResponseCode> avatarPedestalsByWorld = await AvatarPedestalDataServiceClient.GetBehaviourEntriesByWorld(WorldConfiguration.WorldId);

			if(entriesByWorld.isSuccessful && templatesByWorld.isSuccessful)
			{
				AddGameObjectTemplates(templatesByWorld.Result.Entries);
				AddGameObjectInstances(entriesByWorld.Result.Entries);

				//It's possible there were none.
				if (teleportersByWorld.isSuccessful)
					AddGameObjectInstances(teleportersByWorld.Result.Entries);

				//It's possible there were none.
				if(avatarPedestalsByWorld.isSuccessful)
					AddGameObjectInstances(avatarPedestalsByWorld.Result.Entries);
			}
			else
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Didn't properly query GameObject Data. This may not actually be an error. Instance Reason: {entriesByWorld.ResultCode} Template Reason: {templatesByWorld.ResultCode}");
			}
		}

		private void AddGameObjectInstances<TInstanceModelType>([NotNull] IReadOnlyCollection<TInstanceModelType> resultEntries)
			where TInstanceModelType : IGameObjectLinkable
		{
			if(resultEntries == null) throw new ArgumentNullException(nameof(resultEntries));

			foreach(var instance in resultEntries)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing {typeof(TInstanceModelType).Name} Data Instance: {instance.LinkedGameObjectId}");

				BehaviourInstanceDataMappable.Add(typeof(TInstanceModelType), new EntityGuidDictionary<object>());

				NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
					.WithEntryId(instance.LinkedGameObjectId)
					.WithType(EntityType.GameObject)
					.Build();

				BehaviourInstanceDataMappable[typeof(TInstanceModelType)].Add(guid, instance);
			}
		}

		private void AddGameObjectInstances([NotNull] IReadOnlyCollection<GameObjectInstanceModel> resultEntries)
		{
			if(resultEntries == null) throw new ArgumentNullException(nameof(resultEntries));

			foreach(var instance in resultEntries)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing GameObject Instance: {instance.Guid}");

				_GameObjectInstanceMappable.Add(instance.Guid, instance);
			}
		}

		private void AddGameObjectTemplates([NotNull] IReadOnlyCollection<GameObjectTemplateModel> resultTemplates)
		{
			if(resultTemplates == null) throw new ArgumentNullException(nameof(resultTemplates));

			foreach(var template in resultTemplates)
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Processing GameObject Template: {template.TemplateId} Name: {template.GameObjectName}");

				_GameObjectTemplateMappable.Add(NetworkEntityGuid.Empty, template);
			}
		}
	}
}
