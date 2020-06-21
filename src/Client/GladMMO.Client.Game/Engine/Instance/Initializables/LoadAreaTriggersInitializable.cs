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

		public LoadAreaTriggersInitializable([NotNull] IReadonlyZoneDataRepository zoneRepository,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
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
					collider.size = new Vector3(at.Radius, at.Radius, at.Radius);
				}
				else
				{
					collider.size = at.UnalignedBoxDimension.ToUnityVector();
				}

				trigger.OnTriggerEntered += (sender, args) => SendService.SendMessage(new CMSG_AREATRIGGER_Payload(at.EntryId));
			}
		}
	}
}
