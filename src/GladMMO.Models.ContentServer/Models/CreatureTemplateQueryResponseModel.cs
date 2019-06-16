using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CreatureTemplateQueryResponseModel : IResponseModel<CreatureEntryCollectionResponseCode>, ISucceedable
	{
		[JsonProperty]
		public CreatureEntryCollectionResponseCode ResultCode { get; private set; }

		[JsonIgnore]
		public bool isSuccessful => ResultCode == CreatureEntryCollectionResponseCode.Success;

		public CreatureTemplateModel CreatureTemplate { get; private set; }

		public CreatureTemplateQueryResponseModel(CreatureEntryCollectionResponseCode resultCode)
		{
			//TODO: Check it's not success, this is failure CTOR.
			if (!Enum.IsDefined(typeof(CreatureEntryCollectionResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int) resultCode, typeof(CreatureEntryCollectionResponseCode));
			ResultCode = resultCode;
		}

		public CreatureTemplateQueryResponseModel([NotNull] CreatureTemplateModel creatureTemplate)
		{
			ResultCode = CreatureEntryCollectionResponseCode.Success;
			CreatureTemplate = creatureTemplate ?? throw new ArgumentNullException(nameof(creatureTemplate));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CreatureTemplateQueryResponseModel()
		{
			
		}
	}
}
