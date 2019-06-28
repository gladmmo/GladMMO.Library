using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class CleanupInterestCollectionsEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionStartingEventSubscribable, EntityDeconstructionStartingEventArgs>
	{
		private IReadonlyEntityGuidMappable<InterestCollection> InterestCollections { get; }

		private IEntityInterestChangeEventSpoofable InterestEventSpoofer { get; }

		public CleanupInterestCollectionsEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService, 
			IReadonlyEntityGuidMappable<InterestCollection> interestCollections, 
			IEntityInterestChangeEventSpoofable interestEventSpoofer) 
			: base(subscriptionService)
		{
			InterestCollections = interestCollections;
			InterestEventSpoofer = interestEventSpoofer;
		}

		protected override void OnEventFired(object source, EntityDeconstructionStartingEventArgs args)
		{
			//To avoid major issues with previous physics based issue we're writing this horriblely slow, hacky solution\
			//This actually scales O(n) which isn't bad. A quick iteration and a hashmap check basically.
			foreach(var ic in InterestCollections)
			{
				if(ic.Value.Contains(args.EntityGuid))
				{
					//We just spoof an exit to every interested collection who knows of the entity being cleaned up.
					InterestEventSpoofer.SpoofExitInterest(new EntityInterestChangeEventArgs(ic.Key, args.EntityGuid, EntityInterestChangeEventArgs.ChangeType.Exit));
				}
			}
		}
	}
}
