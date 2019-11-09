using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class HealEntityActorCurrentHealthMessage : EntityActorMessage
	{
		public int HealAmount { get; }

		public HealEntityActorCurrentHealthMessage(int healAmount)
		{
			if (healAmount < 0) throw new ArgumentOutOfRangeException(nameof(healAmount));

			HealAmount = healAmount;
		}
	}
}
