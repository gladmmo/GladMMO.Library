using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//From: https://github.com/HaloLive/HaloLive.Library/tree/master/src/HaloLive.Models.Common
	//The concept here is that response models should be related to a request.
	//The request should have an enumerable state of results that the response can be in.
	/// <summary>
	/// Contract for models that are responses to a request.
	/// </summary>
	public interface IResponseModel<out TResultType>
		where TResultType : Enum //TODO: Contraint to C# 7.3 enum
	{
		/// <summary>
		/// Indicates the result of the response.
		/// </summary>
		TResultType ResultCode { get; }
	}
}