using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(GameObjectWorldTeleporterEntityActor))]
	public sealed class WorldTeleporterSuccessfulInitializationHandler : BaseEntityActorMessageHandler<BehaviourGameObjectState<WorldTeleporterInstanceModel>, EntityActorInitializationSuccessMessage>
	{
		private ILog Logger { get; }

		public WorldTeleporterSuccessfulInitializationHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, BehaviourGameObjectState<WorldTeleporterInstanceModel> state, EntityActorInitializationSuccessMessage message)
		{
			//world teleporters can be interacted with.
			state.EntityData.AddBaseObjectFieldFlag(BaseObjectFieldFlags.UNIT_FLAG_INTERACTABLE);
		}
	}
}
