using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO.GaiaOnline
{
	//I don't really want to do dependency injection into custom avatars
	//so referencing statically accessible singletons will be the ideal
	//approach.
	/// <summary>
	/// GaiaOnline integration service client singleton.
	/// </summary>
	public static class GaiaOnlineIntegrationClientSingleton
	{
		private static readonly IGaiaOnlineImageCDNClient _ImageCDNClient;

		private static readonly IGaiaOnlineQueryClient _QueryClient;

		/// <summary>
		/// The Gaia Online image CDN client.
		/// </summary>
		public static IGaiaOnlineImageCDNClient ImageCDNClient => _ImageCDNClient;

		/// <summary>
		/// The Gaia Online data query client.
		/// </summary>
		public static IGaiaOnlineQueryClient QueryClient => _QueryClient;

		static GaiaOnlineIntegrationClientSingleton()
		{
			_ImageCDNClient = Refit.RestService.For<IGaiaOnlineImageCDNClient>(@"http://a2.cdn.gaiaonline.com/");

			_QueryClient = Refit.RestService.For<IGaiaOnlineQueryClient>(@"http://www.gaiaonline.com/", new RefitSettings
			{
				ContentSerializer = new XmlContentSerializer()
			});
		}
	}
}
