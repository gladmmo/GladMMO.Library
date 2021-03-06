﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: We shouldn't combine all the zoneserver query stuff in a single interface
	//TODO: We need to do authorization headers for zoneserver stuff
	[Headers("User-Agent: ZoneServer")]
	public interface IZoneServerToGameServerClient
	{
		//TODO: Move this over to a Content service interface
		/*/// <summary>
		/// Requests the NPC data associated with the provided <see cref="mapId"/>.
		/// </summary>
		/// <param name="mapId">The ID of the map to load the NPC entries for.</param>
		/// <returns>HTTP response.</returns>
		[Headers("Cache-Control: max-age=5000")]
		[Get("/api/npcdata/map/{id}")]
		Task<ZoneServerNPCEntryCollectionResponse> GetNPCEntriesByMapId([AliasAs("id")] int mapId);*/

		/// <summary>
		/// Queries the server for the location of the character with the provided <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId">The character id.</param>
		/// <returns>The location model of the character (potentially empty).</returns>
		[Get("/api/characters/location/{id}")]
		Task<ZoneServerCharacterLocationResponse> GetCharacterLocation([AliasAs("id")] int characterId);

		//TODO: Doc
		//[Post("/api/characters/location")]
		//Task SaveCharacterLocation([JsonBody] ZoneServerCharacterLocationSaveRequest saveRequest);

		[Get("/api/zoneserverdata/waypoint/{id}")]
		Task<ZoneServerWaypointQueryResponse> GetPathWaypoints([AliasAs("id")] int pathId);

		[Post("/api/charactersession/claim")]
		Task<ZoneServerTryClaimSessionResponse> TryClaimSession([JsonBody] ZoneServerTryClaimSessionRequest request);

		//[AuthorizeJwt]
		[Get("/api/Authorization/user/id")] //TODO: Is this a good path?
		//[NoResponseCache] //TODO: No cache
		Task<int> GetAccountIdFromToken([AuthenticationToken] string authToken);

		//TODO: Auth and move this to a seperate service
		[Post("/api/ZoneServerCharacter/WorldTeleport")]
		Task TryWorldTeleportCharacter([JsonBody] ZoneServerWorldTeleportCharacterRequest request);

		[Patch("/api/ZoneServerCharacter/ChangeAvatar")]
		Task<AvatarPedestalChangeResponse> UpdatePlayerAvatar([JsonBody] ZoneServerAvatarPedestalInteractionCharacterRequest request);
	}
}
