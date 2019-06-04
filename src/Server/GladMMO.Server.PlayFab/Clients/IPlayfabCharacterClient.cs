using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: GuardiansServer")]
	public interface IPlayfabCharacterClient
	{
		/// <summary>
		/// See: https://api.playfab.com/documentation/server/method/GrantCharacterToUser
		/// Grants the specified character type to the user. CharacterIds are not globally unique; characterId must be evaluated with the parent PlayFabId to guarantee uniqueness.
		/// </summary>
		/// <param name="request">The character grant request.</param>
		/// <returns>Result of the character grant attempt.</returns>
		[PlayFabServerAuthenticationHeader]
		[Post("/Server/GrantCharacterToUser")]
		Task<PlayFabResultModel<GladMMOPlayFabGrantCharacterToUserResult>> GrantCharacterToUser(GladMMOPlayFabGrantCharacterToUserRequest request);
	}
}
