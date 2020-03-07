using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;
using UnityEngine;

namespace GladMMO.GaiaOnline
{
	//Just a get request to the Avatar url
	//Exmaple: http://a2.cdn.gaiaonline.com/dress-up/avatar/ava/9f/8b/28c7853cf78b9f.png
	/// <summary>
	/// Network client capable of querying the GaiaOnline CDN for images.
	/// </summary>
	public interface IGaiaOnlineImageCDNClient
	{
		//Warning: This can throw if it's not valid.
		//TODO: Does this send back automatically with gzip? Should we add gzip header? Does Mono/Unity have gzip implemented?
		/// <summary>
		/// Sends a GET request to the /dress-up/avatar service to get the avatar image. <see cref="uniqueAvatarUrl"/>
		/// should be a valid unique user-specific URL that can be queried through another API endpoint.
		/// </summary>
		/// <param name="uniqueAvatarUrl">The Avatar URL.</param>
		/// <returns>A Unity3D <see cref="Texture2D"/> wrapper containing the avatar image.</returns>
		[Get(@"/dress-up/avatar/{**url}")] //last slash is important, do not remove
		Task<HttpContent> GetAvatarImageBytesAsync([AliasAs("url")] string uniqueAvatarUrl);
	}
}