using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	//While it might seem silly to have this right now, the context may be expanded on in the future.
	public sealed class PlayerEntityDestructionContext : IEntityGuidContainer
	{
		/// <inheritdoc />
		public NetworkEntityGuid EntityGuid { get; }

		/// <inheritdoc />
		public PlayerEntityDestructionContext([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
