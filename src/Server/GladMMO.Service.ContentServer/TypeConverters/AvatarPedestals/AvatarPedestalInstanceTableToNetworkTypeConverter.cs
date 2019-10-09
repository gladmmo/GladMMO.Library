using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GladMMO
{
	public sealed class AvatarPedestalInstanceTableToNetworkTypeConverter : ITypeConverterProvider<GameObjectAvatarPedestalEntryModel, AvatarPedestalInstanceModel>
	{
		public AvatarPedestalInstanceModel Convert([NotNull] GameObjectAvatarPedestalEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new AvatarPedestalInstanceModel(fromObject.TargetGameObjectId, (int)fromObject.AvatarModelId);
		}
	}
}
