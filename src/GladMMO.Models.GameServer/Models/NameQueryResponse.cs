using System;
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
	public sealed class NameQueryResponse : IResponseModel<NameQueryResponseCode>, ISucceedable
	{
		/// <summary>
		/// Optional name of the entity from the request.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public string EntityName { get; private set; }

		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public NameQueryResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == NameQueryResponseCode.Success;

		/// <inheritdoc />
		public NameQueryResponse(string characterName)
		{
			if(string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(characterName));

			EntityName = characterName;
			ResultCode = NameQueryResponseCode.Success;
		}

		/// <inheritdoc />
		public NameQueryResponse(NameQueryResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(NameQueryResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(NameQueryResponseCode));

			//We don't want success to be possible. It means we need to provide the actual data.
			if(resultCode == NameQueryResponseCode.Success)
				throw new ArgumentException($"Cannot provide success to response without providing optional success based data. Use other ctor.");

			ResultCode = resultCode;
		}

		//Serializer ctor
		public NameQueryResponse()
		{
			
		}
	}
}
