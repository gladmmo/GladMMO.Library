using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	public sealed class IncrementingGameObjectGuidFactory : IFactoryCreatable<ObjectGuid, GameObjectInstanceModel>
	{
		/// <summary>
		/// The atomically incremented (or should be) counter for unique gameobject guid issueing requests.
		/// </summary>
		private int GameObjectCount = 0;

		public ObjectGuid Create(GameObjectInstanceModel context)
		{
			if(!context.Guid.isTemplateGuid)
				throw new InvalidOperationException($"Cannot create instance {nameof(ObjectGuid)} from non-Template {nameof(ObjectGuid)}.");

			ObjectGuidBuilder builder = new ObjectGuidBuilder();

			return builder.WithId(Interlocked.Increment(ref GameObjectCount))
				.WithType(EntityType.GameObject)
				.WithEntryId(context.Guid.EntryId)
				.Build();
		}
	}
}