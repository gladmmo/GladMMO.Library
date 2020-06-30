using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalLoadSpellIconForAuraEventListener : LoadSpellIconForAuraEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public LocalLoadSpellIconForAuraEventListener(IAuraApplicationAppliedEventSubscribable subscriptionService, 
			IAddressableContentLoader contentLoadService,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerAuraBuffCollection)] IUIAuraBuffCollection auraBuffUiCollection, 
			IClientDataCollectionContainer clientData, 
			IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService, contentLoadService, auraBuffUiCollection, clientData, auraApplicationMappable)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override bool IsHandlingTarget(ObjectGuid target)
		{
			return PlayerDetails.LocalPlayerGuid == target;
		}
	}
}
