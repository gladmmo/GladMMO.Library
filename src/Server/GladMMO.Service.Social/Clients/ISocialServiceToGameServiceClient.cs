using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace GladMMO
{
	//TODO: We should unify these clients into a single GameServer client.
	/// <summary>
	/// Proxy interface for ServerSelection (List) Server RPCs.
	/// </summary>
	[Headers("User-Agent: GuardiansBackend")]
	public interface ISocialServiceToGameServiceClient
	{
		[RequiresAuthentication]
		[Get("/api/CharacterSession/account/{id}/data")]
		Task<CharacterSessionDataResponse> GetCharacterSessionDataByAccount([AliasAs("id")] int accountId);
	}
}
