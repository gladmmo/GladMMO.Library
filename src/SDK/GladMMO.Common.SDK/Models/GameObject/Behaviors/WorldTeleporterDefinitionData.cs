using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class WorldTeleporterDefinitionData : GladMMOSDKMonoBehaviour, INetworkGameObjectBehaviour, IRemoteModelUpdateable<WorldTeleporterInstanceModel>
	{
		public GameObjectType BehaviorType => GameObjectType.WorldTeleporter;

		[HideInInspector]
		[SerializeField]
		private int _TargetTeleportWorldId;

		[HideInInspector]
		[SerializeField]
		private PlayerStaticSpawnPointDefinition _LocalSpawnPointId;

		[HideInInspector]
		[SerializeField]
		private int _RemoteSpawnPointId;

		public PlayerStaticSpawnPointDefinition LocalSpawnPointId
		{
			get => _LocalSpawnPointId;
			set => _LocalSpawnPointId = value;
		}

		public int RemoteSpawnPointId
		{
			get => _RemoteSpawnPointId;
			set => _RemoteSpawnPointId = value;
		}

		public int TargetTeleportWorldId
		{
			get => _TargetTeleportWorldId;
			set => _TargetTeleportWorldId = value;
		}

		public void UpdateModel([NotNull] WorldTeleporterInstanceModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			LocalSpawnPointId = GameObject
				.FindObjectsOfType<PlayerStaticSpawnPointDefinition>()
				.First(sp => sp.PlayerSpawnPointId == model.LocalSpawnPointId);

			RemoteSpawnPointId = model.RemoteSpawnPointId;
		}
	}
}
