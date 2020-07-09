using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="LocalPlayerQuestAddedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnLocalPlayerQuestAdded"/>
	/// </summary>
	public interface ILocalPlayerQuestAddedEventSubscribable
	{
		event EventHandler<LocalPlayerQuestAddedEventArgs> OnLocalPlayerQuestAdded;
	}

	/// <summary>
	/// Event arguments for the <see cref="ILocalPlayerQuestAddedEventSubscribable"/> interface.
	/// </summary>
	public sealed class LocalPlayerQuestAddedEventArgs : EventArgs
	{
		/// <summary>
		/// Represents the slot the quest is added to.
		/// Ranges from 0 to 24. (25 quests)
		/// </summary>
		public int QuestSlot { get; }

		/// <summary>
		/// The id of the quest.
		/// </summary>
		public int QuestId { get; }

		public LocalPlayerQuestAddedEventArgs(int questSlot, int questId)
		{
			//TODO: Verify against quest max range.
			if (questSlot < 0) throw new ArgumentOutOfRangeException(nameof(questSlot));
			if (questId <= 0) throw new ArgumentOutOfRangeException(nameof(questId));

			QuestSlot = questSlot;
			QuestId = questId;
		}
	}
}