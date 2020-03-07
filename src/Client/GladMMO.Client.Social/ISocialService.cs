using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface ISocialService
	{
		[RequiresAuthentication]
		[Get("/api/CharacterFriends")]
		Task<CharacterFriendListResponseModel> GetCharacterListAsync();

		[RequiresAuthentication]
		[Post("/api/CharacterFriends/befriend/{name}")]
		Task<ResponseModel<CharacterFriendAddResponseModel, CharacterFriendAddResponseCode>> TryAddFriendAsync([AliasAs("name")] string characterName);

		[Get("/api/guild/character/{id}")]
		Task<ResponseModel<CharacterGuildMembershipStatusResponse, CharacterGuildMembershipStatusResponseCode>> GetCharacterMembershipGuildStatus([AliasAs("id")] int characterId);

		[RequiresAuthentication]
		[Get("/api/guild/list")]
		Task<ResponseModel<CharacterGuildListResponseModel, CharacterGuildMembershipStatusResponseCode>> GetGuildListAsync();
	}
}