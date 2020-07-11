using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseQuestRequirementsWindowCreateEventListener : BaseSingleEventListenerInitializable<IQuestRequirementsWindowCreateEventSubscribable, QuestRequirementsWindowCreateEventArgs>
	{
		protected BaseQuestRequirementsWindowCreateEventListener(IQuestRequirementsWindowCreateEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
