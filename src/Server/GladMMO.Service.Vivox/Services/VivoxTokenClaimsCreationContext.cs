using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class VivoxTokenClaimsCreationContext
	{
		public int CharacterId { get; }

		public VivoxAction Action { get; }

		public VivoxTokenClaimsCreationContext(int characterId, VivoxAction action)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (!Enum.IsDefined(typeof(VivoxAction), action)) throw new InvalidEnumArgumentException(nameof(action), (int)action, typeof(VivoxAction));

			CharacterId = characterId;
			Action = action;
		}
	}
}
