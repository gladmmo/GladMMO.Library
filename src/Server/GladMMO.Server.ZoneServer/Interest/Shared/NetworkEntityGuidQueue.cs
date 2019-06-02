using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class NetworkEntityGuidQueue : Queue<NetworkEntityGuid>, IDequeable<NetworkEntityGuid>
	{
		/// <inheritdoc />
		public bool isEmpty => Count == 0;

		public NetworkEntityGuidQueue()
		{

		}

		public NetworkEntityGuidQueue(IEnumerable<NetworkEntityGuid> collection) 
			: base(collection)
		{

		}

		public NetworkEntityGuidQueue(int capacity) 
			: base(capacity)
		{

		}
	}
}
