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
	public sealed class CreatureTestPathingInteractionMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, InteractWithEntityActorMessage>
	{
		private ILog Logger { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		public CreatureTestPathingInteractionMessageHandler([NotNull] ILog logger,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, InteractWithEntityActorMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.EntityInteracting} Interacted with Creature: {state.EntityGuid}");

			messageContext.Sender.Tell(new AddPlayerExperienceActorMessage(50));

			//Let's create the path data.
			IMovementGenerator<GameObject> movementGenerator = MovementGeneratorMappable.RetrieveEntity(state.EntityGuid);
			var playerMovementGenerator = MovementGeneratorMappable.RetrieveEntity(message.EntityInteracting);

			//WARNING: NEVER DO THIS, NOT SAFE TO ACCESS PLAYER DATA.
			Vector3 creatureCurrentPosition = movementGenerator.CurrentPosition;
			Vector3 playerCurrentPosition = playerMovementGenerator.CurrentPosition;

			UnityAsyncHelper.UnityMainThreadContext.Post(o =>
			{
				var path = new NavMeshPath();
				NavMesh.CalculatePath(creatureCurrentPosition, playerCurrentPosition, NavMesh.AllAreas, path);

				if(path.status != NavMeshPathStatus.PathComplete)
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Produced invalid PathResult: {path.status} for Entity: {state.EntityGuid} Start: {creatureCurrentPosition} End: {playerCurrentPosition}");

				//Don't broadcast if not invalid
				if(path.status == NavMeshPathStatus.PathComplete)
					messageContext.Entity.Tell(new CreatureSetPathMovementMessage(creatureCurrentPosition, path.corners));
			}, null);
		}
	}
}
