using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class PlayerSpellCastFailedMessageHandler : BaseEntityActorMessageHandler<DefaultPlayerEntityActorState, SpellCastFailedMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultPlayerEntityActorState state, SpellCastFailedMessage message)
		{
			state.SendService.SendMessage(new SpellCastResponsePayload(message.Result, message.SpellId));
		}
	}
}
