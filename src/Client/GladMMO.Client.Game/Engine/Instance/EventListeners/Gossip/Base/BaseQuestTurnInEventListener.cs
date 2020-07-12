using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class BaseQuestTurnInEventListener : BaseSingleEventListenerInitializable<IQuestTurnInEventSubscribable, QuestTurnInEventArgs>
	{
		protected BaseQuestTurnInEventListener(IQuestTurnInEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
