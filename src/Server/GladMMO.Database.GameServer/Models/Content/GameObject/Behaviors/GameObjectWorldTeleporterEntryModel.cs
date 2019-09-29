using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("gameobject_worldteleporter")]
	public class GameObjectWorldTeleporterEntryModel
	{
		/// <summary>
		/// Defines the <see cref="GameObjectEntryModel"/> instance that
		/// this behavior is attached to. It is the primary
		/// and foreign key to the instance it's attached to.
		/// </summary>
		[Key]
		[Column(Order = 1)]
		[Required]
		[ForeignKey(nameof(TargetGameObject))]
		public int TargetGameObjectId { get; set; }

		//Navigation property
		/// <summary>
		/// The GameObject instance the world teleport behavior is attached to.
		/// </summary>
		public virtual GameObjectEntryModel TargetGameObject { get; private set; }

		/// <summary>
		/// The spawn point local to the world the <see cref="TargetGameObject"/>
		/// is within. It's the spawn point for players using this World Teleporter
		/// should spawn at.
		/// </summary>
		[Required]
		[Column(Order = 2)]
		[ForeignKey(nameof(LocalSpawnPoint))]
		public int LocalSpawnPointId { get; set; }

		//Navigation property
		/// <summary>
		/// The local spawn point entry that people using this teleporter
		/// will spawn at.
		/// </summary>
		public virtual PlayerSpawnPointEntryModel LocalSpawnPoint { get; private set; }

		/// <summary>
		/// The spawn point in the remote world the the player should spawn at.
		/// It's the spawn point for players when they end up in the linked world.
		/// should spawn at.
		/// </summary>
		[CanBeNull]
		[Column(Order = 3)]
		[ForeignKey(nameof(RemoteSpawnPoint))]
		public int RemoteSpawnPointId { get; set; }

		//Navigation property
		/// <summary>
		/// The linked teleporter
		/// </summary>
		public virtual PlayerSpawnPointEntryModel RemoteSpawnPoint { get; private set; }

		public GameObjectWorldTeleporterEntryModel(int targetGameObjectId, int localSpawnPointId, int remoteSpawnPointId)
		{
			if (targetGameObjectId <= 0) throw new ArgumentOutOfRangeException(nameof(targetGameObjectId));
			if (localSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(localSpawnPointId));
			if (remoteSpawnPointId <= 0) throw new ArgumentOutOfRangeException(nameof(remoteSpawnPointId));

			TargetGameObjectId = targetGameObjectId;
			LocalSpawnPointId = localSpawnPointId;
			RemoteSpawnPointId = remoteSpawnPointId;
		}

		/// <summary>
		/// EF ctor.
		/// </summary>
		protected GameObjectWorldTeleporterEntryModel()
		{
			
		}
	}
}
