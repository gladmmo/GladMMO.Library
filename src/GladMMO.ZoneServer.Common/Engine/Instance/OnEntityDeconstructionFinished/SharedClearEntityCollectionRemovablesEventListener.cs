using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Base type for entity collection removable cleanup listener.
	/// </summary>
	public abstract class SharedClearEntityCollectionRemovablesEventListener : BaseSingleEventListenerInitializable<IEntityWorldRepresentationDeconstructionFinishedEventSubscribable, EntityWorldRepresentationDeconstructionFinishedEventArgs>
	{
		private IReadOnlyCollection<IEntityCollectionRemovable> RemovableCollections { get; }

		private bool ShouldCleanupPlayerData { get; }

		public SharedClearEntityCollectionRemovablesEventListener(IEntityWorldRepresentationDeconstructionFinishedEventSubscribable subscriptionService,
			[NotNull] IReadOnlyCollection<IEntityCollectionRemovable> removableCollections,
			bool shouldCleanupPlayerData) 
			: base(subscriptionService)
		{
			RemovableCollections = removableCollections ?? throw new ArgumentNullException(nameof(removableCollections));
			ShouldCleanupPlayerData = shouldCleanupPlayerData;
		}

		protected override void OnEventFired(object source, EntityWorldRepresentationDeconstructionFinishedEventArgs args)
		{
			//Zoneserver handles player differently, for peristence purposes.
			if (!ShouldCleanupPlayerData && args.EntityGuid.EntityType == EntityType.Player)
				return;

			//When the entity deconstruction is finally finished, we're free to cleanup/freeup the entity
			//data within the collections.
			foreach(var removable in RemovableCollections)
				removable.RemoveEntityEntry(args.EntityGuid);
		}
	}
}
