using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncStaticContentDataServiceClient : BaseAsyncEndpointService<IStaticContentDataServiceClient>, IStaticContentDataServiceClient
	{
		/// <inheritdoc />
		public AsyncStaticContentDataServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncStaticContentDataServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ContentIconInstanceModel[]> ContentIconsAsync()
		{
			return await (await GetService().ConfigureAwait(false)).ContentIconsAsync().ConfigureAwait(false);
		}
	}
}
