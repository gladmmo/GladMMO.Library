using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public sealed class NetworkGameObjectContentResourceManager : DefaultLoadableContentResourceManager
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		public NetworkGameObjectContentResourceManager([NotNull] IDownloadableContentServerServiceClient contentClient, ILog logger) 
			: base(logger, UserContentType.GameObject)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
		}

		protected override async Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId)
		{
			return await ContentClient.RequestGameObjectModelDownloadUrl(contentId)
				.ConfigureAwait(false);
		}
	}
}
