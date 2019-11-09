using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class DefaultGameObjectEntityActorFactory : IGameObjectEntityActorFactory
	{
		private IGameObjectDataService GameObjectDataContainer { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IReadonlyEntityGuidMappable<InterestCollection> InterestMappable { get; }

		public DefaultGameObjectEntityActorFactory([NotNull] IGameObjectDataService gameObjectDataContainer,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> interestMappable)
		{
			GameObjectDataContainer = gameObjectDataContainer ?? throw new ArgumentNullException(nameof(gameObjectDataContainer));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			InterestMappable = interestMappable ?? throw new ArgumentNullException(nameof(interestMappable));
		}

		public EntityActorCreationResult Create([NotNull] NetworkEntityGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			GameObjectTemplateModel templateModel = GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(context);

			Type behaviourType = ComputeExpectedActorType(templateModel.ObjectType);

			return new EntityActorCreationResult(behaviourType, CreateInitialState(context, templateModel.ObjectType));
		}

		private IEntityActorStateInitializeMessage<DefaultGameObjectActorState> CreateInitialState([NotNull] NetworkEntityGuid entityGuid, GameObjectType objectType)
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));
			if (!Enum.IsDefined(typeof(GameObjectType), objectType)) throw new InvalidEnumArgumentException(nameof(objectType), (int) objectType, typeof(GameObjectType));

			switch(objectType)
			{
				case GameObjectType.Visual:
					return CreateDefaultInitializationState(entityGuid);
				//TODO: Add reflection-based way to discover this stuff.
				case GameObjectType.WorldTeleporter:
					return CreateState<WorldTeleporterInstanceModel>(entityGuid);
				case GameObjectType.AvatarPedestal:
					return CreateState<AvatarPedestalInstanceModel>(entityGuid);
				default:
					throw new ArgumentOutOfRangeException(nameof(objectType), objectType, $"Cannot create Actor for GameObjectType: {objectType}");
			}
		}

		private IEntityActorStateInitializeMessage<DefaultGameObjectActorState> CreateDefaultInitializationState(NetworkEntityGuid entityGuid)
		{
			GameObjectTemplateModel templateModel = GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(entityGuid);
			GameObjectInstanceModel instanceModel = GameObjectDataContainer.GameObjectInstanceMappable.RetrieveEntity(entityGuid);
			return new EntityActorStateInitializeMessage<DefaultGameObjectActorState>(new DefaultGameObjectActorState(EntityDataMappable.RetrieveEntity(entityGuid), entityGuid, instanceModel, templateModel, InterestMappable.RetrieveEntity(entityGuid)));
		}

		private EntityActorStateInitializeMessage<BehaviourGameObjectState<TBehaviourType>> CreateState<TBehaviourType>(NetworkEntityGuid entityGuid) 
			where TBehaviourType : class, IGameObjectLinkable
		{
			GameObjectTemplateModel templateModel = GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(entityGuid);
			GameObjectInstanceModel instanceModel = GameObjectDataContainer.GameObjectInstanceMappable.RetrieveEntity(entityGuid);

			return new EntityActorStateInitializeMessage<BehaviourGameObjectState<TBehaviourType>>(new BehaviourGameObjectState<TBehaviourType>(EntityDataMappable.RetrieveEntity(entityGuid), entityGuid, instanceModel, templateModel, GameObjectDataContainer.GetBehaviourInstanceData<TBehaviourType>(entityGuid), InterestMappable.RetrieveEntity(entityGuid)));
		}

		private Type ComputeExpectedActorType(GameObjectType templateModelObjectType)
		{
			if (!Enum.IsDefined(typeof(GameObjectType), templateModelObjectType)) throw new InvalidEnumArgumentException(nameof(templateModelObjectType), (int) templateModelObjectType, typeof(GameObjectType));

			switch (templateModelObjectType)
			{
				//TODO: Add reflection-based way to discover this stuff.
				case GameObjectType.Visual:
					return typeof(DefaultGameObjectEntityActor);
				case GameObjectType.WorldTeleporter:
					return typeof(GameObjectWorldTeleporterEntityActor);
				case GameObjectType.AvatarPedestal:
					return typeof(GameObjectAvatarPedestalEntityActor);
				default:
					throw new ArgumentOutOfRangeException(nameof(templateModelObjectType), templateModelObjectType, $"Cannot create Actor for GameObjectType: {templateModelObjectType}");
			}
		}
	}
}
