using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.AI;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	public sealed class CreatureSetPathMovementMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, CreatureSetPathMovementMessage>
	{
		private ILog Logger { get; }

		private IMovementGeneratorFactory MovementGeneratorFactory { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		public CreatureSetPathMovementMessageHandler([NotNull] ILog logger,
			[NotNull] IMovementGeneratorFactory movementGeneratorFactory,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			MovementGeneratorFactory = movementGeneratorFactory ?? throw new ArgumentNullException(nameof(movementGeneratorFactory));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, CreatureSetPathMovementMessage message)
		{
			PathBasedMovementData data = new PathBasedMovementData(message.PathPoints, TimeService.CurrentLocalTime);
			IMovementGenerator<GameObject> generator = MovementGeneratorFactory.Create(new EntityAssociatedData<IMovementData>(state.EntityGuid, data));

			MovementDataMappable.ReplaceObject(state.EntityGuid, data);
			MovementGeneratorMappable.ReplaceObject(state.EntityGuid, generator);
		}
	}
}
