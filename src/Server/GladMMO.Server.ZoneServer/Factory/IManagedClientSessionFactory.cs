using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	/// <summary>
	/// Simplified type interface for the <see cref="IFactoryCreatable{TCreateType,TContextType}"/>
	/// for managed client sessions.
	/// </summary>
	public interface IManagedClientSessionFactory : IFactoryCreatable<ManagedClientSession<GameServerPacketPayload, GameClientPacketPayload>, ManagedClientSessionCreationContext>
	{

	}
}
