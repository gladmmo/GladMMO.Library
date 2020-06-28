using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IAuraApplicationUpdatedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IAuraApplicationRemovedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IAuraApplicationAppliedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AuraDataChangedHandlerTickable : EventQueueBasedTickable<IAuraStateChangedEventSubscribable, AuraStateChangedEventArgs>, 
		IAuraApplicationAppliedEventSubscribable, 
		IAuraApplicationRemovedEventSubscribable,
		IAuraApplicationUpdatedEventSubscribable
	{
		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		public event EventHandler<AuraApplicationAppliedEventArgs> OnAuraApplicationApplied;

		public event EventHandler<AuraApplicationRemovedEventArgs> OnAuraApplicationRemoved;

		public event EventHandler<AuraApplicationUpdatedEventArgs> OnAuraApplicationUpdated;

		public AuraDataChangedHandlerTickable(IAuraStateChangedEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable) 
			: base(subscriptionService, true, logger)
		{
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
		}

		protected override void HandleEvent(AuraStateChangedEventArgs args)
		{
			if (!KnownEntities.isEntityKnown(args.Target))
			{
				if (Logger.IsWarnEnabled)
					Logger.Warn($"Aura Update from Unknown Entity: {args.Target}");

				return;
			}

			if (Logger.IsDebugEnabled)
				Logger.Debug($"Aura Update. Entity: {args.Target} IsRemove: {args.Data.IsAuraRemoved} Slot: {args.Data.SlotIndex} SpellId: {args.Data.AuraSpellId}");

			IAuraApplicationCollection collection = AuraApplicationMappable.RetrieveEntity(args.Target);

			if (args.Data.IsAuraRemoved)
			{
				if (collection.IsSlotActive(args.Data.SlotIndex))
				{
					var slotData = collection[args.Data.SlotIndex];
					collection.Remove(args.Data.SlotIndex);

					//This broadcasts the main thead engine event for aura apply/remove
					OnAuraApplicationRemoved?.Invoke(this, new AuraApplicationRemovedEventArgs(args.Target, args.Data.SlotIndex, slotData.AuraSpellId));
				}
				else
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Attempted to Remove Aura Slot: {args.Data.SlotIndex} but Slot was not active on Entity: {args.Target}");
				}
			}
			else
			{
				//It's possible to get aura update events that aren't removal for already active
				//aura slots. This can mean that the aura application state or data has changed serverside
				//and is NOT an error.
				if (collection.IsSlotActive(args.Data.SlotIndex))
				{
					collection.Update(args.Data);

					OnAuraApplicationUpdated?.Invoke(this, new AuraApplicationUpdatedEventArgs(args.Data.SlotIndex, args.Target, args.Data.AuraSpellId, args.Data.State));
				}
				else
				{
					collection.Apply(args.Data);

					//This broadcasts the main thead engine event for aura apply/remove
					OnAuraApplicationApplied?.Invoke(this, new AuraApplicationAppliedEventArgs(args.Data.SlotIndex, args.Target, args.Data.AuraSpellId, args.Data.State));
				}
			}
		}
	}
}
