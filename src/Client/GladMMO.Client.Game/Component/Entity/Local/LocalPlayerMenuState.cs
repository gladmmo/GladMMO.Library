using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class LocalPlayerMenuState
	{
		public IReadOnlyList<GossipMenuItem> GossipOptions { get; private set; } = Array.Empty<GossipMenuItem>();

		public IReadOnlyList<QuestGossipEntry> QuestOptions { get; private set; } = Array.Empty<QuestGossipEntry>();

		public void Clear()
		{
			GossipOptions = Array.Empty<GossipMenuItem>();
			QuestOptions = Array.Empty<QuestGossipEntry>();
		}

		public void Update([NotNull] IReadOnlyList<GossipMenuItem> options, [NotNull] IReadOnlyList<QuestGossipEntry> quests)
		{
			GossipOptions = options ?? throw new ArgumentNullException(nameof(options));
			QuestOptions = quests ?? throw new ArgumentNullException(nameof(quests));
		}
	}
}
