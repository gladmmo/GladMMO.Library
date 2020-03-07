﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;

namespace GladMMO
{
	public sealed class DefaultGameObjectEntryModelFactory : IFactoryCreatable<GameObjectEntryModel, WorldInstanceableEntryModelCreationContext>
	{
		public GameObjectEntryModel Create([NotNull] WorldInstanceableEntryModelCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			return new GameObjectEntryModel(1, new GladMMO.Database.Vector3<float>(0, 0, 0), 0, context.WorldId);
		}
	}
}
