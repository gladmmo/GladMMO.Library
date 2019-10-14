using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// This has nothing to do with health/combat.
	/// Don't use this externally.
	/// </summary>
	public sealed class KillPlayerActorMessage : EntityActorMessage
	{
		public NetworkEntityGuid EntityGuid { get; }

		public KillPlayerActorMessage([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
