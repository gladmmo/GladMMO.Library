using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ZoneServerNPCEntryCollectionResponse : IResponseModel<NpcEntryCollectionResponseCode>
	{
		/// <inheritdoc />
		[JsonRequired]
		[JsonProperty]
		public NpcEntryCollectionResponseCode ResultCode { get; private set; }
		
		[JsonProperty(PropertyName = "Entries")]
		private ZoneServerNpcEntryModel[] _Entries { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<ZoneServerNpcEntryModel> Entries => _Entries;

		/// <summary>
		/// Creatures a Successful <see cref="ZoneServerNPCEntryCollectionResponse"/>
		/// </summary>
		/// <param name="entries">The entries to send.</param>
		public ZoneServerNPCEntryCollectionResponse([NotNull] ZoneServerNpcEntryModel[] entries)
		{
			if(entries == null) throw new ArgumentNullException(nameof(entries));
			if(entries.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(entries));

			ResultCode = NpcEntryCollectionResponseCode.Success;
			_Entries = entries;
		}

		/// <summary>
		/// Creates a failing <see cref="ZoneServerNPCEntryCollectionResponse"/>.
		/// </summary>
		/// <param name="resultCode">The failing code.</param>
		public ZoneServerNPCEntryCollectionResponse(NpcEntryCollectionResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(NpcEntryCollectionResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int)resultCode, typeof(NpcEntryCollectionResponseCode));

			//TODO: Check and throw if success, can't be success since this is suppose to be a FAIL

			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected ZoneServerNPCEntryCollectionResponse()
		{
			
		}
	}
}
