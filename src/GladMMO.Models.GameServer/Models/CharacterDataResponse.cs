using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterDataResponse
	{
		//We may have more stuff eventually, right now we only have the model id.
		[JsonProperty]
		public int Experience { get; private set; }

		public CharacterDataResponse(int experience)
		{
			if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));

			Experience = experience;
		}

		//Serializer ctor
		private CharacterDataResponse()
		{

		}
	}
}
