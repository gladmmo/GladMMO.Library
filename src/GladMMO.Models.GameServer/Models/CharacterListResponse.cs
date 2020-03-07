using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	/// <summary>
	/// Response model for character list requests.
	/// </summary>
	[JsonObject]
	public class CharacterListResponse : IResponseModel<CharacterListResponseCode>, ISucceedable
	{
		private static int[] EmptyInts { get; } = new int[0];

		/// <summary>
		/// Optionally backing field for 
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		private int[] _CharacterIds { get; set; } = EmptyInts;

		/// <summary>
		/// The IDs of the characters.
		/// </summary>
		[JsonIgnore]
		public IReadOnlyCollection<int> CharacterIds => _CharacterIds;

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterListResponseCode.Success;

		//Serializer ctor
		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public CharacterListResponseCode ResultCode { get; private set; } //unity requires private set

		/// <summary>
		/// Creates a successful response.
		/// <see cref="ResultCode"/> will be set to Success.
		/// </summary>
		/// <param name="characterIds">The character id.</param>
		public CharacterListResponse(int[] characterIds)
		{
			if(characterIds == null) throw new ArgumentNullException(nameof(characterIds));
			if(characterIds.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(characterIds));

			_CharacterIds = characterIds;
			ResultCode = CharacterListResponseCode.Success;
		}

		/// <inheritdoc />
		public CharacterListResponse(CharacterListResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterListResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(CharacterListResponseCode));
			if(resultCode == CharacterListResponseCode.Success)
				throw new ArgumentException($"Cannot provided {nameof(CharacterListResponseCode)}.{CharacterListResponseCode.Success.ToString()} to failing ctor.");

			//This has to be failing. Otherwise the collection be empty/null with success
			ResultCode = resultCode;
		}

		protected CharacterListResponse()
		{
			
		}
	}
}
