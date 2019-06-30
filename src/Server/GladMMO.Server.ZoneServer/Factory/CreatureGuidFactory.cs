using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	public sealed class IncrementingCreatureGuidFactory : IFactoryCreatable<NetworkEntityGuid, CreatureInstanceModel>
	{
		/// <summary>
		/// The atomically incremented (or should be) counter for unique creature guid issueing requests.
		/// </summary>
		private int CreatureCount = 0;

		public NetworkEntityGuid Create(CreatureInstanceModel context)
		{
			if(!context.Guid.isTemplateGuid)
				throw new InvalidOperationException($"Cannot create instance {nameof(NetworkEntityGuid)} from non-Template {nameof(NetworkEntityGuid)}.");

			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			return builder.WithId(Interlocked.Increment(ref CreatureCount))
				.WithType(EntityType.Creature)
				.WithEntryId(context.Guid.EntryId)
				.Build();
		}
	}
}
