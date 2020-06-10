﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using GladMMO.Component;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	//TODO: Demo script.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DemoMakeObjectsClickableInteractableEventListener : EntityCreationFinishedEventListener
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyLocalPlayerDetails LocalPlayerDetails { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataFieldMappable { get; }

		public DemoMakeObjectsClickableInteractableEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyLocalPlayerDetails localPlayerDetails,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataFieldMappable) 
			: base(subscriptionService)
		{
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			LocalPlayerDetails = localPlayerDetails ?? throw new ArgumentNullException(nameof(localPlayerDetails));
			EntityDataFieldMappable = entityDataFieldMappable ?? throw new ArgumentNullException(nameof(entityDataFieldMappable));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			//TODO: left over from bad desigh.
		}

		protected override void OnEventFired(object source, EntityCreationFinishedEventArgs args)
		{
			//Only world objects
			if(!args.EntityGuid.IsWorldObject())
				return;

			GameObject entity = GameObjectMappable.RetrieveEntity(args.EntityGuid);

			//Adding a test/demo collider to allow for a clicking volume.
			BoxCollider collider = entity.AddComponent<BoxCollider>();
			collider.isTrigger = true;
			entity.AddComponent<OnMouseClickedComponent>().OnMouseClicked += (sender, eventArgs) =>
			{
				if (eventArgs.Type == MouseButtonClickEventArgs.MouseType.Left)
				{
					//Check if they are selectable
					if (!EntityDataFieldMappable.RetrieveEntity(args.EntityGuid).HasBaseObjectFieldFlag(UnitFlags.UNIT_FLAG_NOT_SELECTABLE))
					{
						SendService.SendMessage(new CMSG_SET_SELECTION_Payload(args.EntityGuid));
						//Client side prediction of player target
						LocalPlayerDetails.EntityData.SetFieldValue(EUnitFields.UNIT_FIELD_TARGET, args.EntityGuid);
					}
				}
				else
				{
					//Check if the entity is interactable before sending a packet.
					/*if (EntityDataFieldMappable.RetrieveEntity(args.EntityGuid).HasBaseObjectFieldFlag(UnitFlags.UNIT_FLAG_INTERACTABLE))
					{
						SendService.SendMessage(new ClientInteractNetworkedObjectRequestPayload(args.EntityGuid, ClientInteractNetworkedObjectRequestPayload.InteractType.Interaction));
					}*/
				}
			};
		}
	}
}
