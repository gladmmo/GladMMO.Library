using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(GameObjectAvatarPedestalEntityActor))]
	public sealed class AvatarPedestalInteractionMessageHandler : BaseEntityActorMessageHandler<BehaviourGameObjectState<AvatarPedestalInstanceModel>, InteractWithEntityActorMessage>
	{
		private ILog Logger { get; }

		private IZoneServerToGameServerClient ZoneServerDataClient { get; }

		public AvatarPedestalInteractionMessageHandler([NotNull] ILog logger, [NotNull] IZoneServerToGameServerClient zoneServerDataClient)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ZoneServerDataClient = zoneServerDataClient ?? throw new ArgumentNullException(nameof(zoneServerDataClient));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, BehaviourGameObjectState<AvatarPedestalInstanceModel> state, InteractWithEntityActorMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.EntityInteracting} Interacted with Avatar Pedestal: {state.EntityGuid}");

			//Only players should be able to interact with this.
			if(message.EntityInteracting.EntityType != EntityType.Player)
				return;

			//Initially, we should make the assumption that they CAN change their avatr with this pedestal
			//so we will send the change packet BEFORE checking.
			messageContext.Sender.Tell(new ChangeEntityActorDisplayModelMessage(state.BehaviourData.AvatarModelId));

			//Now we REALLY need to be sure.
			ZoneServerDataClient.UpdatePlayerAvatar(new ZoneServerAvatarPedestalInteractionCharacterRequest(message.EntityInteracting, state.BehaviourData.LinkedGameObjectId))
				.ContinueWith((task, o) =>
				{
					if (task.IsFaulted)
					{
						if (Logger.IsErrorEnabled)
							Logger.Error($"TODO: Log"); //lazy

						return;
					}

					//Now, we should verify the response. If the model id differ then it failed
					//or some race condition externally, the desired result is the player gets changed back.
					//To prevent exploits
					if(state.BehaviourData.AvatarModelId != task.Result.PersistedModelId)
						messageContext.Sender.Tell(new ChangeEntityActorDisplayModelMessage(task.Result.PersistedModelId));
				}, TaskContinuationOptions.ExecuteSynchronously);
		}
	}
}
