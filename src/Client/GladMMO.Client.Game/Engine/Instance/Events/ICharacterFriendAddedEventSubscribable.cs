using System;
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
		public NetworkEntityGuid FriendGuid { get; }

		public CharacterFriendAddedEventArgs([NotNull] NetworkEntityGuid friendGuid)
		{
			FriendGuid = friendGuid ?? throw new ArgumentNullException(nameof(friendGuid));
		}
	}
}
