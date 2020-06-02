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
		private ILocalCharacterDataRepository CharacterDataRepository { get; }

		/// <inheritdoc />
		public CharacterListResponseHandler(ILog logger, [NotNull] ILocalCharacterDataRepository characterDataRepository)
			: base(logger)
		{
			CharacterDataRepository = characterDataRepository ?? throw new ArgumentNullException(nameof(characterDataRepository));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, CharacterListResponse payload)
		{
			//TODO: Support character login request to character selection selected character.
			//Idea here is to just login to the first character.
			await context.PayloadSendService.SendMessage(new CharacterLoginRequest(CharacterDataRepository.LocalCharacterGuid));
		}
	}
}