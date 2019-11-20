using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO.GaiaOnline
{
	public static class GaiaOnlineClientExtensions
	{
		/// <summary>
		/// Calls <see cref="IGaiaOnlineImageCDNClient"/>.GetAvatarImageBytesAsync
		/// </summary>
		/// <param name="client">The gaia image CDN client.</param>
		/// <param name="uniqueAvatarUrl">The Avatar URL.</param>
		/// <returns>Texture2D wrapper</returns>
		public static async Task<Texture2DWrapper> GetAvatarImageAsync([NotNull] this IGaiaOnlineImageCDNClient client, string uniqueAvatarUrl)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			byte[] imageBytes = await (await client.GetAvatarImageBytesAsync(uniqueAvatarUrl)).ReadAsByteArrayAsync();

			return new Texture2DWrapper(imageBytes); 
		}
	}
}
