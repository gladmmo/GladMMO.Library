using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="QuestCompleteWindowCreateEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnQuestCompleteWindowCreate"/>
	/// </summary>
	public interface IQuestCompleteWindowCreateEventSubscribable
	{
		event EventHandler<QuestCompleteWindowCreateEventArgs> OnQuestCompleteWindowCreate;
	}

	/// <summary>
	/// Event arguments for the <see cref="IQuestCompleteWindowCreateEventSubscribable"/> interface.
	/// </summary>
	public sealed class QuestCompleteWindowCreateEventArgs : EventArgs
	{
		public ObjectGuid QuestGiver { get; }

		public int QuestId { get; }

		public string CompletionText { get; }

		public QuestCompleteWindowCreateEventArgs([NotNull] ObjectGuid questGiver, int questId, [NotNull] string completionText)
		{
			if(questId <= 0) throw new ArgumentOutOfRangeException(nameof(questId));

			QuestGiver = questGiver ?? throw new ArgumentNullException(nameof(questGiver));
			QuestId = questId;
			CompletionText = completionText ?? throw new ArgumentNullException(nameof(completionText));
		}
	}
}