using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(GameObjectAvatarPedestalEntityActor))]
	public sealed class AvatarPedestalSuccessfulInitializationHandler : BaseEntityActorMessageHandler<BehaviourGameObjectState<AvatarPedestalInstanceModel>, EntityActorInitializationSuccessMessage>
	{
		private ILog Logger { get; }

		public AvatarPedestalSuccessfulInitializationHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, BehaviourGameObjectState<AvatarPedestalInstanceModel> state, EntityActorInitializationSuccessMessage message)
		{
			//We need to initialize our replicateable state here.

			//TODO: Better handling of enum names for specialized gameobjects
			//Basically this is how clients see the avatar id.
			state.EntityData.SetFieldValue(GameObjectField.RESERVED_DATA_1, state.BehaviourData.AvatarModelId);
		}
	}
}
