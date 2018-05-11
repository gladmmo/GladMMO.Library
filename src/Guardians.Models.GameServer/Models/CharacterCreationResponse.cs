﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace Guardians
{
	[JsonObject]
	public sealed class CharacterCreationResponse : IResponseModel<CharacterCreationResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public CharacterCreationResponseCode ResultCode { get; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterCreationResponseCode.Success;

		/// <inheritdoc />
		public CharacterCreationResponse(CharacterCreationResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CharacterCreationResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(CharacterCreationResponseCode));

			ResultCode = resultCode;
		}
	}
}
