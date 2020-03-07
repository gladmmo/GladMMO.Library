using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Response model to a character namequery request.
	/// </summary>
	[JsonObject]
	public sealed class NameQueryResponse
	{
		/// <summary>
		/// Optional name of the entity from the request.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public string EntityName { get; private set; }

		/// <inheritdoc />
		public NameQueryResponse(string characterName)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));

			EntityName = characterName;
		}

		//Serializer ctor
		public NameQueryResponse()
		{
			
		}
	}
}
