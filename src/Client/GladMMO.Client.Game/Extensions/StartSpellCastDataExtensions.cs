using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public static class StartSpellCastDataExtensions
	{
		public static long CalculateTemporarilyUniqueKey([NotNull] this StartSpellCastData castData)
		{
			if (castData == null) throw new ArgumentNullException(nameof(castData));

			return (long)castData.SpellCastCount << 32 | (long)castData.SpellId;
		}
	}
}
