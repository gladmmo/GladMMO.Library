using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="QuestRequirementWindowCreateEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnQuestRequirementWindowCreate"/>
	/// </summary>
	public interface IQuestRequirementsWindowCreateEventSubscribable
	{
		event EventHandler<QuestRequirementWindowCreateEventArgs> OnQuestRequirementWindowCreate;
	}

	/// <summary>
	/// Event arguments for the <see cref="IQuestRequirementsWindowCreateEventSubscribable"/> interface.
	/// </summary>
	public sealed class QuestRequirementWindowCreateEventArgs : EventArgs
	{
		public ObjectGuid QuestGiver { get; }

		public int QuestId { get; }

		public string Requirements { get; }

		public QuestRequirementWindowCreateEventArgs([NotNull] ObjectGuid questGiver, int questId, [NotNull] string requirements)
		{
			if (questId <= 0) throw new ArgumentOutOfRangeException(nameof(questId));

			QuestGiver = questGiver ?? throw new ArgumentNullException(nameof(questGiver));
			QuestId = questId;
			Requirements = requirements ?? throw new ArgumentNullException(nameof(requirements));
		}
	}
}