using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AuraDataChangedHandlerTickable : EventQueueBasedTickable<IAuraStateChangedEventSubscribable, AuraStateChangedEventArgs>, IAuraApplicationAppliedEventSubscribable, IAuraApplicationRemovedEventSubscribable
	{
		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		public event EventHandler<AuraApplicationAppliedEventArgs> OnAuraApplicationApplied;

		public event EventHandler<AuraApplicationRemovedEventArgs> OnAuraApplicationRemoved;

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
					OnAuraApplicationRemoved?.Invoke(this, new AuraApplicationRemovedEventArgs(args.Data.SlotIndex, slotData.AuraSpellId));
				}
				else
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Attempted to Remove Aura Slot: {args.Data.SlotIndex} but Slot was not active on Entity: {args.Target}");
				}
			}
			else
			{
				if (collection.IsSlotActive(args.Data.SlotIndex))
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Attempted to Apply Aura Slot: {args.Data.SlotIndex} Spell: {args.Data.AuraSpellId} to Slot but Slot was already active on Entity: {args.Target}");
				}
				else
				{
					collection.Apply(args.Data);

					//This broadcasts the main thead engine event for aura apply/remove
					OnAuraApplicationApplied?.Invoke(this, new AuraApplicationAppliedEventArgs(args.Data.SlotIndex, args.Data.AuraSpellId, args.Data.State));
				}
			}
		}
	}
}
