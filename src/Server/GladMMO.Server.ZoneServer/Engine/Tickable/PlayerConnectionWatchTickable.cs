using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	//To put some demo/testing code into
	[GameInitializableOrdering(1)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerConnectionWatchTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IActorRef> EntityDataContainer { get; }

		private ILog Logger { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<IConnectionService> ConnectionServiceMappable { get; }

		/// <inheritdoc />
		public PlayerConnectionWatchTickable([NotNull] IReadonlyEntityGuidMappable<IActorRef> entityDataContainer,
			[NotNull] ILog logger,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] IReadonlyEntityGuidMappable<IConnectionService> connectionServiceMappable)
		{
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			ConnectionServiceMappable = connectionServiceMappable ?? throw new ArgumentNullException(nameof(connectionServiceMappable));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//We check here if an actor is dead when they should be a known entity.
			foreach (var a in EntityDataContainer.EnumerateWithGuid(KnownEntities, EntityType.Player))
			{
				//Kill the actor gracefully if the player connection is gone
				//yet we're still a known entity.
				if(!ConnectionServiceMappable[a.EntityGuid].isConnected)
					a.ComponentValue.Tell(PoisonPill.Instance);
			}
		}
	}
}
