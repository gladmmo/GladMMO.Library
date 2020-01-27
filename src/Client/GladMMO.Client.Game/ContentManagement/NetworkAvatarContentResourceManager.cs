using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace GladMMO
{
	public sealed class NetworkAvatarContentResourceManager : DefaultLoadableContentResourceManager
	{
		private IDownloadableContentServerServiceClient ContentClient { get; }

		public NetworkAvatarContentResourceManager([NotNull] IDownloadableContentServerServiceClient contentClient, ILog logger) 
			: base(logger, UserContentType.Avatar)
		{
			ContentClient = contentClient ?? throw new ArgumentNullException(nameof(contentClient));
		}

		protected override async Task<ContentDownloadURLResponse> RequestDownloadURL(long contentId)
		{
			return await ContentClient.RequestAvatarDownloadUrl(contentId)
				.ConfigureAwaitFalse();
		}
	}
}
