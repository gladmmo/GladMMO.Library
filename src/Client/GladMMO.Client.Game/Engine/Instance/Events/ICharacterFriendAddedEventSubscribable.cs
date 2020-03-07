using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterFriendAddedEventSubscribable
	{
		event EventHandler<CharacterFriendAddedEventArgs> OnCharacterFriendAdded;
	}

	public class CharacterFriendAddedEventArgs : EventArgs
	{
		public ObjectGuid FriendGuid { get; }

		public CharacterFriendAddedEventArgs([NotNull] ObjectGuid friendGuid)
		{
			FriendGuid = friendGuid ?? throw new ArgumentNullException(nameof(friendGuid));
		}
	}
}
