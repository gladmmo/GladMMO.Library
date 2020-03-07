﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IEntityGuidMappable<TValue> : IReadonlyEntityGuidMappable<TValue>, Glader.Essentials.IEntityGuidMappable<ObjectGuid, TValue>, IEntityCollectionRemovable
	{
		
	}
}
