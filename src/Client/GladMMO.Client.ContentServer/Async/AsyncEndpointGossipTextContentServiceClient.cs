using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointGossipTextContentServiceClient : BaseAsyncEndpointService<IGossipTextContentServiceClient>, IGossipTextContentServiceClient
	{
		/// <inheritdoc />
		public AsyncEndpointGossipTextContentServiceClient(Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncEndpointGossipTextContentServiceClient(Task<string> futureEndpoint, RefitSettings settings) 
			: base(futureEndpoint, settings)
		{

		}

		public async Task<ResponseModel<QuestTextContentModel, GameContentQueryResponseCode>> GetQuestGossipTextAsync(int questId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetQuestGossipTextAsync(questId).ConfigureAwaitFalse();
		}

		public async Task<string> GetCreatureGossipTextAsync(int textId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCreatureGossipTextAsync(textId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<string, GameContentQueryResponseCode>> GetQuestIncompleteGossipTextAsync(int questId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetQuestIncompleteGossipTextAsync(questId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<string, GameContentQueryResponseCode>> GetQuestCompleteGossipTextAsync(int questId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetQuestCompleteGossipTextAsync(questId).ConfigureAwaitFalse();
		}
	}
}
