using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterSessionDataChangedEventSubscribable
	{
		event EventHandler<CharacterSessionDataChangedEventArgs> OnCharacterSessionDataChanged;
	}

	public sealed class CharacterSessionDataChangedEventArgs : EventArgs
	{
		public int ZoneIdentifier { get; }

		public CharacterSessionDataChangedEventArgs(int zoneIdentifier)
		{
			//So dumb, but it in 3.3.5 TrinityCore it's possible to have map 0. So DUMB
			if (zoneIdentifier < 0) throw new ArgumentOutOfRangeException(nameof(zoneIdentifier));

			ZoneIdentifier = zoneIdentifier;
		}
	}
}
