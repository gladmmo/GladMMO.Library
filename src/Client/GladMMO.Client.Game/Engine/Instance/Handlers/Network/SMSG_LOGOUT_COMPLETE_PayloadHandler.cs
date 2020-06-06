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
	public sealed class SMSG_LOGOUT_COMPLETE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_LOGOUT_COMPLETE_Payload>
	{
		private IInstanceLogoutEventPublisher LogoutEventPublisher { get; }

		/// <inheritdoc />
		public SMSG_LOGOUT_COMPLETE_PayloadHandler(ILog logger,
			[NotNull] IInstanceLogoutEventPublisher logoutEventPublisher)
			: base(logger)
		{
			LogoutEventPublisher = logoutEventPublisher ?? throw new ArgumentNullException(nameof(logoutEventPublisher));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_LOGOUT_COMPLETE_Payload payload)
		{
			LogoutEventPublisher.PublishEvent(this, EventArgs.Empty);
			return Task.CompletedTask;
		}
	}
}