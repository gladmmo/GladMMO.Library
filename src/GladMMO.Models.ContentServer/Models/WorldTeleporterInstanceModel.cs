using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	[JsonObject]
	public class WorldTeleporterInstanceModel : IGameObjectLinkable
	{
		//TODO: Combine the TargetGameObjectId and the LinkedGameObjectId. They are conceptually the same, legacy stuff.
		[JsonIgnore]
		public int LinkedGameObjectId => TargetGameObjectId;

		/// <summary>
		/// Defines the GameObject instance that
		/// this behavior is attached to. It is the primary
		/// and foreign key to the instance it's attached to.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public int TargetGameObjectId { get; private set; }

		/// <summary>
		/// The spawn point local to the world the TargetGameObject
		/// is within. It's the spawn point for players using this World Teleporter
		/// should spawn at.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public int LocalSpawnPointId { get; private set; }

		/// <summary>
		/// The spawn point in the remote world the the player should spawn at.
		/// It's the spawn point for players when they end up in the linked world.
		/// should spawn at.
		/// </summary>
		[JsonRequired]
		[JsonProperty]
		public int RemoteSpawnPointId { get; private set; }

		public WorldTeleporterInstanceModel(int targetGameObjectId, int localSpawnPointId, int remoteSpawnPointId)
		{
			if(targetGameObjectId <= 0) throw new ArgumentOutOfRangeException(nameof(targetGameObjectId));
			if(localSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(localSpawnPointId));
			if(remoteSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(remoteSpawnPointId));

			TargetGameObjectId = targetGameObjectId;
			LocalSpawnPointId = localSpawnPointId;
			RemoteSpawnPointId = remoteSpawnPointId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		protected WorldTeleporterInstanceModel()
		{

		}
	}
}
