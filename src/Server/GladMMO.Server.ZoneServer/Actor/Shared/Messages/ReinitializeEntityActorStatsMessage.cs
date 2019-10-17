using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message that tells an entity to reset or reinitialize the stats.
	/// Ex: Health, mana and etc.
	/// </summary>
	public sealed class ReinitializeEntityActorStatsMessage : EntityActorMessage
	{
		public ReinitializeEntityActorStatsMessage()
		{
			
		}
	}
}
