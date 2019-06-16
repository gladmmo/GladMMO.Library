using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CreatureEntryCollectionResponse : IResponseModel<CreatureEntryCollectionResponseCode>
	{
		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public CreatureEntryCollectionResponseCode ResultCode { get; private set; }
		
		[JsonProperty(PropertyName = "Entries")]
		private CreatureInstanceModel[] _Entries { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<CreatureInstanceModel> Entries => _Entries;

		/// <summary>
		/// Creatures a Successful <see cref="CreatureEntryCollectionResponse"/>
		/// </summary>
		/// <param name="entries">The entries to send.</param>
		public CreatureEntryCollectionResponse([NotNull] CreatureInstanceModel[] entries)
		{
			if(entries == null) throw new ArgumentNullException(nameof(entries));
			if(entries.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(entries));

			ResultCode = CreatureEntryCollectionResponseCode.Success;
			_Entries = entries;
		}

		/// <summary>
		/// Creates a failing <see cref="CreatureEntryCollectionResponse"/>.
		/// </summary>
		/// <param name="resultCode">The failing code.</param>
		public CreatureEntryCollectionResponse(CreatureEntryCollectionResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(CreatureEntryCollectionResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(CreatureEntryCollectionResponseCode));

			//TODO: Check and throw if success, can't be success since this is suppose to be a FAIL

			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected CreatureEntryCollectionResponse()
		{
			
		}
	}
}
