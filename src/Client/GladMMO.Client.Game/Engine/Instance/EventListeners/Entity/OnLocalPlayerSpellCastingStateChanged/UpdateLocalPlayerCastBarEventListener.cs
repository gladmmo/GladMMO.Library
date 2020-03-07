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
		[CanBeNull]
		public SpellDefinitionDataModel SpellDefinition { get; }

		/// <summary>
		/// The starting cast timestamp. 0 if <see cref="isSpellCasting"/> is false.
		/// </summary>
		public long StartTimeStamp { get; }

		public BarCastingState(bool isSpellCasting, [CanBeNull] [NotNull] SpellDefinitionDataModel spellDefinition, long startTimeStamp)
		{
			if(startTimeStamp < 0) throw new ArgumentOutOfRangeException(nameof(startTimeStamp));

			this.isSpellCasting = isSpellCasting;
			SpellDefinition = spellDefinition ?? throw new ArgumentNullException(nameof(spellDefinition));
			StartTimeStamp = startTimeStamp;
		}

		public BarCastingState(bool isSpellCasting)
		{
			this.isSpellCasting = isSpellCasting;
		}
	}

	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class UpdateLocalPlayerCastBarEventListener : BaseLocalPlayerSpellCastingStateChanged, IGameTickable
	{
		private ILog Logger { get; }

		private IUICastingBar CastingBar { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlySpellDataCollection SpellDataCollection { get; }

		private BarCastingState CastingState { get; set; } = new BarCastingState(false);

		/// <inheritdoc />
		public UpdateLocalPlayerCastBarEventListener(ILocalPlayerSpellCastingStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerCastBar)] [NotNull] IUICastingBar castingBar,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlySpellDataCollection spellDataCollection)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CastingBar = castingBar ?? throw new ArgumentNullException(nameof(castingBar));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
		}

		public void Tick()
		{
			if (!CastingState.isSpellCasting)
				return;

			long currentRemoteTime = TimeService.CurrentRemoteTime;

			//This is UTC tick time. We need to convert it to seconds.
			TimeSpan span = new TimeSpan(Math.Max(0, currentRemoteTime - CastingState.StartTimeStamp)); //time sync may be abit off so clamp it

			CastingBar.CastingBarFillable.FillAmount = (float)(span.TotalMilliseconds / (float)CastingState.SpellDefinition.CastTime);
		}

		protected override void OnEventFired(object source, SpellCastingStateChangedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Player started casting Spell: {args.CastingSpellId}");

			//Spell casting stopped. Disable the bar.
			if(!args.isCasting)
			{
				CastingBar.SetElementActive(false);
				CastingBar.CastingBarFillable.FillAmount = 0;
				CastingState = new BarCastingState(false);
			}
			else
			{
				SpellDefinitionDataModel spellDefinition = SpellDataCollection.GetSpellDefinition(args.CastingSpellId);
				CastingState = new BarCastingState(true, spellDefinition, args.CastingStartTimeStamp);
				CastingBar.CastingBarSpellNameText.Text = spellDefinition.SpellName;
				CastingBar.SetElementActive(true);
			}
		}
	}
}
