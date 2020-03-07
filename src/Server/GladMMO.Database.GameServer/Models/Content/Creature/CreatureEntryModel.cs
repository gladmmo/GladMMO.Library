﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	/// <summary>
	/// Table model that represents an instance of a <see cref="CreatureTemplateEntryModel"/>.
	/// Represents an actual Creature entry that exists in the world.
	/// </summary>
	[Table("creature")]
	public class CreatureEntryModel : IInstanceableWorldObjectModel, IModelEntryUpdateable<CreatureEntryModel>
	{
		public int ObjectInstanceId => CreatureEntryId;

		/// <summary>
		/// Primary key for the Creature entry.
		/// Must be unique.
		/// </summary>
		[Column(Order = 1)]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CreatureEntryId { get; private set; }

		/// <summary>
		/// Defines the template data from which this entry/instance of a creature
		/// should be created from.
		/// </summary>
		[Column(Order = 2)]
		[Required]
		[ForeignKey(nameof(CreatureTemplate))]
		public int CreatureTemplateId { get; set; }

		//Navigation property
		/// <summary>
		/// The NPC template.
		/// </summary>
		public virtual CreatureTemplateEntryModel CreatureTemplate { get; private set; }

		/// <summary>
		/// The spawn position of the Creature.
		/// </summary>
		[Column(Order = 3)]
		[Required]
		public GladMMO.Database.Vector3<float> SpawnPosition { get; set; }

		/// <summary>
		/// The initial Y-axis orientation/rotation of the Creature when spawned.
		/// Especially important for stationary Creatures.
		/// </summary>
		[Column(Order = 4)]
		[Required]
		public float InitialOrientation { get; set; }

		/// <summary>
		/// The map/zone that this creature exists in.
		/// (Each map/zone has an origin and the <see cref="SpawnPosition"/> is based on that).
		/// </summary>
		[Column(Order = 5)]
		[Required]
		[ForeignKey(nameof(WorldEntry))]
		public long WorldId { get; private set; }

		//Navigation property
		/// <summary>
		/// The NPC template.
		/// </summary>
		public virtual WorldEntryModel WorldEntry { get; private set; }

		/// <inheritdoc />
		public CreatureEntryModel(int npcTemplateId, GladMMO.Database.Vector3<float> spawnPosition, float initialOrientation, long worldId)
		{
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));
			if(npcTemplateId <= 0) throw new ArgumentOutOfRangeException(nameof(npcTemplateId));

			CreatureTemplateId = npcTemplateId;
			SpawnPosition = spawnPosition ?? throw new ArgumentNullException(nameof(spawnPosition));
			WorldId = worldId;
			InitialOrientation = initialOrientation;
		}

		/// <summary>
		/// EF constructor.
		/// </summary>
		private CreatureEntryModel()
		{
			
		}

		public void Update(CreatureEntryModel model)
		{
			CreatureTemplateId = model.CreatureTemplateId;
			InitialOrientation = model.InitialOrientation;
			SpawnPosition = model.SpawnPosition;
		}
	}
}
