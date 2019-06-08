using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnMoveInputChangeNetworkSendInputEventListener : BaseSingleEventListenerInitializable<IMovementInputChangedEventSubscribable, MovementInputChangedEventArgs>
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		public OnMoveInputChangeNetworkSendInputEventListener(IMovementInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			SendService.SendMessage(new ClientMovementDataUpdateRequest(new Vector2(args.NewHorizontalInput, args.NewVerticalInput)));
		}
	}
}
