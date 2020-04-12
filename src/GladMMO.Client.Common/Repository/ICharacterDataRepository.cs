using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILocalCharacterDataRepository
	{
		/// <summary>
		/// The character id.
		/// </summary>
		ObjectGuid LocalCharacterGuid { get; }

		//The reason we don't use a property setter is due to MS conventions
		//as it would hide a potentially LARGE, complex and far reaching state change (or at least I expect it to).
		/// <summary>
		/// Sets the character id to the provided <see cref="characterGuid"/>
		/// value.
		/// Resets and sets dirty all other data in this repository
		/// if the current <see cref="characterGuid"/> does not match.
		/// </summary>
		/// <param name="characterGuid"></param>
		void UpdateCharacterId(ObjectGuid characterGuid);
	}
}
