using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public interface INetworkClientManager : INetworkClientManager<GamePacketPayload, GamePacketPayload>
	{

	}

	public interface INetworkClientManager<TIncomingPayloadType, out TOutgoingPayloadType>
		where TOutgoingPayloadType : class
		where TIncomingPayloadType : class
	{
		bool isNetworkHandling { get; }

		Task StartHandlingNetworkClient([NotNull] IManagedNetworkClient<TOutgoingPayloadType, TIncomingPayloadType> client);

		Task StopHandlingNetworkClient(bool handleImmediately = false, bool silent = false);
	}
}
