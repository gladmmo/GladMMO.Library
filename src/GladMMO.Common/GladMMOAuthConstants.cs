using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GladMMOAuthConstants
	{
		/// <summary>
		/// JWT claim entry type for account id.
		/// </summary>
		public const string ACCOUNT_ID_OWNER_CLAIM_NAME = "vrgid"; //vrgid is VRGuardians User Identifier.

		public const string ZONESERVER_AUTHORIZATION_ROLE = "ZoneServer";

		public const string PLAYERACCOUNT_AUTHORIZATION_ROLE = "Player";
	}
}
