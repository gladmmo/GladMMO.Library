using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class BarCastingState
	{
		public bool isSpellCasting { get; }

		/// <summary>
		/// Null if <see cref="isSpellCasting"/> is not true.
		/// </summary>
		public SpellEntry<string> SpellDefinition { get; }

		/// <summary>
		/// The starting cast timestamp. 0 if <see cref="isSpellCasting"/> is false.
		/// </summary>
		public long StartTimeStamp { get; }

		public long EndTimeStamp { get; }

		//TODO: Use spell entry dutation eventually
		public long CastDuration => EndTimeStamp - StartTimeStamp;

		/// <summary>
		/// Temporarily unique-ish identifier.
		/// </summary>
		public long Identifier { get; }

		public BarCastingState(bool isSpellCasting, [NotNull] SpellEntry<string> spellDefinition, long startTimeStamp, long endTimeStamp, long identifier)
		{
			if (startTimeStamp < 0) throw new ArgumentOutOfRangeException(nameof(startTimeStamp));
			if (endTimeStamp <= 0) throw new ArgumentOutOfRangeException(nameof(endTimeStamp));

			this.isSpellCasting = isSpellCasting;
			SpellDefinition = spellDefinition ?? throw new ArgumentNullException(nameof(spellDefinition));
			StartTimeStamp = startTimeStamp;
			EndTimeStamp = endTimeStamp;
			Identifier = identifier;
		}

		public BarCastingState(bool isSpellCasting, long identifier)
		{
			this.isSpellCasting = isSpellCasting;
			Identifier = identifier;
		}
	}

	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class UpdateLocalPlayerCastBarEventListener : BaseLocalPlayerSpellCastingStateChanged, IGameTickable
	{
		private ILog Logger { get; }

		private IUICastingBar CastingBar { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private BarCastingState CastingState { get; set; } = new BarCastingState(false, 0);

		/// <inheritdoc />
		public UpdateLocalPlayerCastBarEventListener(ILocalPlayerSpellCastingStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerCastBar)] [NotNull] IUICastingBar castingBar,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IClientDataCollectionContainer clientData)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CastingBar = castingBar ?? throw new ArgumentNullException(nameof(castingBar));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
		}

		public void Tick()
		{
			if (!CastingState.isSpellCasting)
				return;

			long currentRemoteTime = TimeService.CurrentRemoteTime;

			//This is UTC tick time. We need to convert it to seconds.
			long span = currentRemoteTime - CastingState.StartTimeStamp; //time sync may be abit off so clamp it

			//TODO: Handle spell duration.
			CastingBar.CastingBarFillable.FillAmount = (float)(span / (float)CastingState.CastDuration);
		}

		protected override void OnThreadUnSafeEventFired(object source, SpellCastingStateChangedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player started casting Spell: {args.CastingSpellId}");

			//If the current state and the new state match keys, then it's an update to the current casting state
			if (CastingState.Identifier == args.SpellCastIdentifier)
			{
				//TODO: Different handling for STOPPED vs Canceled/Interrupted.
				//Spell casting stopped. Disable the bar.
				if((args.State == SpellCastingEventChangeState.Canceled || args.State == SpellCastingEventChangeState.Stopped) && CastingState.isSpellCasting)
				{
					CastingBar.SetElementActive(false);
					CastingBar.CastingBarFillable.FillAmount = 0;
					CastingState = new BarCastingState(false, args.SpellCastIdentifier);
				}
			}
			else
			{
				//otherwise, if keys don't match this is a NEW cast and we should ignore any old or current state.
				if (args.isCasting)
				{
					SpellEntry<string> entry = ClientData.AssertEntry<SpellEntry<string>>(args.CastingSpellId);

					CastingState = new BarCastingState(true, entry, TimeService.CurrentRemoteTime, TimeService.CurrentRemoteTime + args.RemainingCastTime, args.SpellCastIdentifier);
					CastingBar.CastingBarSpellNameText.Text = entry.SpellName.enUS;
					CastingBar.SetElementActive(true);
				}
			}
		}
	}
}
