using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class CustomAvatarLoaderCreationContext
	{
		public long AvatarId { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public CustomAvatarLoaderCreationContext(long avatarId, [NotNull] NetworkEntityGuid entityGuid)
		{
			if (avatarId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarId));

			AvatarId = avatarId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
