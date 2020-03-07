using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GladMMO
{
	public interface IOnHubConnectionEventListener
	{
		Task<HubOnConnectionState> OnConnected(Hub hubConnectedTo);
	}

	public enum HubOnConnectionState
	{
		/// <summary>
		/// Success indicates that the hub connection event listener was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Abort indicates that the event listener would like to request that the connection be aborted.
		/// </summary>
		Abort = 2,

		//TODO: We need error recovery somehow
		/// <summary>
		/// Error means a localized issue within the event listener occured but that
		/// the state of the listener should not affect globally the connection's service.
		/// </summary>
		Error = 3
	}
}
