using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerUnitFrameResourceUpdateEventListener : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>
	{
		private IEntityDataChangeCallbackRegisterable EntityDataCallbackRegister { get; }

		/// <summary>
		/// The local player's details
		/// </summary>
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IUIUnitFrame PlayerUnitFrame { get; }

		/// <inheritdoc />
		public LocalPlayerUnitFrameResourceUpdateEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, [NotNull] IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, [NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.PlayerUnitFrame)] IUIUnitFrame playerUnitFrame) 
			: base(subscriptionService)
		{
			EntityDataCallbackRegister = entityDataCallbackRegister ?? throw new ArgumentNullException(nameof(entityDataCallbackRegister));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			PlayerUnitFrame = playerUnitFrame ?? throw new ArgumentNullException(nameof(playerUnitFrame));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			//One local player spawn we want to subscribe the resource updates
			RecalulateHealthUI(PlayerDetails.EntityData.GetFieldValue<int>((int)EntityObjectField.UNIT_FIELD_HEALTH));

			//TODO: Any way to do type inference on generics for Action types???
			//We should subscribe to changes in the current health
			EntityDataCallbackRegister.RegisterCallback<int>(PlayerDetails.LocalPlayerGuid, (int)EntityObjectField.UNIT_FIELD_HEALTH, OnCurrentHealthChangedValue);
		}

		private void RecalulateHealthUI(int currentHealth)
		{
			float healthPercentage = (float)currentHealth / PlayerDetails.EntityData.GetFieldValue<int>((int)EntityObjectField.UNIT_FIELD_MAXHEALTH);

			PlayerUnitFrame.HealthBar.BarFillable.FillAmount = healthPercentage;

			//Also we want to see the percentage text
			PlayerUnitFrame.HealthBar.BarText.Text = $"{currentHealth} / {PlayerDetails.EntityData.GetFieldValue<int>((int)EntityObjectField.UNIT_FIELD_MAXHEALTH)}";
		}

		private void OnCurrentHealthChangedValue(NetworkEntityGuid source, EntityDataChangedArgs<int> changeArgs)
		{
			RecalulateHealthUI(changeArgs.NewValue);
		}
	}
}
