using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public sealed class CustomModelLoaderCreationContext
	{
		public long ContentId { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public UserContentType ContentType { get; }

		public CustomModelLoaderCreationContext(long contentId, [NotNull] NetworkEntityGuid entityGuid)
		{
			if (contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));

			ContentId = contentId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));

			switch (EntityGuid.EntityType)
			{
				case EntityType.Player:
					ContentType = UserContentType.Avatar;
					break;
				case EntityType.GameObject:
					ContentType = UserContentType.GameObject;
					break;
				case EntityType.Creature:
					ContentType = UserContentType.Creature;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public CustomModelLoaderCreationContext(long contentId, [NotNull] NetworkEntityGuid entityGuid, UserContentType contentType)
		{
			if(contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));
			if (!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int) contentType, typeof(UserContentType));

			ContentId = contentId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			ContentType = contentType;
		}
	}
}
