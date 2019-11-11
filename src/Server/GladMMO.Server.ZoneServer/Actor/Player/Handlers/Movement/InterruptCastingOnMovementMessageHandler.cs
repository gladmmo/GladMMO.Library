using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class InterruptCastingOnMovementMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, PlayerMovementStateChangedMessage>
	{
		private IReadonlyEntityGuidMappable<PendingSpellCastData> PendingSpellCastMappable { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		public InterruptCastingOnMovementMessageHandler([NotNull] IReadonlyEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable,
			[NotNull] IReadonlyNetworkTimeService timeService)
		{
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, PlayerMovementStateChangedMessage message)
		{
			//If we started moving then we should check the current spell cast.
			if (message.isMoving)
			{
				if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
				{
					PendingSpellCastData castData = PendingSpellCastMappable.RetrieveEntity(state.EntityGuid);

					if (castData.isInstantCast || castData.isCastCanceled || castData.IsSpellcastFinished(TimeService.CurrentLocalTime))
						return;

					//Ok, so we're casting and moving. Let's cancel the cast.
					messageContext.Entity.TellSelf(new CancelSpellCastMessage());
				}
			}
		}
	}
}
