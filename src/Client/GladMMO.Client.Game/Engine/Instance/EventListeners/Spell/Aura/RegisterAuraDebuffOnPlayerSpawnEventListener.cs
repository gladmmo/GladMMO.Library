using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	//This isn't really an aura event listener, but we START listening when player spawns.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RegisterAuraDebuffOnPlayerSpawnEventListener : OnLocalPlayerSpawnedEventListener
	{
		private IUIAuraBuffCollection AuraBuffUICollection { get; }

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		public RegisterAuraDebuffOnPlayerSpawnEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.AuraBuffCollection)] [NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService) 
			: base(subscriptionService)
		{
			AuraBuffUICollection = auraBuffUiCollection ?? throw new ArgumentNullException(nameof(auraBuffUiCollection));
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			AuraBuffUICollection.OnAuraBuffClicked += OnAuraBuffClicked;
		}

		private void OnAuraBuffClicked(object sender, AuraBuffClickedEventArgs args)
		{
			IAuraApplicationCollection collection = AuraApplicationMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			//Why wouldn't it be active?? But honeslty, might as very verify.
			if (collection.IsSlotActive(args.Slot))
			{
				SendService.SendMessage(new CMSG_CANCEL_AURA_Payload(collection[args.Slot].Data.AuraSpellId));
			}
		}
	}
}
