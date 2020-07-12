using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="QuestTurnInEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnQuestTurnIn"/>
	/// </summary>
	public interface IQuestTurnInEventSubscribable
	{
		event EventHandler<QuestTurnInEventArgs> OnQuestTurnIn;
	}

	/// <summary>
	/// Event arguments for the <see cref="IQuestTurnInEventSubscribable"/> interface.
	/// </summary>
	public sealed class QuestTurnInEventArgs : EventArgs
	{
		/// <summary>
		/// The ID of the quest turned in.
		/// </summary>
		public int QuestId { get; }

		public QuestRewardModel Reward { get; }

		public QuestTurnInEventArgs(int questId, [NotNull] QuestRewardModel reward)
		{
			if (questId <= 0) throw new ArgumentOutOfRangeException(nameof(questId));

			QuestId = questId;
			Reward = reward ?? throw new ArgumentNullException(nameof(reward));
		}
	}
}