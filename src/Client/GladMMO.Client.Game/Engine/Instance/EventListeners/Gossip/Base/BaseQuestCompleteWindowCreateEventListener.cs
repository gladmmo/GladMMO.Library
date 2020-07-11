using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseQuestCompleteWindowCreateEventListener : BaseSingleEventListenerInitializable<IQuestCompleteWindowCreateEventSubscribable, QuestCompleteWindowCreateEventArgs>
	{
		protected BaseQuestCompleteWindowCreateEventListener(IQuestCompleteWindowCreateEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
