using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientClearEntityCollectionRemovablesEventListener : SharedClearEntityCollectionRemovablesEventListener
	{
		public ClientClearEntityCollectionRemovablesEventListener(IEntityWorldRepresentationDeconstructionFinishedEventSubscribable subscriptionService, 
			IReadOnlyCollection<IEntityCollectionRemovable> removableCollections) 
			: base(subscriptionService, removableCollections)
		{

		}
	}
}
