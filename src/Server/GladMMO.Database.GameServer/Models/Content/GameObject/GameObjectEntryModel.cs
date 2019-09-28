using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	/// <summary>
	/// Table model that represents an instance of a <see cref="GameObjectTemplateEntryModel"/>.
	/// Represents an actual GameObject entry that exists in the world.
	/// </summary>
	[Table("gameobject")]
	public class GameObjectEntryModel : IInstanceableWorldObjectModel, IModelEntryUpdateable<GameObjectEntryModel>
	{
		public int ObjectInstanceId => GameObjectEntryId;

		/// <summary>
		/// Primary key for the GameObject entry.
		/// Must be unique.
		/// </summary>
		[Column(Order = 1)]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GameObjectEntryId { get; private set; }

		/// <summary>
		/// Defines the template data from which this entry/instance of a GameObject
		/// should be created from.
		/// </summary>
		[Column(Order = 2)]
		[Required]
		[ForeignKey(nameof(GameObjectTemplate))]
		public int GameObjectTemplateId { get; set; }

		//Navigation property
		/// <summary>
		/// The GameObject template.
		/// </summary>
		public virtual CreatureTemplateEntryModel GameObjectTemplate { get; private set; }

		/// <summary>
		/// The spawn position of the GameObject.
		/// </summary>
		[Column(Order = 3)]
		[Required]
		public Vector3<float> SpawnPosition { get; set; }

		/// <summary>
		/// The initial Y-axis orientation/rotation of the GameObject when spawned.
		/// Especially important for stationary GameObjects.
		/// </summary>
		[Column(Order = 4)]
		[Required]
		public float InitialOrientation { get; set; }

		/// <summary>
		/// The map/zone that this GameObject exists in.
		/// (Each map/zone has an origin and the <see cref="SpawnPosition"/> is based on that).
		/// </summary>
		[Column(Order = 5)]
		[Required]
		[ForeignKey(nameof(WorldEntry))]
		public long WorldId { get; private set; }

		//Navigation property
		/// <summary>
		/// The World entry this instance of a GameObject is located within.
		/// </summary>
		public virtual WorldEntryModel WorldEntry { get; private set; }

		/// <inheritdoc />
		public GameObjectEntryModel(int npcTemplateId, Vector3<float> spawnPosition, float initialOrientation, long worldId)
		{
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));
			if(npcTemplateId <= 0) throw new ArgumentOutOfRangeException(nameof(npcTemplateId));

			GameObjectTemplateId = npcTemplateId;
			SpawnPosition = spawnPosition ?? throw new ArgumentNullException(nameof(spawnPosition));
			WorldId = worldId;
			InitialOrientation = initialOrientation;
		}

		/// <summary>
		/// EF constructor.
		/// </summary>
		private GameObjectEntryModel()
		{
			
		}

		public void Update(GameObjectEntryModel model)
		{
			GameObjectTemplateId = model.GameObjectTemplateId;
			InitialOrientation = model.InitialOrientation;
			SpawnPosition = model.SpawnPosition;
		}
	}
}
