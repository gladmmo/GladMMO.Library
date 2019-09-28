using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	[Table("player_spawnpoint")]
	public class PlayerSpawnPointModel : IInstanceableWorldObjectModel, IModelEntryUpdateable<PlayerSpawnPointModel>
	{
		public int ObjectInstanceId => PlayerSpawnId;

		/// <summary>
		/// Primary key for the spawn entry.
		/// Must be unique.
		/// </summary>
		[Column(Order = 1)]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PlayerSpawnId { get; private set; }

		/// <summary>
		/// The spawn position of the player.
		/// </summary>
		[Column(Order = 2)]
		[Required]
		public Vector3<float> SpawnPosition { get; set; }

		/// <summary>
		/// The initial Y-axis orientation/rotation of the player when spawned.
		/// </summary>
		[Column(Order = 3)]
		[Required]
		public float InitialOrientation { get; set; }

		/// <summary>
		/// The map/zone that this spawn point exists in.
		/// (Each map/zone has an origin and the <see cref="SpawnPosition"/> is based on that).
		/// </summary>
		[Column(Order = 4)]
		[Required]
		[ForeignKey(nameof(WorldEntry))]
		public long WorldId { get; private set; }

		//Navigation property
		/// <summary>
		/// The NPC template.
		/// </summary>
		public virtual WorldEntryModel WorldEntry { get; private set; }

		/// <summary>
		/// Indicates if the spawn point is reserved for special actions.
		/// </summary>
		[Column(Order = 5)]
		[Required]
		public bool isReserved { get; private set; }

		public PlayerSpawnPointModel(Vector3<float> spawnPosition, float initialOrientation, long worldId, bool isReserved)
		{
			if (worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			SpawnPosition = spawnPosition;
			InitialOrientation = initialOrientation;
			WorldId = worldId;
			this.isReserved = isReserved;
		}

		/// <summary>
		/// EF constructor.
		/// </summary>
		private PlayerSpawnPointModel()
		{

		}

		public void Update([JetBrains.Annotations.NotNull] PlayerSpawnPointModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			//Don't let world id be updateable
			SpawnPosition = model.SpawnPosition;
			InitialOrientation = model.InitialOrientation;
			isReserved = model.isReserved;
		}
	}
}
