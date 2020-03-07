using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GladMMO
{
	public static class SignalRCallerHubExtensions
	{
		/// <summary>
		/// Gets the <typeparamref name="TRemoteClientHubInterfaceType"/> associated with the provided entity guid <see cref="clientGuid"/>.
		/// </summary>
		/// <typeparam name="TRemoteClientHubInterfaceType">The remote interface type.</typeparam>
		/// <param name="clientGroup">The client group.</param>
		/// <param name="clientGuid">The entity.</param>
		/// <returns>The remote client RPC interface.</returns>
		public static TRemoteClientHubInterfaceType RetrievePlayerClient<TRemoteClientHubInterfaceType>(this IHubCallerClients<TRemoteClientHubInterfaceType> clientGroup, ObjectGuid clientGuid)
		{
			return clientGroup.User(clientGuid.CurrentObjectGuid.ToString());
		}
	}
}
