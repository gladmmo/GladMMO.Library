using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class ReflectionBasedGameObjectEntityBehaviourFactory : IGameObjectEntityBehaviourFactory
	{
		private ILifetimeScope ReflectionContainer { get; }

		private IGameObjectDataService GameObjectDataContainer { get; }

		public ReflectionBasedGameObjectEntityBehaviourFactory([NotNull] ILifetimeScope reflectionContainer, [NotNull] IGameObjectDataService gameObjectDataContainer)
		{
			ReflectionContainer = reflectionContainer ?? throw new ArgumentNullException(nameof(reflectionContainer));
			GameObjectDataContainer = gameObjectDataContainer ?? throw new ArgumentNullException(nameof(gameObjectDataContainer));
		}

		public BaseGameObjectEntityBehaviourComponent Create([NotNull] NetworkEntityGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			GameObjectTemplateModel templateModel = GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(context);

			Type behaviourType = ComputeExpectedBehaviourType(templateModel.ObjectType);

			//TODO: Verify that the gameobject is known.
			using (var scope = ReflectionContainer.BeginLifetimeScope(cb =>
			{
				cb.RegisterInstance(GameObjectDataContainer.GameObjectInstanceMappable.RetrieveEntity(context))
					.AsSelf();

				cb.RegisterInstance(templateModel)
					.AsSelf();

				cb.RegisterInstance(context)
					.AsSelf();

				//Behaviour instance data and Type
				RegisterBehaviourInstanceData(templateModel.ObjectType, context, cb);
				cb.RegisterType(behaviourType);
			}))
			{
				return (BaseGameObjectEntityBehaviourComponent)scope.Resolve(behaviourType);
			}
		}

		//TODO: Find a way to do this via reflection.
		private void RegisterBehaviourInstanceData(GameObjectType templateModelObjectType, [NotNull] NetworkEntityGuid context, [NotNull] ContainerBuilder cb)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (cb == null) throw new ArgumentNullException(nameof(cb));
			if (!Enum.IsDefined(typeof(GameObjectType), templateModelObjectType)) throw new InvalidEnumArgumentException(nameof(templateModelObjectType), (int) templateModelObjectType, typeof(GameObjectType));

			object instanceData = null;

			switch (templateModelObjectType)
			{
				case GameObjectType.WorldTeleporter:
					instanceData = GameObjectDataContainer.GetBehaviourInstanceData<WorldTeleporterInstanceModel>(context);
					break;
				case GameObjectType.AvatarPedestal:
					instanceData = GameObjectDataContainer.GetBehaviourInstanceData<AvatarPedestalInstanceModel>(context);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(templateModelObjectType), templateModelObjectType, $"Cannot load instance data for {nameof(GameObjectType)}: {templateModelObjectType}");
			}

			cb.RegisterInstance(instanceData)
				.AsSelf();
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
