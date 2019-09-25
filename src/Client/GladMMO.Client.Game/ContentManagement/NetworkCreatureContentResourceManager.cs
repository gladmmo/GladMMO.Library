using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public sealed class NetworkCreatureContentResourceManager : DefaultLoadableContentResourceManager
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		public NetworkCreatureContentResourceManager([NotNull] IDownloadableContentServerServiceClient contentClient, ILog logger) 
			: base(logger, UserContentType.Creature)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
		}

		protected override async Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId)
		{
			return await ContentClient.RequestCreatureModelDownloadUrl(contentId)
				.ConfigureAwait(false);
		}
	}
}
