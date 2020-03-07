using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	public sealed class AsyncSocialServiceClient : BaseAsyncEndpointService<ISocialService>, ISocialService
	{
		/// <inheritdoc />
		public AsyncSocialServiceClient(Task<string> futureEndpoint)
			: base(futureEndpoint)
		{

		}

		/// <inheritdoc />
		public AsyncSocialServiceClient(Task<string> futureEndpoint, RefitSettings settings)
			: base(futureEndpoint, settings)
		{

		}

		public async Task<CharacterFriendListResponseModel> GetCharacterListAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterListAsync().ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CharacterFriendAddResponseModel, CharacterFriendAddResponseCode>> TryAddFriendAsync(string characterName)
		{
			return await (await GetService().ConfigureAwaitFalse()).TryAddFriendAsync(characterName).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CharacterGuildMembershipStatusResponse, CharacterGuildMembershipStatusResponseCode>> GetCharacterMembershipGuildStatus(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterMembershipGuildStatus(characterId).ConfigureAwaitFalse();
		}

		public async Task<ResponseModel<CharacterGuildListResponseModel, CharacterGuildMembershipStatusResponseCode>> GetGuildListAsync()
		{
			return await (await GetService().ConfigureAwaitFalse()).GetGuildListAsync().ConfigureAwaitFalse();
		}
	}
}
