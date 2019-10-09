using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	[CustomEditor(typeof(AvatarPedestalDefinitionData))]
	public sealed class AvatarPedestalDefinitionTypeDrawer : BaseBehaviourDefinitionTypeDrawer<AvatarPedestalDefinitionData, AvatarPedestalInstanceModel>
	{

		public AvatarPedestalDefinitionTypeDrawer() 
			: base(new AvatarPedestalBehaviourToTransportModelTypeConverter(), new AvatarPedestalContentServiceClientFactory())
		{

		}

		protected override void GatherConfigurableData([NotNull] AvatarPedestalDefinitionData target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));

			target.TargetAvatarModelId = EditorGUILayout.IntField("Avatar Model Id:", GetTarget().TargetAvatarModelId);
		}
	}
}
