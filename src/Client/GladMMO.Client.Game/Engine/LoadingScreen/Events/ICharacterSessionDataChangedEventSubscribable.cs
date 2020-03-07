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
			if (zoneIdentifier <= 0) throw new ArgumentOutOfRangeException(nameof(zoneIdentifier));

			ZoneIdentifier = zoneIdentifier;
		}
	}
}
