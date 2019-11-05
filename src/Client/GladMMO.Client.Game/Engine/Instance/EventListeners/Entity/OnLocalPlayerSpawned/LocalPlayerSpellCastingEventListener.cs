using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpellCastingEventListener : DataChangedLocalPlayerSpawnedEventListener, IGameTickable
	{
		private ILog Logger { get; }

		private bool isSpellCasting = false;

		private IUIFillableImage CastingBarFillable { get; }

		private IUIElement CastingBarRoot { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public LocalPlayerSpellCastingEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerCastBar)] [NotNull] IUIFillableImage castingBarFillable,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerCastBar)] [NotNull] IUIElement castingBarRoot)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CastingBarFillable = castingBarFillable ?? throw new ArgumentNullException(nameof(castingBarFillable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			CastingBarRoot = castingBarRoot ?? throw new ArgumentNullException(nameof(castingBarRoot));
		}

		/// <inheritdoc />
		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<int>(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, OnSpellCastingIdChanged);
		}

		private void OnSpellCastingIdChanged(NetworkEntityGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player started casting Spell: {changeArgs.NewValue}");

			isSpellCasting = changeArgs.NewValue > 0;

			//Spell casting stopped. Disable the bar.
			if (!isSpellCasting)
			{
				CastingBarRoot.SetElementActive(false);
				CastingBarFillable.FillAmount = 0;
			}
			else
				CastingBarRoot.SetElementActive(true);
		}

		public void Tick()
		{
			if (!isSpellCasting)
				return;

			long timeStarted = this.PlayerDetails.EntityData.GetFieldValue<long>(EntityObjectField.UNIT_FIELD_CASTING_STARTTIME);
			long currentRemoteTime = TimeService.CurrentRemoteTime;

			//This is UTC tick time. We need to convert it to seconds.
			TimeSpan span = new TimeSpan(Math.Max(0, currentRemoteTime - timeStarted)); //time sync may be abit off so clamp it

			//TODO: Don't assume all cast times are 1 second.
			CastingBarFillable.FillAmount = (float)(span.TotalSeconds / 1.0f);
		}
	}
}
