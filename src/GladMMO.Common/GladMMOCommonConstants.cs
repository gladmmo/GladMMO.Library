using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GladMMOCommonConstants
	{
		//These are just 8 right now because that's the minimum size.
		public const int PLAYER_DATA_FIELD_SIZE = 16;

		public const int GAMEOBJECT_DATA_FIELD_SIZE = 8;

		static GladMMOCommonConstants()
		{
			//If these throws highlight then you've fucked up the above.
			if ((GAMEOBJECT_DATA_FIELD_SIZE % 8) != 0)
				throw new InvalidOperationException($"Cannot have {nameof(GAMEOBJECT_DATA_FIELD_SIZE)} with field length that isn't a multiple of 8.");

			if ((PLAYER_DATA_FIELD_SIZE % 8) != 0)
				throw new InvalidOperationException($"Cannot have {nameof(PLAYER_DATA_FIELD_SIZE)} with field length that isn't a multiple of 8.");
		}
	}
}
