using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Response model for requests that try to create/enter sessions.
	/// </summary>
	[JsonObject]
	public sealed class CharacterSessionEnterResponse : IResponseModel<CharacterSessionEnterResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public CharacterSessionEnterResponseCode ResultCode { get; private set; }
		
		//We send the id instead of the connection details because some sessions may not contiue
		//additionally this gives the client time to load the instance map and stuff while it takes time
		//to query for the zone endpoint. You could argue that we could send it in this message
		//but the reasoning is smaller message, less time to compute, less time the database spends loading the data
		//less time we take to send the response and the sooner the client can begin async loading the map
		//and during that can query for the actual endpoint. Keeps responsibility seperated between methods/actions too.
		/// <summary>
		/// The id of the zone the session will be on.
		/// </summary>
		[JsonProperty]
		public int ZoneId { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterSessionEnterResponseCode.Success;

		/// <summary>
		/// Creates a successful enter response.
		/// <see cref="ResultCode"/> will be set to success.
		/// </summary>
		/// <param name="zoneId"></param>
		/// <param name="zoneType"></param>
		public CharacterSessionEnterResponse(int zoneId)
		{
			if(zoneId < 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			//We know the response code is success if we have a zoneid
			ResultCode = CharacterSessionEnterResponseCode.Success;
			ZoneId = zoneId;
		}

		/// <summary>
		/// Creates a failing session enter response.
		/// </summary>
		/// <param name="resultCode">The failure code. Throws if success is provided.</param>
		public CharacterSessionEnterResponse(CharacterSessionEnterResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterSessionEnterResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(CharacterSessionEnterResponseCode));
			if(resultCode == CharacterSessionEnterResponseCode.Success)
				throw new ArgumentException($"Cannot provide success to a failing enter response.", nameof(resultCode));

			ResultCode = resultCode;
		}

		//Serializer ctor
		protected CharacterSessionEnterResponse()
		{
			
		}
	}
}
