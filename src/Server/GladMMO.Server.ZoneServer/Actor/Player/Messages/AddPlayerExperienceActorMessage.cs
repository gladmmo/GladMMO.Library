using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class AddPlayerExperienceActorMessage : EntityActorMessage
	{
		/// <summary>
		/// The amount of experience to add to the player.
		/// </summary>
		public int ExperienceAmount { get; }

		public AddPlayerExperienceActorMessage(int experienceAmount)
		{
			if (experienceAmount < 0) throw new ArgumentOutOfRangeException(nameof(experienceAmount));

			ExperienceAmount = experienceAmount;
		}
	}
}
