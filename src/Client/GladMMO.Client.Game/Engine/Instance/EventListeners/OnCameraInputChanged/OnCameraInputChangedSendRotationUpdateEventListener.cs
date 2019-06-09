using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnCameraInputChangedSendRotationUpdateEventListener : BaseSingleEventListenerInitializable<ICameraInputChangedEventSubscribable, CameraInputChangedEventArgs>
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		public OnCameraInputChangedSendRotationUpdateEventListener(ICameraInputChangedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnEventFired(object source, CameraInputChangedEventArgs args)
		{
			SendService.SendMessage(new ClientRotationDataUpdateRequest(args.Rotation));
		}
	}
}
