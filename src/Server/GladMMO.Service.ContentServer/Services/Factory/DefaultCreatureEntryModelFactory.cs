using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;

namespace GladMMO
{
	public sealed class DefaultCreatureEntryModelFactory : IFactoryCreatable<CreatureEntryModel, WorldInstanceableEntryModelCreationContext>
	{
		public CreatureEntryModel Create([NotNull] WorldInstanceableEntryModelCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			return new CreatureEntryModel(1, new Vector3<float>(0, 0, 0), 0, context.WorldId);
		}
	}
}
