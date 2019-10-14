using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;
using Glader.Essentials;
using JetBrains.Annotations;

namespace GladMMO
{
	//To put some demo/testing code into
	[GameInitializableOrdering(1)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerActorDeathWatchTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IActorRef> EntityDataContainer { get; }

		private ILog Logger { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IEntityDestructionRequestedEventPublisher EntityDestructionPublisher { get; }

		/// <inheritdoc />
		public PlayerActorDeathWatchTickable([NotNull] IReadonlyEntityGuidMappable<IActorRef> entityDataContainer,
			[NotNull] ILog logger,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] IEntityDestructionRequestedEventPublisher entityDestructionPublisher)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			EntityDestructionPublisher = entityDestructionPublisher ?? throw new ArgumentNullException(nameof(entityDestructionPublisher));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//We check here if an actor is dead when they should be a known entity.
			foreach (var a in EntityDataContainer.EnumerateWithGuid(KnownEntities, EntityType.Player))
			{
				if (a.ComponentValue.IsNobody())
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Found Stopped Actor: {a.EntityGuid}");

					//Now the entity will begin to be cleaned up.
					EntityDestructionPublisher.PublishEvent(this, new EntityDeconstructionRequestedEventArgs(a.EntityGuid));
				}
			}
		}
	}
}
