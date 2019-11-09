using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class NetworkEntityGuidQueue : HashSet<NetworkEntityGuid>, IDequeable<NetworkEntityGuid>
	{
		/// <inheritdoc />
		public bool isEmpty => Count == 0;

		public NetworkEntityGuidQueue()
			: base(NetworkGuidEqualityComparer<NetworkEntityGuid>.Instance)
		{

		}
	}
}
