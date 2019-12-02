using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ContentIconInstanceModel : IContentIconEntry
	{
		[JsonProperty]
		public int IconId { get; private set; }

		[JsonProperty]
		public string IconPathName { get; private set; }

		public ContentIconInstanceModel(int iconId, [NotNull] string iconPathName)
		{
			if (iconId <= 0) throw new ArgumentOutOfRangeException(nameof(iconId));
			if (string.IsNullOrWhiteSpace(iconPathName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(iconPathName));

			IconId = iconId;
			IconPathName = iconPathName;
		}

		[JsonConstructor]
		internal ContentIconInstanceModel()
		{

		}
	}
}
