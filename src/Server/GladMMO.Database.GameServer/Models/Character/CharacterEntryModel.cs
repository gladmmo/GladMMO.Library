using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GladMMO
{
	/// <summary>
	/// The model for the character database table.
	/// </summary>
	[Table("characters")]
	public class CharacterEntryModel
	{
		/// <summary>
		/// Primary key for the character.
		/// Must be unique.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Account ID associated with the character.
		/// </summary>
		[Required]
		public int AccountId { get; private set; }

		/// <summary>
		/// The name of the associated character.
		/// </summary>
		[Required]
		public string CharacterName { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		/// <inheritdoc />
		public CharacterEntryModel(int accountId, string characterName)
		{
			//Character name validation is handled externally
			if(accountId < 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));

			AccountId = accountId;
			CharacterName = characterName;
		}

		//Serializer ctor
		protected CharacterEntryModel()
		{
			
		}
	}
}
