using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="QuestWindowCreateEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnQuestWindowCreate"/>
	/// </summary>
	public interface IQuestWindowCreateEventSubscribable
	{
		event EventHandler<QuestWindowCreateEventArgs> OnQuestWindowCreate;
	}

	/// <summary>
	/// Event arguments for the <see cref="IQuestWindowCreateEventSubscribable"/> interface.
	/// </summary>
	public sealed class QuestWindowCreateEventArgs : EventArgs
	{
		public ObjectGuid QuestGiver { get; }

		public int QuestId { get; }

		public QuestTextContentModel Content { get; }

		public QuestWindowCreateEventArgs([NotNull] ObjectGuid questGiver, int questId, [NotNull] QuestTextContentModel content)
		{
			if (questId <= 0) throw new ArgumentOutOfRangeException(nameof(questId));

			QuestGiver = questGiver ?? throw new ArgumentNullException(nameof(questGiver));
			Content = content ?? throw new ArgumentNullException(nameof(content));
			QuestId = questId;
		}
	}
}