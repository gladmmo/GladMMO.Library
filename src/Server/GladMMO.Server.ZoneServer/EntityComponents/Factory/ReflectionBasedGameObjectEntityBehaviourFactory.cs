using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class ReflectionBasedGameObjectEntityBehaviourFactory : IGameObjectEntityBehaviourFactory
	{
		private IContainer ReflectionContainer { get; }

		private IGameObjectDataService GameObjectDataContainer { get; }

		public ReflectionBasedGameObjectEntityBehaviourFactory([NotNull] IContainer reflectionContainer, [NotNull] IGameObjectDataService gameObjectDataContainer)
		{
			ReflectionContainer = reflectionContainer ?? throw new ArgumentNullException(nameof(reflectionContainer));
			GameObjectDataContainer = gameObjectDataContainer ?? throw new ArgumentNullException(nameof(gameObjectDataContainer));
		}

		public BaseGameObjectEntityBehaviourComponent Create([NotNull] NetworkEntityGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			//TODO: Verify that the gameobject is known.
			using (var scope = ReflectionContainer.BeginLifetimeScope(cb =>
			{
				cb.RegisterInstance(GameObjectDataContainer.GameObjectInstanceMappable.RetrieveEntity(context))
					.AsSelf();

				cb.RegisterInstance(GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(context))
					.AsSelf();

				//TODO: We're hackily supporting world teleporters here by default
				if(GameObjectDataContainer.GameObjectTemplateMappable.RetrieveEntity(context).ObjectType != GameObjectType.WorldTeleporter)
					throw new NotImplementedException($"TODO: Handle other game object types.");

				cb.RegisterInstance(GameObjectDataContainer.WorldTeleporterInstanceMappable.RetrieveEntity(context))
					.AsSelf();
			}))
			{
				return (DefaultWorldTeleporterInteractableGameObjectBehaviourComponent)scope.Resolve(typeof(DefaultWorldTeleporterInteractableGameObjectBehaviourComponent));
			}
		}
	}
}
