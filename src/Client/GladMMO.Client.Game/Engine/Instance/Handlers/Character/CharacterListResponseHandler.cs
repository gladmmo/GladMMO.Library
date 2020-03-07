using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace FreecraftCore.Swarm
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CharacterListResponseHandler : BaseGameClientGameMessageHandler<CharacterListResponse>
	{
		/// <inheritdoc />
		public CharacterListResponseHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, CharacterListResponse payload)
		{
			if(Logger.IsErrorEnabled)
				Logger.Error("TODO: Implement character screen selection for character properly.");

			//TODO: Support character login request to character selection selected character.
			//Idea here is to just login to the first character.
			await context.PayloadSendService.SendMessage(new CharacterLoginRequest(payload.Characters.First().Data.CharacterGuid));
		}
	}
}