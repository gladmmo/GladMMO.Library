using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SetEntityActorLevelMessage : EntityActorMessage
	{
		public int NewLevel { get; }

		public SetEntityActorLevelMessage(int newLevel)
		{
			if (newLevel <= 0) throw new ArgumentOutOfRangeException(nameof(newLevel));

			NewLevel = newLevel;
		}
	}
}
