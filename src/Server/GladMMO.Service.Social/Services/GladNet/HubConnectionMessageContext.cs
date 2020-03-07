using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladNet;
using Microsoft.AspNetCore.SignalR;

namespace GladMMO
{
	public sealed class HubConnectionMessageContext<TRemoteClientHubType> : IPeerMessageContext<object>, IHubConnectionMessageContext<TRemoteClientHubType>
	{
		/// <inheritdoc />
		public IConnectionService ConnectionService { get; }

		//TODO: Initialize from creation or get from cache somehow
		/// <inheritdoc />
		public ObjectGuid CallerGuid => new ObjectGuidBuilder()
			.WithId(int.Parse(HubConntext.UserIdentifier))
			.WithType(EntityTypeId.TYPEID_PLAYER)
			.Build();

		/// <inheritdoc />
		public IPeerPayloadSendService<object> PayloadSendService { get; }

		/// <inheritdoc />
		public IPeerRequestSendService<object> RequestSendService { get; }

		public IGroupManager Groups { get; }

		public IHubCallerClients<TRemoteClientHubType> Clients { get; }

		public HubCallerContext HubConntext { get; }

		/// <inheritdoc />
		public HubConnectionMessageContext(
			[JetBrains.Annotations.NotNull] IConnectionService connectionService,
			[JetBrains.Annotations.NotNull] IPeerPayloadSendService<object> payloadSendService,
			[JetBrains.Annotations.NotNull] IPeerRequestSendService<object> requestSendService,
			[JetBrains.Annotations.NotNull] IGroupManager groups,
			[JetBrains.Annotations.NotNull] IHubCallerClients<TRemoteClientHubType> clients,
			[JetBrains.Annotations.NotNull] HubCallerContext hubConntext)
		{
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			PayloadSendService = payloadSendService ?? throw new ArgumentNullException(nameof(payloadSendService));
			RequestSendService = requestSendService ?? throw new ArgumentNullException(nameof(requestSendService));
			Groups = groups ?? throw new ArgumentNullException(nameof(groups));
			Clients = clients ?? throw new ArgumentNullException(nameof(clients));
			HubConntext = hubConntext ?? throw new ArgumentNullException(nameof(hubConntext));
		}
	}
}
