using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="GossipCompleteEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnGossipComplete"/>
	/// </summary>
	public interface IGossipCompleteEventSubscribable
	{
		event EventHandler<GossipCompleteEventArgs> OnGossipComplete;
	}

	/// <summary>
	/// Event arguments for the <see cref="IGossipCompleteEventSubscribable"/> interface.
	/// </summary>
	public sealed class GossipCompleteEventArgs : EventArgs
	{

	}
}
