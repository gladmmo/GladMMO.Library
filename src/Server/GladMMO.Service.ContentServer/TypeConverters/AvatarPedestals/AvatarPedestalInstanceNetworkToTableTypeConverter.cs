using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using UnityEngine;

namespace GladMMO
{
	
	public sealed class AvatarPedestalInstanceNetworkToTableTypeConverter : ITypeConverterProvider<AvatarPedestalInstanceModel, GameObjectAvatarPedestalEntryModel>
	{
		public GameObjectAvatarPedestalEntryModel Convert([NotNull] AvatarPedestalInstanceModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new GameObjectAvatarPedestalEntryModel(fromObject.TargetGameObjectId, fromObject.AvatarModelId);
		}
	}
}
