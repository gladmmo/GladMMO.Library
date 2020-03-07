using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public class SocialServiceAuthTokenRepository : IReadonlyAuthTokenRepository
	{
		/// <summary>
		/// The authentication token.
		/// </summary>
		private static string AuthToken { get; } = Environment.GetEnvironmentVariable(GladMMOServiceConstants.SERVICE_AUTHORIZATION_TOKEN_ENV_VAR_PATH);

		private static string AuthTokenWithType { get; } = $"Bearer {Environment.GetEnvironmentVariable(GladMMOServiceConstants.SERVICE_AUTHORIZATION_TOKEN_ENV_VAR_PATH)}"; 

		/// <inheritdoc />
		public string Retrieve()
		{
			return AuthToken;
		}

		public SocialServiceAuthTokenRepository()
		{

		}

		/// <inheritdoc />
		public string RetrieveWithType()
		{
			return AuthTokenWithType;
		}

		/// <inheritdoc />
		public string RetrieveType()
		{
			return "Bearer";
		}
	}
}
