using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Refit;

namespace GladMMO
{
	public sealed class AsyncEndpointZoneServerToGameServerService : BaseAsyncEndpointService<IZoneServerToGameServerClient>, IZoneServerToGameServerClient
	{
		/// <inheritdoc />
		public AsyncEndpointZoneServerToGameServerService([NotNull] Task<string> futureEndpoint) 
			: base(futureEndpoint)
		{
		}

		/// <inheritdoc />
		public AsyncEndpointZoneServerToGameServerService([NotNull] Task<string> futureEndpoint, [NotNull] RefitSettings settings) 
			: base(futureEndpoint, settings)
		{
		}

		/// <inheritdoc />
		public async Task<ZoneServerCharacterLocationResponse> GetCharacterLocation(int characterId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetCharacterLocation(characterId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ZoneServerWaypointQueryResponse> GetPathWaypoints(int pathId)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetPathWaypoints(pathId).ConfigureAwaitFalse();
		}

		/// <inheritdoc />
		public async Task<ZoneServerTryClaimSessionResponse> TryClaimSession(ZoneServerTryClaimSessionRequest request)
		{
			return await (await GetService().ConfigureAwaitFalse()).TryClaimSession(request).ConfigureAwaitFalse();
		}

		//TODO: This should actually be in another service
		/// <inheritdoc />
		public async Task<int> GetAccountIdFromToken(string authToken)
		{
			return await (await GetService().ConfigureAwaitFalse()).GetAccountIdFromToken(authToken).ConfigureAwaitFalse();
		}

		public async Task TryWorldTeleportCharacter(ZoneServerWorldTeleportCharacterRequest request)
		{
			await (await GetService().ConfigureAwaitFalse()).TryWorldTeleportCharacter(request).ConfigureAwaitFalseVoid();
		}

		public async Task<AvatarPedestalChangeResponse> UpdatePlayerAvatar(ZoneServerAvatarPedestalInteractionCharacterRequest request)
		{
			return await (await GetService().ConfigureAwaitFalse()).UpdatePlayerAvatar(request).ConfigureAwaitFalse();
		}
	}
}
