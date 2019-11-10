using System;
using System.Collections.Generic;
using System.Text;
using GladMMO.Component;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DemoMakeObjectsClickableInteractableEventListener : EntityCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		public DemoMakeObjectsClickableInteractableEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService) 
			: base(subscriptionService)
		{
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//TODO: left over from bad desigh.
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			GameObject entity = GameObjectMappable.RetrieveEntity(args.EntityGuid);

			//Adding a test/demo collider to allow for a clicking volume.
			BoxCollider collider = entity.AddComponent<BoxCollider>();
			collider.isTrigger = true;
			entity.AddComponent<OnMouseClickedComponent>().OnMouseClicked += (sender, eventArgs) =>
			{
				SendService.SendMessage(new ClientInteractNetworkedObjectRequestPayload(args.EntityGuid));
			};
		}
	}
}
