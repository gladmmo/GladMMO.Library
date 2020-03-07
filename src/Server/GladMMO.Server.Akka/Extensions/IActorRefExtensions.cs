using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public static class IActorRefExtensions
	{
		/// <summary>
		/// See Akka.NET's <see cref="IActorRef"/>.Tell.
		/// </summary>
		/// <param name="actorReference"></param>
		/// <param name="message"></param>
		public static void Tell(this IActorRef actorReference, EntityActorMessage message)
		{
			if (actorReference == null) throw new ArgumentNullException(nameof(actorReference));
			if (message == null) throw new ArgumentNullException(nameof(message));

			actorReference.Tell((object) message);
		}

		/// <summary>
		/// See Akka.NET's <see cref="IActorRef"/>.Tell.
		/// </summary>
		/// <param name="actorReference"></param>
		/// <param name="message"></param>
		/// <param name="sender"></param>
		public static void Tell(this IActorRef actorReference, EntityActorMessage message, IActorRef sender)
		{
			if(actorReference == null) throw new ArgumentNullException(nameof(actorReference));
			if(message == null) throw new ArgumentNullException(nameof(message));
			if (sender == null) throw new ArgumentNullException(nameof(sender));

			actorReference.Tell((object)message, sender);
		}

		/// <summary>
		/// See Akka.NET's <see cref="IActorRef"/>.Tell.
		/// Sends self with <see cref="actorReference"/> as the sender.
		/// </summary>
		/// <param name="actorReference"></param>
		/// <param name="message"></param>
		public static void TellSelf(this IActorRef actorReference, EntityActorMessage message)
		{
			if(actorReference == null) throw new ArgumentNullException(nameof(actorReference));
			if(message == null) throw new ArgumentNullException(nameof(message));

			actorReference.Tell((object)message, actorReference);
		}
	}
}
