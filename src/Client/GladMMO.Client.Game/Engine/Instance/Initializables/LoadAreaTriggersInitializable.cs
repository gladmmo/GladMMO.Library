using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LoadAreaTriggersInitializable : IGameInitializable
	{
		private IReadonlyZoneDataRepository ZoneRepository { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public LoadAreaTriggersInitializable([NotNull] IReadonlyZoneDataRepository zoneRepository,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		public async Task OnGameInitialized()
		{
			//Get to main thread.
			await new UnityYieldAwaitable();

			//Get the AT prefab
			GameObject areaTriggerPrefab = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("AreaTrigger").Task;

			foreach (AreaTriggerEntry at in ClientData.Enumerate<AreaTriggerEntry>())
			{
				//Only ATs in our map.
				if (ZoneRepository.ZoneId != at.MapId)
					continue;

				GameObject areaTriggerInstance = GameObject.Instantiate(areaTriggerPrefab, at.Position.ToUnityVector(), Quaternion.Euler(0.0f, at.Orientation.ToUnity3DYAxisRotation(), 0.0f));
				areaTriggerInstance.name = $"AreaTrigger_{at.EntryId}";

				TriggerCallbackComponent trigger = areaTriggerInstance.GetComponent<TriggerCallbackComponent>();

				if (at.isAxisAlignedBox)
				{
					//AreaTrigger Blizzlike sometimes uses negative values for AT
					float radius = Math.Abs(at.Radius);
					trigger.Collider.size = new Vector3(radius, radius, radius);
				}
				else
				{
					//AreaTrigger Blizzlike sometimes uses negative values for AT
					Vector3 vector = at.UnalignedBoxDimension.ToUnityVector();
					trigger.Collider.size = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
				}

				//WARNING: NEVER DELETE THIS VALUABLE REVERSE ENGINEERING INFORMATION
				/*Found some undocumented client behavior. Before the client sends a CMSG_AREATRIGGER packet to the server it will send a MSG_MOVE_HEARTBEAT.
				  I suspected this to be the case today when I realized there wasn't any way for TC to predict positions in the middle of movement during Area Trigger handling, and it has a STRICT position requirement. 
				  The client will send MSG_MOVE_HEARTBEAT right before sending CMSG_AREATRIGGER packet, regardless of how longer the client has been moving. 
				  Even for a split second of movement. 
				  I confirmed this via debugging.*/

				/*Can also confirm that it doesn't send a typical MSG_MOVE_HEARTBEAT. It's a special one, though TrinityCore does not note this nor handle it differently. It sends no movement flags in this MSG_MOVE_HEART which normally happens. Seen here, and confirmed via debugging:*/
				//https://i.imgur.com/NMNS38G.png

				//So, due to this reverse engineered finding we must send move heartbeat first.
				trigger.OnTriggerEntered += (sender, args) =>
				{
					SendService.SendMessage(new MSG_MOVE_HEARTBEAT_Payload(PlayerDetails.LocalPlayerGuid, BuildAreaTriggerMovementInfo(args)));
					SendService.SendMessage(new CMSG_AREATRIGGER_Payload(at.EntryId));
				};
			}
		}

		private MovementInfo BuildAreaTriggerMovementInfo(Transform args)
		{
			//TODO: Use WorldTransform instead of actual Unity3D transform position.
			Vector3 position = args.position;

			//TODO: Use correct timestamp.
			//TODO: Fix Area Trigger rotation.
			return new MovementInfo(MovementFlag.MOVEMENTFLAG_NONE, MovementFlagExtra.None, (uint)Environment.TickCount, position.ToWoWVector(), 0.0f, null, 0, 0, 0, null, 0);
		}
	}
}
