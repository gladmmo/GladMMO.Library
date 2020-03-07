using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DebugTestModelChangeRequest : IGameTickable
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		public DebugTestModelChangeRequest([NotNull] IPeerPayloadSendService<GamePacketPayload> sendService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		public void Tick()
		{
			//if (Input.GetKeyDown(KeyCode.H))
			//	SendService.SendMessage(new PlayerModelChangeRequestPayload(6));

			//if(Input.GetKeyDown(KeyCode.J))
			//	SendService.SendMessage(new PlayerModelChangeRequestPayload(7));
		}
	}
}
