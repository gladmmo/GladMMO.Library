using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseQuestWindowCreateEventListener : BaseSingleEventListenerInitializable<IQuestWindowCreateEventSubscribable, QuestWindowCreateEventArgs>
	{
		protected BaseQuestWindowCreateEventListener(IQuestWindowCreateEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
