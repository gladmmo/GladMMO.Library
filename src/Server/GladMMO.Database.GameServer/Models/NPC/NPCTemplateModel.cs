using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// The model for the NPC main database table.
	/// (Inspired by Trinitycore creature template table)
	/// </summary>
	[Table("npc_template")]
	public sealed class NPCTemplateModel
	{
		/// <summary>
		/// Primary key for the NPC template.
		/// Must be unique.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TemplateId { get; private set; }

		//TODO: Should we maintain a prefab table?
		/// <summary>
		/// Represents the visual component of the NPC prefab.
		/// (NPCs have a base prefab and this will point to their visible prefab)
		/// </summary>
		[Required]
		public int PrefabId { get; private set; }

		/// <summary>
		/// The name of the NPC. Will be the one the client sees, the one the NameQuery will return.
		/// </summary>
		[Required]
		public string NpcName { get; private set; }

		/// <inheritdoc />
		public NPCTemplateModel(int prefabId, string npcName)
		{
			if(prefabId <= 0) throw new ArgumentOutOfRangeException(nameof(prefabId));
			PrefabId = prefabId;
			NpcName = npcName ?? throw new ArgumentNullException(nameof(npcName));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected NPCTemplateModel()
		{
			
		}
	}
}
