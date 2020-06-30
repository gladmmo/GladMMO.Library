using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class DisplayAppliedAuraEventListener : BaseSingleEventListenerInitializable<IAuraApplicationAppliedEventSubscribable, AuraApplicationAppliedEventArgs>, IAuraDataUpdateApplyable
	{
		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		private ILog Logger { get; }

		protected DisplayAppliedAuraEventListener([NotNull] IAuraApplicationAppliedEventSubscribable subscriptionService,
			IAuraApplicationUpdatedEventSubscribable secondarySubscriptionService,
			[NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			//Manually register update event.
			secondarySubscriptionService.OnAuraApplicationUpdated += OnEventFired;
		}

		/// <summary>
		/// Abstract method that indicates if this <see cref="RemoveAppliedAuraFromUIEventListener"/> is handling aura
		/// updates from a particular Entity.
		/// </summary>
		/// <param name="target">Aura target from event.</param>
		/// <returns>True if this UI handler is handling auras for this target.</returns>
		protected abstract bool IsHandlingTarget(ObjectGuid target);

		private void OnEventFired(object source, AuraApplicationUpdatedEventArgs args)
		{
			ApplyAuraData(args);
		}

		protected override void OnEventFired(object source, AuraApplicationAppliedEventArgs args)
		{
			ApplyAuraData(args);
		}

		public void ApplyAuraData(IAuraApplicationDataEventContainer args)
		{
			if (!IsHandlingTarget(args.Target))
				return;

			IUIAuraBuffSlot buffSlot = AuraBuffUICollection[args.ApplicationData.Flags.ToBuffType(), args.Slot];

			DisplayAuraApplicationText(args, buffSlot);

			buffSlot.RootElement.SetElementActive(true);
		}

		protected virtual void DisplayAuraApplicationText(IAuraApplicationDataEventContainer args, IUIAuraBuffSlot buffSlot)
		{
			if (args.ApplicationData.HasDuration)
				buffSlot.DurationText.Text = ComputeAuraDurationText(args.ApplicationData);
			else
				buffSlot.DurationText.Text = String.Empty;

			if (args.ApplicationData.CounterAmount > 0)
				buffSlot.CounterText.Text = args.ApplicationData.CounterAmount.ToString();
			else
				buffSlot.CounterText.Text = String.Empty;
		}

		private string ComputeAuraDurationText([NotNull] AuraApplicationStateUpdate data)
		{
			if (data == null) throw new ArgumentNullException(nameof(data));

			if (!data.HasDuration)
				return "";

			int remainingDuration = data.CurrentAuraDuration;

			if (Logger.IsDebugEnabled)
				Logger.Debug($"Aura. Duration: {data.MaximumAuraDuration} Passed: {data.CurrentAuraDuration} Remaining: {remainingDuration}");

			if (remainingDuration <= 60 * 1000) //60 seconds
			{
				return $"{remainingDuration / 1000} s";
			}
			else if (remainingDuration <= 60 * 60 * 1000) //less than 1 hour
			{
				return $"{remainingDuration / (60 * 1000)} m";
			}
			else //WOW, days!
			{
				//TODO: This is TOTALLY wrong.
				return $"{remainingDuration / (24 * 60 * 1000)} d";
			}
		}
	}
}
