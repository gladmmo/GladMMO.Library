using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AuraDataChangedHandlerTickable : EventQueueBasedTickable<IAuraStateChangedEventSubscribable, AuraStateChangedEventArgs>
	{
		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

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
				collection.Remove(args.Data.SlotIndex);
			else
				collection.Apply(args.Data);
		}
	}
}
