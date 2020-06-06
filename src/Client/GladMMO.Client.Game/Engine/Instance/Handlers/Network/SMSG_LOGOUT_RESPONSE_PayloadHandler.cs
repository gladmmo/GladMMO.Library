using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_LOGOUT_RESPONSE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_LOGOUT_RESPONSE_Payload>
	{
		private IInstanceLogoutEventPublisher LogoutEventPublisher { get; }

		/// <inheritdoc />
		public SMSG_LOGOUT_RESPONSE_PayloadHandler(ILog logger,
			[NotNull] IInstanceLogoutEventPublisher logoutEventPublisher)
			: base(logger)
		{
			LogoutEventPublisher = logoutEventPublisher ?? throw new ArgumentNullException(nameof(logoutEventPublisher));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_LOGOUT_RESPONSE_Payload payload)
		{
			if (payload.Result == LogoutResultCode.Success)
				if (payload.IsInstant)
					LogoutEventPublisher.PublishEvent(this, EventArgs.Empty);

			//Server will send a SMSG_LOGOUT_COMPLETE if it's not instant.
			return Task.CompletedTask;
		}
	}
}