using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	public sealed class ZoneServerTryClaimSessionResponse : IResponseModel<ZoneServerTryClaimSessionResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ZoneServerTryClaimSessionResponseCode.Success;

		/// <inheritdoc />
		[JsonProperty]
		public ZoneServerTryClaimSessionResponseCode ResultCode { get; }

		/// <inheritdoc />
		public ZoneServerTryClaimSessionResponse(ZoneServerTryClaimSessionResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ZoneServerTryClaimSessionResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(ZoneServerTryClaimSessionResponseCode));

			ResultCode = resultCode;
		}

		private ZoneServerTryClaimSessionResponse()
		{
			
		}
	}
}
