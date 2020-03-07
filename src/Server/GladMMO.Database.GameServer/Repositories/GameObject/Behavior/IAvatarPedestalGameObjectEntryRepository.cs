﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IAvatarPedestalGameObjectEntryRepository : IGenericRepositoryCrudable<int, GameObjectAvatarPedestalEntryModel>, IInstanceableWorldObjectRepository<GameObjectAvatarPedestalEntryModel>
	{

	}
}
