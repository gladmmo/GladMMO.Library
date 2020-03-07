using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerCharacterLocationSaveRequest
	{
		/// <summary>
		/// The position to save.
		/// </summary>
		[JsonConverter(typeof(Vector3Converter))]
		[JsonProperty]
		public Vector3 Position { get; private set; }

		//We don't need mapid anymore since we have zoneserver authorization now. It authoratively knows which world it is.

		/// <summary>
		/// 
		/// </summary>
		/// <param name="characterId"></param>
		/// <param name="position"></param>
		public ZoneServerCharacterLocationSaveRequest(Vector3 position)
		{
			Position = position;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		protected ZoneServerCharacterLocationSaveRequest()
		{
			
		}
	}
}
