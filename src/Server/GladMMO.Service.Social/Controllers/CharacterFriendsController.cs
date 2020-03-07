using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/CharacterFriends")]
	public class CharacterFriendsController : AuthorizationReadyController
	{
		public CharacterFriendsController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[ProducesJson]
		[ResponseCache(Duration = 10)] //This should be good for caching
		[AuthorizeJwt]
		[HttpGet]
		public async Task<IActionResult> GetCharacterFriends([FromServices] [JetBrains.Annotations.NotNull] ICharacterFriendRepository friendsRepository,
			[FromServices] [JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialServiceClient)
		{
			if (friendsRepository == null) throw new ArgumentNullException(nameof(friendsRepository));
			if (socialServiceClient == null) throw new ArgumentNullException(nameof(socialServiceClient));

			//Find the character
			CharacterSessionDataResponse response = await socialServiceClient.GetCharacterSessionDataByAccount(ClaimsReader.GetAccountIdInt(User));

			if (response.ResultCode == CharacterSessionDataResponseCode.NoSessionAvailable)
				return Json(new CharacterFriendListResponseModel(Array.Empty<int>()));

			int[] friendsCharacterIds = await friendsRepository.GetCharactersFriendsList(response.CharacterId);

			return Json(new CharacterFriendListResponseModel(friendsCharacterIds));
		}

		[ProducesJson]
		[AuthorizeJwt]
		[HttpPost("befriend/{name}")]
		public async Task<IActionResult> TryAddFriend([FromRoute(Name = "name")] [JetBrains.Annotations.NotNull] string characterFriendName,
			[FromServices] [JetBrains.Annotations.NotNull] ICharacterFriendRepository friendsRepository,
			[FromServices] [JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialServiceClient,
			[FromServices] INameQueryService nameQueryService)
		{
			if (friendsRepository == null) throw new ArgumentNullException(nameof(friendsRepository));
			if (socialServiceClient == null) throw new ArgumentNullException(nameof(socialServiceClient));
			if (string.IsNullOrEmpty(characterFriendName)) throw new ArgumentException("Value cannot be null or empty.", nameof(characterFriendName));

			//Find the character
			CharacterSessionDataResponse response = await socialServiceClient.GetCharacterSessionDataByAccount(ClaimsReader.GetAccountIdInt(User));

			if (response.ResultCode == CharacterSessionDataResponseCode.NoSessionAvailable)
				return BadRequest();

			var nameReverseQueryResponse = await nameQueryService.RetrievePlayerGuidAsync(characterFriendName);

			//Handle known failure cases first.
			switch (nameReverseQueryResponse.ResultCode)
			{
				case NameQueryResponseCode.UnknownIdError:
					return BuildFailedResponseModel(CharacterFriendAddResponseCode.CharacterNotFound);
				case NameQueryResponseCode.GeneralServerError:
					return BuildFailedResponseModel(CharacterFriendAddResponseCode.GeneralServerError);
			}

			//If the player is trying to add himself, just say not found
			if(nameReverseQueryResponse.Result.CurrentObjectGuid == response.CharacterId)
				return BuildFailedResponseModel(CharacterFriendAddResponseCode.CharacterNotFound);

			//Ok, reverse namequery is a success
			//now we must check some stuff

			//Already friends check
			if (await friendsRepository.IsFriendshipPresentAsync(response.CharacterId, nameReverseQueryResponse.Result.CurrentObjectGuid))
				return BuildFailedResponseModel(CharacterFriendAddResponseCode.AlreadyFriends);

			if (await friendsRepository.TryCreateAsync(new CharacterFriendModel(response.CharacterId, nameReverseQueryResponse.Result.CurrentObjectGuid)))
			{
				//This is a success, let's tell them about who they added.
				return BuildSuccessfulResponseModel(new CharacterFriendAddResponseModel(nameReverseQueryResponse.Result));
			}
			else
				return BuildFailedResponseModel(CharacterFriendAddResponseCode.GeneralServerError);
		}
	}
}
