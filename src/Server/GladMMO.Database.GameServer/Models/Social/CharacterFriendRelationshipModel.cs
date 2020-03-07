using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Database table model for friend requests.
	/// </summary>
	[Table("character_friendrelationship")]
	public class CharacterFriendRelationshipModel
	{
		/// <summary>
		/// The primary key
		/// </summary>
		[Key]
		public int FriendshipRelationshipId { get; private set; }

		/// <summary>
		/// The ID of the character requesting the friend request.
		/// </summary>
		[ForeignKey(nameof(RequestingCharacter))]
		public int RequestingCharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel RequestingCharacter { get; private set; }

		/// <summary>
		/// The ID of the target for the friend request.
		/// </summary>
		[ForeignKey(nameof(TargetRequestCharacter))]
		public int TargetRequestCharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel TargetRequestCharacter { get; private set; }

		/// <summary>
		/// The creation time of the friendship.
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		/// <summary>
		/// This column exists ONLY for uniqueness enforcement
		/// so that a request cannot be sent from N to M
		/// and from M to N. Only 1 request between two entities can exist.
		/// </summary>
		internal long DirectionalUniqueness { get; private set; }

		/// <summary>
		/// The state of the relation.
		/// See doc in <see cref="FriendshipRelationshipState"/> to understand the meaning.
		/// </summary>
		[Required]
		public FriendshipRelationshipState RelationState { get; private set; }

		/// <inheritdoc />
		public CharacterFriendRelationshipModel(int requestingCharacterId, int targetRequestCharacterId)
			: this(requestingCharacterId, targetRequestCharacterId, FriendshipRelationshipState.Pending)
		{

		}

		/// <inheritdoc />
		public CharacterFriendRelationshipModel(int requestingCharacterId, int targetRequestCharacterId, FriendshipRelationshipState relationState)
		{
			if(requestingCharacterId <= 0) throw new ArgumentOutOfRangeException(nameof(requestingCharacterId));
			if(targetRequestCharacterId <= 0) throw new ArgumentOutOfRangeException(nameof(targetRequestCharacterId));

			if(requestingCharacterId == targetRequestCharacterId)
				throw new ArgumentException($"Provided arguments: {nameof(requestingCharacterId)} and {nameof(targetRequestCharacterId)} must be unique.");
			if(!Enum.IsDefined(typeof(FriendshipRelationshipState), relationState)) throw new InvalidEnumArgumentException(nameof(relationState), (int)relationState, typeof(FriendshipRelationshipState));

			RequestingCharacterId = requestingCharacterId;
			TargetRequestCharacterId = targetRequestCharacterId;

			//Directional uniqueness MUST be computed only after sorting to ensure unique column given integers N and M
			DirectionalUniqueness = ComputeDirectionalUniquenessIndex(requestingCharacterId, targetRequestCharacterId);
			RelationState = relationState;
		}

		/// <summary>
		/// Computes the 8 byte identifier for the two entity's
		/// entering a relationship <see cref="RequestingCharacterId"/> and
		/// <see cref="TargetRequestCharacterId"/>. This will produce a unique
		/// value for the relationship that will be the same regardless of which id is either.
		/// This ID can be searched for to check if a request or relationship already exists eithout checking both orders.
		/// It will also help prevent race conditions of multiple requests sent by throwing if one exists.
		/// </summary>
		/// <param name="requestingCharacterId"></param>
		/// <param name="targetRequestCharacterId"></param>
		/// <returns></returns>
		public static long ComputeDirectionalUniquenessIndex(int requestingCharacterId, int targetRequestCharacterId)
		{
			return (long)Math.Min(requestingCharacterId, targetRequestCharacterId) + ((long)Math.Max(requestingCharacterId, targetRequestCharacterId) << 32);
		}
	}
}
