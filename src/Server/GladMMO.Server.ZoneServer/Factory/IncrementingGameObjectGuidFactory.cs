using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	public sealed class IncrementingGameObjectGuidFactory : IFactoryCreatable<NetworkEntityGuid, GameObjectInstanceModel>
	{
		/// <summary>
		/// The atomically incremented (or should be) counter for unique gameobject guid issueing requests.
		/// </summary>
		private int GameObjectCount = 0;

		public NetworkEntityGuid Create(GameObjectInstanceModel context)
		{
			if(!context.Guid.isTemplateGuid)
				throw new InvalidOperationException($"Cannot create instance {nameof(NetworkEntityGuid)} from non-Template {nameof(NetworkEntityGuid)}.");

			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			return builder.WithId(Interlocked.Increment(ref GameObjectCount))
				.WithType(EntityType.GameObject)
				.WithEntryId(context.Guid.EntryId)
				.Build();
		}
	}
}