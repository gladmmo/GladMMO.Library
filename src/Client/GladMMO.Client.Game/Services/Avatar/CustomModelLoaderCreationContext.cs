using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class CustomModelLoaderCreationContext
	{
		public long ContentId { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public CustomModelLoaderCreationContext(long contentId, [NotNull] NetworkEntityGuid entityGuid)
		{
			if (contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));

			ContentId = contentId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
