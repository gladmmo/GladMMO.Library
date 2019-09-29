using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	//Conceptually based on TrinityCore's (WoW's)
	[Table("gameobject_template")]
	public class GameObjectTemplateEntryModel : IDatabaseModelKeyable
	{
		[NotMapped]
		public int EntryKey => GameObjectTemplateId;

		/// <summary>
		/// The id of the GameObject template.
		/// </summary>
		[Column(Order = 1)]
		[Key]
		[Range(0, long.MaxValue)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GameObjectTemplateId { get; private set; }

		//TODO: Should we like tables somehow with auth? Probably not?
		/// <summary>
		/// Key for the account associated with the creation/registeration of this gameobject template.
		/// </summary>
		[Column(Order = 2)]
		[Required]
		[Range(0, int.MaxValue)]
		public int AccountId { get; private set; }

		/// <summary>
		/// The primary unique 64bit integer key used for the
		/// GameObject model's unique ID.
		/// </summary>
		[Column(Order = 3)]
		[ForeignKey(nameof(GameObjectModelEntry))]
		[Range(0, long.MaxValue)]
		public long ModelId { get; private set; }

		//Navigation property
		public virtual GameObjectModelEntryModel GameObjectModelEntry { get; private set; }

		/// <summary>
		/// The name of the GameObject. Will be the one the client sees, the one the NameQuery will return.
		/// </summary>
		[Column(Order = 4)]
		[Required]
		public string GameObjectName { get; private set; }

		/// <summary>
		/// Indicates the type of the game object.
		/// </summary>
		[Column(Order = 5)]
		[Required]
		public GameObjectType Type { get; private set; }

		public GameObjectTemplateEntryModel(int accountId, long modelId, [NotNull] string gameObjectName, GameObjectType type)
		{
			if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			if (modelId <= 0) throw new ArgumentOutOfRangeException(nameof(modelId));
			if (string.IsNullOrWhiteSpace(gameObjectName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(gameObjectName));
			if (!Enum.IsDefined(typeof(GameObjectType), type)) throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(GameObjectType));

			AccountId = accountId;
			ModelId = modelId;
			GameObjectName = gameObjectName;
			Type = type;
		}

		protected GameObjectTemplateEntryModel()
		{

		}
	}
}
