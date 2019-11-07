using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class CleanupInterestCollectionsEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionFinishedEventSubscribable, EntityDeconstructionFinishedEventArgs>
	{
		private IReadonlyEntityGuidMappable<InterestCollection> InterestCollections { get; }

		private IEntityInterestChangeEventSpoofable InterestEventSpoofer { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public CleanupInterestCollectionsEventListener(IEntityDeconstructionFinishedEventSubscribable subscriptionService, 
			IReadonlyEntityGuidMappable<InterestCollection> interestCollections, 
			IEntityInterestChangeEventSpoofable interestEventSpoofer,
			[NotNull] IReadonlyKnownEntitySet knownEntities) 
			: base(subscriptionService)
		{
			InterestCollections = interestCollections;
			InterestEventSpoofer = interestEventSpoofer;
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		protected override void OnEventFired(object source, EntityDeconstructionFinishedEventArgs args)
		{
			//At this point, the entity deconstruction is FINISHED so we can actually tell all known entites to forget it.
			//Threadsafe internally to iterate
			foreach(NetworkEntityGuid entity in KnownEntities)
			{
				InterestCollection ic = InterestCollections.RetrieveEntity(entity);

				if(ic.Contains(args.EntityGuid))
				{
					//We just spoof an exit to every interested collection who knows of the entity being cleaned up.
					InterestEventSpoofer.SpoofExitInterest(new EntityInterestChangeEventArgs(entity, args.EntityGuid, EntityInterestChangeEventArgs.ChangeType.Exit));
				}
			}
		}
	}
}
