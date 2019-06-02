using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	/// <summary>
	/// Simplified type interface for the <see cref="IFactoryCreatable{TCreateType,TContextType}"/>
	/// for managed sessions.
	/// </summary>
	public interface IManagedSessionFactory : IFactoryCreatable<IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload>, ManagedSessionCreationContext>
	{
		
	}
}
