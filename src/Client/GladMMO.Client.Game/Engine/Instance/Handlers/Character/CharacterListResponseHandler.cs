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

		private INetworkTimeService TimeService { get; }

		/// <inheritdoc />
		public CharacterListResponseHandler(ILog logger, 
			[NotNull] ILocalCharacterDataRepository characterDataRepository,
			[NotNull] INetworkTimeService timeService)
			: base(logger)
		{
			CharacterDataRepository = characterDataRepository ?? throw new ArgumentNullException(nameof(characterDataRepository));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, CharacterListResponse payload)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Handling: {nameof(CharacterListResponse)}");

			//Not accurate to WoW client but the first packet we are going to send
			//is the packet that will get the server absolute timestamp.
			context.PayloadSendService.SendMessage(new CMSG_QUERY_TIME_Payload())
				.ConfigureAwaitFalseVoid();

			TimeService.RecalculateQueryTime();

			//TODO: Support character login request to character selection selected character.
			//Idea here is to just login to the first character.
			context.PayloadSendService.SendMessage(new CharacterLoginRequest(CharacterDataRepository.LocalCharacterGuid))
				.ConfigureAwaitFalseVoid();

			return Task.CompletedTask;
		}
	}
}