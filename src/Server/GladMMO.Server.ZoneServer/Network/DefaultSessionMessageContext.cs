using System;
using System.Collections.Generic;
using System.Text;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class DefaultSessionMessageContext<TPayloadWriteType> : IPeerSessionMessageContext<TPayloadWriteType> 
		where TPayloadWriteType : class
	{
		/// <inheritdoc />
		public IConnectionService ConnectionService { get; }

		/// <inheritdoc />
		public IPeerPayloadSendService<TPayloadWriteType> PayloadSendService { get; }

		/// <inheritdoc />
		public IPeerRequestSendService<TPayloadWriteType> RequestSendService { get; }

		/// <inheritdoc />
		public SessionDetails Details { get; }

		/// <inheritdoc />
		public DefaultSessionMessageContext([NotNull] IConnectionService connectionService, [NotNull] IPeerPayloadSendService<TPayloadWriteType> payloadSendService, [NotNull] IPeerRequestSendService<TPayloadWriteType> requestSendService, [NotNull] SessionDetails details)
		{
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			PayloadSendService = payloadSendService ?? throw new ArgumentNullException(nameof(payloadSendService));
			RequestSendService = requestSendService ?? throw new ArgumentNullException(nameof(requestSendService));
			Details = details ?? throw new ArgumentNullException(nameof(details));
		}
	}
}
