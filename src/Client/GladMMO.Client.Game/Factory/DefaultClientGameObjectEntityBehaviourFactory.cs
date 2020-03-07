using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultClientGameObjectEntityBehaviourFactory : IClientGameObjectEntityBehaviourFactory
	{
		private ILifetimeScope ReflectionContainer { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IReadonlyEntityGuidMappable<GameObject> WorldObjectMappable { get; }

		public DefaultClientGameObjectEntityBehaviourFactory([NotNull] ILifetimeScope reflectionContainer,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> worldObjectMappable)
		{
			ReflectionContainer = reflectionContainer ?? throw new ArgumentNullException(nameof(reflectionContainer));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			WorldObjectMappable = worldObjectMappable ?? throw new ArgumentNullException(nameof(worldObjectMappable));
		}

		public BaseEntityBehaviourComponent Create([NotNull] ObjectGuid context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			IEntityDataFieldContainer dataFieldContainer = EntityDataMappable.RetrieveEntity(context);
			GameObject worldObject = WorldObjectMappable.RetrieveEntity(context);

			Type behaviourType = ComputeExpectedBehaviourType(dataFieldContainer.GetEnumFieldValue<GameObjectType>(GameObjectField.GAMEOBJECT_TYPE_ID));

			using(var scope = ReflectionContainer.BeginLifetimeScope(cb =>
			{
				cb.RegisterInstance(context)
					.AsSelf();

				cb.RegisterInstance(worldObject)
					.As<GameObject>();

				cb.RegisterInstance(dataFieldContainer)
					.As<IEntityDataFieldContainer>()
					.As<IReadonlyEntityDataFieldContainer>()
					.SingleInstance();

				cb.RegisterType(behaviourType);
			}))
			{
				return (BaseEntityBehaviourComponent)scope.Resolve(behaviourType);
			}
		}

		//TODO: Find a way to do this via reflection.
		private Type ComputeExpectedBehaviourType(GameObjectType templateModelObjectType)
		{
			if(!Enum.IsDefined(typeof(GameObjectType), templateModelObjectType)) throw new InvalidEnumArgumentException(nameof(templateModelObjectType), (int)templateModelObjectType, typeof(GameObjectType));

			switch(templateModelObjectType)
			{
				//TODO: Add reflection-based way to discover this stuff.
				case GameObjectType.WorldTeleporter:
					return typeof(StubWorldTeleporterGameObjectBehaviour);
				case GameObjectType.AvatarPedestal:
					return typeof(DefaultAvatarPedestalGameObjectBehaviour);
				default:
					throw new ArgumentOutOfRangeException(nameof(templateModelObjectType), templateModelObjectType, $"Cannot create behaviour for GameObjectType: {templateModelObjectType}");
			}
		}
	}
}
