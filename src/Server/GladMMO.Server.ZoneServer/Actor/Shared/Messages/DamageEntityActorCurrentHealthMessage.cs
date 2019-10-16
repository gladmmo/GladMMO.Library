using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DamageEntityActorCurrentHealthMessage : EntityActorMessage
	{
		public int Damage { get; }

		public DamageEntityActorCurrentHealthMessage(int damage)
		{
			if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));

			Damage = damage;
		}
	}
}
