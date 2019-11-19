using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO.GaiaOnline
{
	//profiles?mode=lookup&avatar_username=
	/// <summary>
	/// Network client interface that allows for querying the Gaia API.
	/// </summary>
	public interface IGaiaOnlineQueryClient
	{
		//Should end up as something like this ?mode=lookup&avatar_username={username}
		//Example: http://gaiaonline.com/profiles?mode=lookup&avatar_username=lm_a_kitty_cat
		/// <summary>
		/// Requests the avatar information for the specified <see cref="userName"/>.
		/// Returns an XML model <see cref="UserAvatarQueryResponse"/>.
		/// </summary>
		/// <param name="userName">The username to query for.</param>
		/// <returns>A model containing the result of the request.</returns>
		[Get("/profiles?mode=lookup")]
		Task<UserAvatarQueryResponse> GetAvatarFromUsernameAsync([AliasAs("avatar_username")] string userName);
	}
}