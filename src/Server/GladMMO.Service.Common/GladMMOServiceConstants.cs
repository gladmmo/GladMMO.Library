using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GladMMOServiceConstants
	{
		/// <summary>
		/// The environment variable path for the auth database connection string.
		/// </summary>
		public const string AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH = "AUTHENTICATION_DATABASE_CONNECTION_STRING";

		/// <summary>
		/// The environment variable path for the content database connection string.
		/// </summary>
		public const string CONTENT_DATABASE_CONNECTION_STRING_ENV_VAR_PATH = "CONTENT_DATABASE_CONNECTION_STRING";

		/// <summary>
		/// The environment variable path for the character database connection string.
		/// </summary>
		public const string CHARACTER_DATABASE_CONNECTION_STRING_ENV_VAR_PATH = "CHARACTER_DATABASE_CONNECTION_STRING";

		/// <summary>
		/// The environment variable path for the Azure SignalR connection string.
		/// </summary>
		public const string AZURE_SIGNALR_CONNECTION_STRING_ENV_VAR_PATH = "AZURE_SIGNALR_CONNECTION_STRING";

		public const string SERVICE_AUTHORIZATION_TOKEN_ENV_VAR_PATH = "SERVICE_AUTHORIZATION_TOKEN";
	}
}
