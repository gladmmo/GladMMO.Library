using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GladMMO
{
	public sealed class IncrementingCreatureGuidFactory : IFactoryCreatable<ObjectGuid, CreatureInstanceModel>
	{
		/// <summary>
		/// The atomically incremented (or should be) counter for unique creature guid issueing requests.
		/// </summary>
		private int CreatureCount = 0;

		public ObjectGuid Create(CreatureInstanceModel context)
		{
			if(!context.Guid.isTemplateGuid)
				throw new InvalidOperationException($"Cannot create instance {nameof(ObjectGuid)} from non-Template {nameof(ObjectGuid)}.");

			ObjectGuidBuilder builder = new ObjectGuidBuilder();

			return builder.WithId(Interlocked.Increment(ref CreatureCount))
				.WithType(EntityType.Creature)
				.WithEntryId(context.Guid.EntryId)
				.Build();
		}
	}
}
