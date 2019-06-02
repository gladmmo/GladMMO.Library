using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public sealed class MockedPayloadInterceptorService : IPeerRequestSendService<GameServerPacketPayload>
	{
		/// <inheritdoc />
		public Task<TResponseType> SendRequestAsync<TResponseType>(GameServerPacketPayload request, DeliveryMethod method = DeliveryMethod.ReliableOrdered, CancellationToken cancellationToken = new CancellationToken())
		{
			throw new NotSupportedException($"Servers do not support {nameof(IPeerRequestSendService<GameServerPacketPayload>)}");
		}
	}
}
