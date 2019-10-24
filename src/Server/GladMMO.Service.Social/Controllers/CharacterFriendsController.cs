using System;
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
			CharacterSessionDataResponse response = await socialServiceClient.GetCharacterSessionDataByAccount(ClaimsReader.GetUserIdInt(User));

			if (response.ResultCode == CharacterSessionDataResponseCode.NoSessionAvailable)
				return Json(new CharacterFriendListResponseModel(Array.Empty<int>()));

			int[] friendsCharacterIds = await friendsRepository.GetCharactersFriendsList(response.CharacterId);

			return Json(new CharacterFriendListResponseModel(friendsCharacterIds));
		}
	}
}
