using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="GossipMenuCreateEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnGossipMenuCreate"/>
	/// </summary>
	public interface IGossipMenuCreateEventSubscribable
	{
		event EventHandler<GossipMenuCreateEventArgs> OnGossipMenuCreate;
	}

	/// <summary>
	/// Event arguments for the <see cref="IGossipMenuCreateEventSubscribable"/> interface.
	/// </summary>
	public sealed class GossipMenuCreateEventArgs : EventArgs
	{
		public ObjectGuid GossipSource { get; }

		public IReadOnlyList<GossipMenuItem> GossipMenuEntries { get; }

		public IReadOnlyList<QuestGossipEntry> QuestMenuEntries { get; }

		public GossipMenuCreateEventArgs([NotNull] ObjectGuid gossipSource,
			[NotNull] IReadOnlyList<GossipMenuItem> gossipMenuEntries,
			[NotNull] IReadOnlyList<QuestGossipEntry> questMenuEntries)
		{
			GossipSource = gossipSource ?? throw new ArgumentNullException(nameof(gossipSource));
			GossipMenuEntries = gossipMenuEntries ?? throw new ArgumentNullException(nameof(gossipMenuEntries));
			QuestMenuEntries = questMenuEntries ?? throw new ArgumentNullException(nameof(questMenuEntries));
		}
	}
}