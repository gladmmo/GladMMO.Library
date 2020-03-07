using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnCameraInputChangedSendRotationUpdateEventListener : BaseSingleEventListenerInitializable<ICameraInputChangedEventSubscribable, CameraInputChangedEventArgs>
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<WorldTransform> TransformMappable { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public OnCameraInputChangedSendRotationUpdateEventListener(ICameraInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<WorldTransform> transformMappable,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			TransformMappable = transformMappable ?? throw new ArgumentNullException(nameof(transformMappable));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override void OnEventFired(object source, CameraInputChangedEventArgs args)
		{
			WorldTransform entity = TransformMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);

			SendService.SendMessage(new ClientRotationDataUpdateRequest(args.Rotation, TimeService.CurrentRemoteTime, new Vector3(entity.PositionX, entity.PositionY, entity.PositionZ)));
		}
	}
}
