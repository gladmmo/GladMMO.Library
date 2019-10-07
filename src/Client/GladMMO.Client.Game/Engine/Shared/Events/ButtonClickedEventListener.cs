using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class ButtonClickedEventListener<TButtonClickedEventType> : BaseSingleEventListenerInitializable<TButtonClickedEventType, ButtonClickedEventArgs>
		where TButtonClickedEventType : class, IButtonClickedEventSubscribable
	{
		protected ButtonClickedEventListener(TButtonClickedEventType subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
