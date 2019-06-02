using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Context for a player entity/session leaving the world.
	/// </summary>
	public sealed class PlayerSessionDeconstructionContext
	{
		public int ConnectionId { get; }

		/// <inheritdoc />
		public PlayerSessionDeconstructionContext(int connectionId)
		{
			ConnectionId = connectionId;
		}
	}
}
