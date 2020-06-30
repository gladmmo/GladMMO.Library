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
	//TODO: It is bad that we register this as this type directly, since it's generalized.
	[AdditionalRegisterationAs(typeof(IAuraDataUpdateApplyable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class TargetLoadSpellIconForAuraEventListener : LoadSpellIconForAuraEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public TargetLoadSpellIconForAuraEventListener(IAuraApplicationAppliedEventSubscribable subscriptionService, 
			IAddressableContentLoader contentLoadService,
			[KeyFilter(UnityUIRegisterationKey.TargetAuraBuffCollection)] IUIAuraBuffCollection auraBuffUiCollection, 
			IClientDataCollectionContainer clientData, 
			IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService, contentLoadService, auraBuffUiCollection, clientData, auraApplicationMappable)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override bool IsHandlingTarget(ObjectGuid target)
		{
			return PlayerDetails.EntityData.GetEntityGuidValue(EUnitFields.UNIT_FIELD_TARGET) == target;
		}
	}
}
