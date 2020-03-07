using System; using FreecraftCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using GladMMO;

namespace GladMMO
{
	public static class IClaimsPrincipalReaderExtensions
	{
		/// <summary>
		/// Returns the User ID claim value if present otherwise will throw.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The User ID claim value. Will throw if the principal doesn't contain the id.</returns>
		/// <exception cref="ArgumentException">Throws if the provided principal doesn't contain an id.</exception>
		/// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier"/>.</remarks>
		public static int GetAccountIdInt(this IClaimsPrincipalReader reader, ClaimsPrincipal principal)
		{
			int accountId;
			if(!int.TryParse(reader.GetAccountId(principal), out accountId))
			{
				throw new ArgumentException($"Provided {nameof(ClaimsPrincipal)} does not contain a user id.", nameof(principal));
			}

			return accountId;
		}

		//TODO: Doc.
		public static int GetPlayerAccountId(this IClaimsPrincipalReader reader, ClaimsPrincipal principal)
		{
			//Cannot get playeraccountid claim from non-zoneserver roles
			if(!principal.IsInRole(GladMMOAuthConstants.ZONESERVER_AUTHORIZATION_ROLE))
				throw new InvalidOperationException($"Failed to read Player AccountId Claim: {GladMMOAuthConstants.ACCOUNT_ID_OWNER_CLAIM_NAME} because provided identity did not have the zoneserver role.");

			string accountIdString = principal.FindFirstValue(GladMMOAuthConstants.ACCOUNT_ID_OWNER_CLAIM_NAME);

			if(int.TryParse(accountIdString, out int resultValue))
				return resultValue;
			else
				throw new InvalidOperationException($"Failed to read Player AccountId Claim: {GladMMOAuthConstants.ACCOUNT_ID_OWNER_CLAIM_NAME}");
		}
	}
}