using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public sealed class CustomModelLoaderCreationContext
	{
		public long ContentId { get; }

		public ObjectGuid EntityGuid { get; }

		public UserContentType ContentType { get; }

		public CustomModelLoaderCreationContext(long contentId, [NotNull] ObjectGuid entityGuid)
		{
			if (contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));

			ContentId = contentId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));

			switch (EntityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_PLAYER:
					ContentType = UserContentType.Avatar;
					break;
				case EntityTypeId.TYPEID_GAMEOBJECT:
					ContentType = UserContentType.GameObject;
					break;
				case EntityTypeId.TYPEID_UNIT:
					ContentType = UserContentType.Creature;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public CustomModelLoaderCreationContext(long contentId, [NotNull] ObjectGuid entityGuid, UserContentType contentType)
		{
			if(contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));
			if (!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int) contentType, typeof(UserContentType));

			ContentId = contentId;
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			ContentType = contentType;
		}
	}
}
