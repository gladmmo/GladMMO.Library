using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RemoveLocalPlayerAppliedAuraEventListener : BaseSingleEventListenerInitializable<IAuraApplicationRemovedEventSubscribable, AuraApplicationRemovedEventArgs>
	{
		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private ILog Logger { get; }

		public RemoveLocalPlayerAppliedAuraEventListener([NotNull] IAuraApplicationRemovedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.AuraBuffCollection)] [NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, AuraApplicationRemovedEventArgs args)
		{
			if (PlayerDetails.LocalPlayerGuid != args.Target)
				return;

			IUIAuraBuffSlot slot = AuraBuffUICollection[AuraBuffType.Positive, args.Slot];
			slot.RootElement.SetElementActive(false);
		}
	}
}
