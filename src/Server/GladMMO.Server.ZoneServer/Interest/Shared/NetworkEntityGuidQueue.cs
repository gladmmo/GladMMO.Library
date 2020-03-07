using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class ObjectGuidQueue : HashSet<ObjectGuid>, IDequeable<ObjectGuid>
	{
		/// <inheritdoc />
		public bool isEmpty => Count == 0;

		public ObjectGuidQueue()
			: base(NetworkGuidEqualityComparer<ObjectGuid>.Instance)
		{

		}
	}
}
