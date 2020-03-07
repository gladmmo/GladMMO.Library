using System; using FreecraftCore;
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
		/// The PlayFabId associated with the character.
		/// </summary>
		[Required]
		public string PlayFabId { get; private set; }

		[Required]
		public string PlayFabCharacterId { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		/// <inheritdoc />
		public CharacterEntryModel(int accountId,
			[NotNull] string characterName, 
			[NotNull] string playFabId = "UNKNOWN",
			[NotNull] string playFabCharacterId = "UNKNOWN")
		{
			//Character name validation is handled externally
			if(accountId < 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));
			if (string.IsNullOrWhiteSpace(playFabId)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(playFabId));
			if (string.IsNullOrWhiteSpace(playFabCharacterId)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(playFabCharacterId));

			AccountId = accountId;
			CharacterName = characterName;
			PlayFabCharacterId = playFabCharacterId;
			PlayFabId = playFabId;
		}

		//Serializer ctor
		protected CharacterEntryModel()
		{
			
		}
	}
}
