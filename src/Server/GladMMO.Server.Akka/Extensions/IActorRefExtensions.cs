using System;
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
	}
}
