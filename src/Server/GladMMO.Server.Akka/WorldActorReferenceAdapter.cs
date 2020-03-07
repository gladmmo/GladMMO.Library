using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Akka.Actor;
using Akka.Util;

namespace GladMMO
{
	public sealed class WorldActorReferenceAdapter : IWorldActorRef
	{
		private IActorRef Reference { get; }

		public WorldActorReferenceAdapter(IActorRef reference)
		{
			Reference = reference ?? throw new ArgumentNullException(nameof(reference));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Tell(object message, IActorRef sender)
		{
			Reference.Tell(message, sender);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(IActorRef other)
		{
			return Reference.Equals(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(IActorRef other)
		{
			return Reference.CompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ISurrogate ToSurrogate(ActorSystem system)
		{
			return Reference.ToSurrogate(system);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(object obj)
		{
			return Reference.CompareTo(obj);
		}

		public ActorPath Path => Reference.Path;
	}
}
