using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//TODO: Make this a FIXED tickable.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerAuraBarTimeUpdateTickable : IGameTickable
	{
		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private bool ShouldTick { get; set; } = false;

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationCollection { get; }

		private IUIAuraBuffCollection LocalPlayerBuffUI { get; }

		public LocalPlayerAuraBarTimeUpdateTickable([NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] ILocalPlayerSpawnedEventSubscribable localPlayerSpawnEventSubscription,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationCollection,
			[KeyFilter(UnityUIRegisterationKey.AuraBuffCollection)] [NotNull] IUIAuraBuffCollection localPlayerBuffUi)
		{
			if (localPlayerSpawnEventSubscription == null) throw new ArgumentNullException(nameof(localPlayerSpawnEventSubscription));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			AuraApplicationCollection = auraApplicationCollection ?? throw new ArgumentNullException(nameof(auraApplicationCollection));
			LocalPlayerBuffUI = localPlayerBuffUi ?? throw new ArgumentNullException(nameof(localPlayerBuffUi));

			localPlayerSpawnEventSubscription.OnLocalPlayerSpawned += EnableTickable;
		}

		private void EnableTickable(object sender, LocalPlayerSpawnedEventArgs e)
		{
			ShouldTick = true;
		}

		public void Tick()
		{
			if (!ShouldTick)
				return;

			IAuraApplicationCollection auraApplicationCollection = AuraApplicationCollection.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			int currentRemoteTime = (int)TimeService.CurrentRemoteTime;

			foreach (var application in auraApplicationCollection)
			{
				//No duration, do not tick this.
				if (!application.Data.State.HasDuration)
					continue;

				IUIAuraBuffSlot buffSlot = LocalPlayerBuffUI[AuraBuffType.Positive, application.Data.SlotIndex];

				//This is a hacky optimization that avoid allocations
				string currentDurationText = buffSlot.DurationText.Text;

				//Format should be LAST character is the TIME unit
				//Before that is a space
				int valueLength = currentDurationText.Length - 2; //Should never be more than 2
				char unit = currentDurationText[currentDurationText.Length - 1];
				int convertedValue = valueLength > 1 ? ((currentDurationText[0] - '0') * 10) + (currentDurationText[1] - '0') : (currentDurationText[0] - '0');

				//What would be the new aura value if we computed it?
				//This is in MILLISECONDS
				int startTimeDiff = currentRemoteTime - application.ApplicationTimeStamp;

				//Compute NEW values
				int unitizedRemainingDuration = ComputeRemainingDuration(startTimeDiff, application.Data.State.MaximumAuraDuration, out char remainingDurationUnit);

				//No update required
				if (unit == remainingDurationUnit && (unitizedRemainingDuration == convertedValue))
					continue;

				//Otherwise, we're not in sync and NEED to update this.
				buffSlot.DurationText.Text = $"{unitizedRemainingDuration} {remainingDurationUnit}";
			}
		}

		private int ComputeRemainingDuration(int startTimeDiff, int stateMaximumAuraDuration, out char c)
		{
			int remainingDuration = stateMaximumAuraDuration - startTimeDiff;

			if(remainingDuration <= 60 * 1000) //60 seconds
			{
				c = 's';
				return remainingDuration / 1000;
			}
			else if(remainingDuration <= 60 * 60 * 1000) //less than 1 hour
			{
				c = 'm';
				return remainingDuration / (60 * 1000);
			}
			else //WOW, days!
			{
				//TODO: This is TOTALLY wrong.
				c = 'd';
				return remainingDuration / (24 * 60 * 1000);
			}
		}
	}
}
