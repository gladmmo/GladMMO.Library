using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerRegisterationResponse : IResponseModel<ZoneServerRegisterationResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public ZoneServerRegisterationResponseCode ResultCode { get; private set; }

		/// <summary>
		/// The ID of the world that is assigned to this zone.
		/// </summary>
		[JsonProperty]
		public long WorldId { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ZoneServerRegisterationResponseCode.Success;

		/// <summary>
		/// Creates a successful response with the provided <see cref="WorldId"/>
		/// </summary>
		/// <param name="worldId">The ID of the world.</param>
		public ZoneServerRegisterationResponse(long worldId)
		{
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			WorldId = worldId;
			ResultCode = ZoneServerRegisterationResponseCode.Success;
		}

		/// <inheritdoc />
		public ZoneServerRegisterationResponse(ZoneServerRegisterationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ZoneServerRegisterationResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(ZoneServerRegisterationResponseCode));

			//TODO: Asset it'snot success
			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ZoneServerRegisterationResponse()
		{
			
		}
	}
}
