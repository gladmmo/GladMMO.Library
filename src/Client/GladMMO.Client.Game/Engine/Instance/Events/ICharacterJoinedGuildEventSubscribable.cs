using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterJoinedGuildEventSubscribable
	{
		event EventHandler<CharacterJoinedGuildEventArgs> OnCharacterJoinedGuild;
	}

	public sealed class CharacterJoinedGuildEventArgs : EventArgs
	{
		/// <summary>
		/// The GUID of the player joining the guild.
		/// </summary>
		public ObjectGuid JoineeGuid { get; }

		/// <summary>
		/// Indicates if this join is hidden.
		/// An example might be the initial roster list query.
		/// </summary>
		public bool isHiddenJoin { get; }

		public CharacterJoinedGuildEventArgs([NotNull] ObjectGuid joineeGuid, bool isHiddenJoin)
		{
			JoineeGuid = joineeGuid ?? throw new ArgumentNullException(nameof(joineeGuid));
			this.isHiddenJoin = isHiddenJoin;
		}
	}
}
