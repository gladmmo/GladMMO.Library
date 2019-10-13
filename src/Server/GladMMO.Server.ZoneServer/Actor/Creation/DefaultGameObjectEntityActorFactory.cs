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

		public DefaultGameObjectEntityActorFactory([NotNull] IGameObjectDataService gameObjectDataContainer,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable)
		{
			GameObjectDataContainer = gameObjectDataContainer ?? throw new ArgumentNullException(nameof(gameObjectDataContainer));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		public EntityActorCreationResult Create([NotNull] NetworkEntityGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			GameObjectTemplateModel templateModel = GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(context);
			GameObjectInstanceModel instanceModel = GameObjectDataContainer.GameObjectInstanceMappable.RetrieveEntity(context);

			Type behaviourType = ComputeExpectedBehaviourType(templateModel.ObjectType);

			return new EntityActorCreationResult(behaviourType, new EntityActorStateInitializeMessage<DefaultGameObjectActorState>(new DefaultGameObjectActorState(EntityDataMappable.RetrieveEntity(context), context, instanceModel, templateModel)));
		}

		//TODO: Find a way to do this via reflection.
		private Type ComputeExpectedBehaviourType(GameObjectType templateModelObjectType)
		{
			if (!Enum.IsDefined(typeof(GameObjectType), templateModelObjectType)) throw new InvalidEnumArgumentException(nameof(templateModelObjectType), (int) templateModelObjectType, typeof(GameObjectType));

			switch (templateModelObjectType)
			{
				//TODO: Add reflection-based way to discover this stuff.
				case GameObjectType.WorldTeleporter:
					return typeof(DefaultWorldTeleporterInteractableGameObjectBehaviourComponent);
				case GameObjectType.AvatarPedestal:
					return typeof(DefaultAvatarPedestalInteractableGameObjectBehaviourComponent);
				default:
					throw new ArgumentOutOfRangeException(nameof(templateModelObjectType), templateModelObjectType, $"Cannot create behaviour for GameObjectType: {templateModelObjectType}");
			}
		}
	}
}
