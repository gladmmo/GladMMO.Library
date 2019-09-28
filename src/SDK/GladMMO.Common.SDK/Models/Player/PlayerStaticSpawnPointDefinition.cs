using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class PlayerStaticSpawnPointDefinition : StaticSpawnPointDefinition
	{
		public override EntityType EntitySpawnType => EntityType.Player;
	}
}
