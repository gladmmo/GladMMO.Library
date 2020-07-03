using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class CorpseNameBuilder : INameBuilder
	{
		private IEntityDataFieldContainer OptionalFieldData { get; }

		private ObjectGuid CorpseGuid { get; }

		private IDictionary<ObjectGuid, string> NameQueryable { get; }

		private string OwnerName { get; }

		public CorpseNameBuilder([CanBeNull] IEntityDataFieldContainer fieldData,
			[NotNull] ObjectGuid corpseGuid,
			[NotNull] IDictionary<ObjectGuid, string> nameQueryable)
		{
			OptionalFieldData = fieldData;
			CorpseGuid = corpseGuid ?? throw new ArgumentNullException(nameof(corpseGuid));
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));

			if (!corpseGuid.isType(EntityTypeId.TYPEID_CORPSE))
				throw new InvalidOperationException($"Cannot use {nameof(CorpseNameBuilder)} for non-Corpse Entity: {corpseGuid}");
		}

		public CorpseNameBuilder(ObjectGuid corpseGuid, 
			IDictionary<ObjectGuid, string> nameQueryable)
			: this(null, corpseGuid, nameQueryable)
		{

		}

		public CorpseNameBuilder([NotNull] string ownerName)
		{
			if (string.IsNullOrWhiteSpace(ownerName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(ownerName));

			OwnerName = ownerName;
		}

		public string Build()
		{
			//Case where string is provided.
			if (!string.IsNullOrWhiteSpace(OwnerName))
				return $"Corpse of {OwnerName}";

			if (OptionalFieldData != null)
			{
				ObjectGuid corpseOwner = OptionalFieldData.GetEntityGuidValue(ECorpseFields.CORPSE_FIELD_OWNER);

				if (NameQueryable.ContainsKey(corpseOwner))
					return $"Corpse of {NameQueryable[corpseOwner]}"; //do not call Retrieve, old versions of Unity3D don't support recursive readwrite locking.
			}
			
			return GladMMOCommonConstants.DEFAULT_UNKNOWN_CORPSE_NAME_STRING;
		}

		public bool IsBuildable()
		{
			if (OptionalFieldData != null)
			{
				ObjectGuid corpseOwner = OptionalFieldData.GetEntityGuidValue(ECorpseFields.CORPSE_FIELD_OWNER);
				return NameQueryable.ContainsKey(corpseOwner);
			}

			return !string.IsNullOrWhiteSpace(OwnerName);
		}

		public static implicit operator string(CorpseNameBuilder builder) => builder?.Build();
	}
}
