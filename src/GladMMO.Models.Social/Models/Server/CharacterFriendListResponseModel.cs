using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterFriendListResponseModel : BaseSocialModel
	{
		[JsonProperty]
		private readonly int[] _CharacterFriendsId;

		[JsonIgnore]
		public IReadOnlyCollection<int> CharacterFriendsId => _CharacterFriendsId;

		public CharacterFriendListResponseModel([JetBrains.Annotations.NotNull] int[] characterFriendsId)
		{
			_CharacterFriendsId = characterFriendsId ?? throw new ArgumentNullException(nameof(characterFriendsId));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private CharacterFriendListResponseModel()
		{
			
		}
	}
}
