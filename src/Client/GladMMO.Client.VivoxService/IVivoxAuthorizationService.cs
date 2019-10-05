using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IVivoxAuthorizationService
	{
		[RequiresAuthentication]
		[Post("/api/VivoxAccount/Login")]
		Task<ResponseModel<string, VivoxLoginResponseCode>> LoginAsync();

		[RequiresAuthentication]
		[Post("/api/VivoxChannel/proximity/join")]
		Task<ResponseModel<VivoxChannelJoinResponse, VivoxLoginResponseCode>> JoinProximityChatAsync();
	}
}
