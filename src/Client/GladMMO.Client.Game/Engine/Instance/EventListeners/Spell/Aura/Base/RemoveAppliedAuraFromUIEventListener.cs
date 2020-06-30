using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class RemoveAppliedAuraFromUIEventListener : BaseSingleEventListenerInitializable<IAuraApplicationRemovedEventSubscribable, AuraApplicationRemovedEventArgs>
	{
		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		protected ILog Logger { get; }

		protected RemoveAppliedAuraFromUIEventListener([NotNull] IAuraApplicationRemovedEventSubscribable subscriptionService,
			[NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Abstract method that indicates if this <see cref="RemoveAppliedAuraFromUIEventListener"/> is handling aura
		/// updates from a particular Entity.
		/// </summary>
		/// <param name="target">Aura target from event.</param>
		/// <returns>True if this UI handler is handling auras for this target.</returns>
		protected abstract bool IsHandlingTarget(ObjectGuid target);

		protected sealed override void OnEventFired(object source, AuraApplicationRemovedEventArgs args)
		{
			if (!IsHandlingTarget(args.Target))
				return;

			//TODO: Need to support disabling NEGATIVE elements.
			IUIAuraBuffSlot slot = AuraBuffUICollection[AuraBuffType.Positive, args.Slot];
			slot.RootElement.SetElementActive(false);
		}
	}
}
