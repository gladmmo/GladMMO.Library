using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IEntityGuidMappable<TValue> : Glader.Essentials.IEntityGuidMappable<NetworkEntityGuid, TValue>, IEntityCollectionRemovable
	{
		
	}
}
