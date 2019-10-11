using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	/// <summary>
	/// The context of the entity actor message sent.
	/// </summary>
	public sealed class EntityActorMessageContext
	{
		/// <summary>
		/// Actor that sent the corresponding <see cref="EntityActorMessage"/>.
		/// </summary>
		public IActorRef Sender { get; }

		/// <summary>
		/// Actor that is receiving this message.
		/// </summary>
		public IActorRef Entity { get; }

		public EntityActorMessageContext(IActorRef sender, IActorRef entity)
		{
			Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			Entity = entity ?? throw new ArgumentNullException(nameof(entity));
		}
	}
}
