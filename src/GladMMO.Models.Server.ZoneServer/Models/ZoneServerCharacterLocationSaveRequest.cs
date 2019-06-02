using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerCharacterLocationSaveRequest
	{
		//TODO: Should we sent EntityGuid?
		[JsonProperty]
		public int CharacterId { get; private set; }

		/// <summary>
		/// The position to save.
		/// </summary>
		[JsonConverter(typeof(Vector3Converter))]
		[JsonProperty]
		public Vector3 Position { get; private set; }

		/// <summary>
		/// The ID of the map the character is located in.
		/// </summary>
		[JsonProperty]
		public int MapId { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="characterId"></param>
		/// <param name="position"></param>
		/// <param name="mapId"></param>
		public ZoneServerCharacterLocationSaveRequest(int characterId, Vector3 position, int mapId)
		{
			if(characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(mapId <= 0) throw new ArgumentOutOfRangeException(nameof(mapId));

			CharacterId = characterId;
			Position = position;
			MapId = mapId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ZoneServerCharacterLocationSaveRequest()
		{
			
		}
	}
}
