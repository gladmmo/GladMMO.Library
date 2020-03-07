using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace GladMMO
{
	//TODO: Consolidate the location data into its own Model to share with the Request.
	[JsonObject]
	public sealed class ZoneServerCharacterLocationResponse : IResponseModel<ZoneServerCharacterLocationResponseCode>, ISucceedable
	{
		[JsonConverter(typeof(Vector3Converter))]
		[JsonProperty]
		public Vector3 Position { get; private set; }

		/// <summary>
		/// The ID of the map the character is located in.
		/// </summary>
		[JsonProperty]
		public int MapId { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public ZoneServerCharacterLocationResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ZoneServerCharacterLocationResponseCode.Success;

		/// <inheritdoc />
		public ZoneServerCharacterLocationResponse(Vector3 position, int mapId)
		{
			if(mapId <= 0) throw new ArgumentOutOfRangeException(nameof(mapId));

			Position = position;
			MapId = mapId;
			ResultCode = ZoneServerCharacterLocationResponseCode.Success;
		}

		public ZoneServerCharacterLocationResponse(ZoneServerCharacterLocationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ZoneServerCharacterLocationResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(ZoneServerCharacterLocationResponseCode));
			
			//TODO: Make sure not success, success would need the data
			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ZoneServerCharacterLocationResponse()
		{
			
		}
	}
}
