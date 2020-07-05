using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GladMMOCommonConstants
	{
		public const string DEFAULT_UNKNOWN_ENTITY_NAME_STRING = "Unknown";

		public const string DEFAULT_UNKNOWN_CORPSE_NAME_STRING = "Corpse of " + DEFAULT_UNKNOWN_ENTITY_NAME_STRING;

		public const int MOVEMENT_PACKET_HEARTBEAT_TIME_MILLISECONDS = 500; //500ms for movement heartbeats.
	}
}
