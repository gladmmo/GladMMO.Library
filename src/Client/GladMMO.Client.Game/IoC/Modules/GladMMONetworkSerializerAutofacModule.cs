using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FreecraftCore.Serializer;
using GladNet;

namespace GladMMO
{
	public sealed class GladMMONetworkSerializerAutofacModule : Module
	{
		public GladMMONetworkSerializerAutofacModule()
		{

		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SerializerService>()
				.AsSelf()
				.As<ISerializerService>()
				.OnActivated(args =>
				{
					//TODO: Automate discovery of payload types.
					args.Instance.RegisterType<GamePacketPayload>();
					args.Instance.RegisterType<ServerPacketHeader>();
					args.Instance.RegisterType<OutgoingClientPacketHeader>();
					args.Instance.RegisterType<SessionAuthChallengeEvent>();
					args.Instance.RegisterType<SessionAuthProofRequest>();
					args.Instance.RegisterType<AuthenticateSessionResponse>();
					args.Instance.RegisterType<CharacterLoginRequest>();
					args.Instance.RegisterType<CharacterListRequest>();

					//We don't really need this packet but we need it for normal TC login flow.
					args.Instance.RegisterType<FreecraftCore.CharacterListResponse>();
					args.Instance.RegisterType<SMSG_LOGIN_VERIFY_WORLD_PAYLOAD>();
					args.Instance.RegisterType<SMSG_COMPRESSED_UPDATE_OBJECT_Payload>();
					args.Instance.RegisterType<ChatMessageRequest>();
					args.Instance.RegisterType<ClientGroupInviteRequest>();
					args.Instance.RegisterType<ServerPartyCommandResultResponse>();
					args.Instance.RegisterType<ServerGroupListEvent>();
					args.Instance.RegisterType<MSG_MOVE_HEARTBEAT_Payload>();

					args.Instance.RegisterType<MSG_MOVE_START_FORWARD_Payload>();
					args.Instance.RegisterType<MSG_MOVE_STOP_Payload>();
					args.Instance.RegisterType<MSG_MOVE_START_STRAFE_LEFT_Payload>();
					args.Instance.RegisterType<MSG_MOVE_START_STRAFE_RIGHT_Payload>();
					args.Instance.RegisterType<MSG_MOVE_HEARTBEAT_Payload>();

					args.Instance.Compile();
				})
				.SingleInstance();

			builder.RegisterType<FreecraftCoreGladNetSerializerAdapter>()
				.AsSelf()
				.As<INetworkSerializationService>()
				.SingleInstance();
		}
	}
}