using System; using FreecraftCore;
using System.ComponentModel;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Response model for name validation requests.
	/// </summary>
	[JsonObject]
	public sealed class CharacterNameValidationResponse : IResponseModel<CharacterNameValidationResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public CharacterNameValidationResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterNameValidationResponseCode.Success;

		/// <inheritdoc />
		public CharacterNameValidationResponse(CharacterNameValidationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterNameValidationResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(CharacterNameValidationResponseCode));

			ResultCode = resultCode;
		}

		//Serializer ctor
		protected CharacterNameValidationResponse()
		{
			
		}
	}
}
