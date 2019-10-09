using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.SDK
{
	public sealed class AvatarPedestalBehaviourToTransportModelTypeConverter : ITypeConverterProvider<AvatarPedestalDefinitionData, AvatarPedestalInstanceModel>
	{
		public AvatarPedestalInstanceModel Convert([NotNull] AvatarPedestalDefinitionData fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new AvatarPedestalInstanceModel(fromObject.gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId, fromObject.TargetAvatarModelId);
		}
	}
}
