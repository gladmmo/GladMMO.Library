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

		public SharedClearEntityCollectionRemovablesEventListener(IEntityWorldRepresentationDeconstructionFinishedEventSubscribable subscriptionService,
			[NotNull] IReadOnlyCollection<IEntityCollectionRemovable> removableCollections) 
			: base(subscriptionService)
		{
			RemovableCollections = removableCollections ?? throw new ArgumentNullException(nameof(removableCollections));
		}

		protected override void OnEventFired(object source, EntityWorldRepresentationDeconstructionFinishedEventArgs args)
		{
			//When the entity deconstruction is finally finished, we're free to cleanup/freeup the entity
			//data within the collections.
			foreach(var removable in RemovableCollections)
				removable.RemoveEntityEntry(args.EntityGuid);
		}
	}
}
