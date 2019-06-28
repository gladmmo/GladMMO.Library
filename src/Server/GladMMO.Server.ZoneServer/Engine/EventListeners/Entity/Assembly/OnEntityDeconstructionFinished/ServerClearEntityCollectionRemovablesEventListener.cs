using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerClearEntityCollectionRemovablesEventListener : SharedClearEntityCollectionRemovablesEventListener
	{
		public ServerClearEntityCollectionRemovablesEventListener(IEntityWorldRepresentationDeconstructionFinishedEventSubscribable subscriptionService, 
			IReadOnlyCollection<IEntityCollectionRemovable> removableCollections) 
			: base(subscriptionService, removableCollections)
		{

		}
	}
}
