using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class ContentPrefabCompletedDownloadEventListener : BaseSingleEventListenerInitializable<IContentPrefabCompletedDownloadEventSubscribable, ContentPrefabCompletedDownloadEventArgs>
	{
		protected ContentPrefabCompletedDownloadEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}
	}
}
