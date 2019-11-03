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

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> TransformMap { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		public OnMoveInputChangeNetworkSendInputEventListener(IMovementInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] ILocalPlayerDetails playerDetails) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override void OnEventFired(object source, MovementInputChangedEventArgs args)
		{
			//We send remote time instead of remoteTime + latency because
			//our client is going to move right away and we want EVERYONE
			//to view us as if we had started moving at the same time as the
			//local client percieves it.
			long timeStamp = TimeService.CurrentRemoteTime;

			//We also are going to send a position hint to the server.
			//Server has authority in rejecting this hint, and it should if it finds its
			//WAY off. However this is how we deal with the issue of desyncronization
			//by having the client be semi-authorative about where it is.
			WorldTransform entity = TransformMap.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			SendService.SendMessage(new ClientMovementDataUpdateRequest(new Vector2(args.NewHorizontalInput, args.NewVerticalInput), timeStamp, entity.Position));
		}
	}
}
