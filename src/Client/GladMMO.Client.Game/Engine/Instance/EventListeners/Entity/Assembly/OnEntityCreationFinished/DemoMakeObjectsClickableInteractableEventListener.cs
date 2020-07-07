using System; using FreecraftCore;
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
					//Assume this is at least a UNIT then.
					//Check if they are selectable
					if (IsSelectable(args))
					{
						SendService.SendMessage(new CMSG_SET_SELECTION_Payload(args.EntityGuid));
						//Client side prediction of player target
						LocalPlayerDetails.EntityData.SetFieldValue(EUnitFields.UNIT_FIELD_TARGET, args.EntityGuid);
					}
				}
				else
				{
					//We right CLICK woooo
					switch(args.EntityGuid.TypeId)
					{
						case EntityTypeId.TYPEID_OBJECT:
							break;
						case EntityTypeId.TYPEID_ITEM:
							break;
						case EntityTypeId.TYPEID_CONTAINER:
							break;
						case EntityTypeId.TYPEID_UNIT:
							//TODO: Only units with NPCFlags GOSSIP should be able to send a Gossip request. CHECK THESE!!
							SendService.SendMessage(new CMSG_GOSSIP_HELLO_Payload(args.EntityGuid));
							break;
						case EntityTypeId.TYPEID_PLAYER:
							break;
						case EntityTypeId.TYPEID_GAMEOBJECT:
							//TODO: Are there flags we should check??
							SendService.SendMessage(new CMSG_GAMEOBJ_USE_Payload(args.EntityGuid));
							break;
						case EntityTypeId.TYPEID_DYNAMICOBJECT:
							break;
						case EntityTypeId.TYPEID_CORPSE:
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}
			};
		}

		private bool IsSelectable(EntityCreationFinishedEventArgs args)
		{
			IEntityDataFieldContainer entity = EntityDataFieldMappable.RetrieveEntity(args.EntityGuid);

			//Game object uses DIFFERENT flags for selectability.
			if(args.EntityGuid.TypeId == EntityTypeId.TYPEID_GAMEOBJECT)
			{
				//TODO: More info is actually needed to determine if a GO is selectable. NOT_SELECTABLE isn't actually enough.
				return !entity.HasBaseGameObjectFieldFlag(GameObjectFlags.GO_FLAG_NOT_SELECTABLE);
			}
			else
			{
				//Assume it's a UNIT then.
				return !entity.HasBaseObjectFieldFlag(UnitFlags.UNIT_FLAG_NOT_SELECTABLE);
			}
		}
	}
}
