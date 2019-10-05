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

		public VivoxChannelData Channel { get; }

		public VivoxTokenClaimsCreationContext(int characterId, VivoxAction action, VivoxChannelData channel = null)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (!Enum.IsDefined(typeof(VivoxAction), action)) throw new InvalidEnumArgumentException(nameof(action), (int)action, typeof(VivoxAction));

			CharacterId = characterId;
			Action = action;

			//Channel data is optional, we don't need to null check it.
			//it's not my fault that it's designed this dumb way
			Channel = channel;
		}
	}
}
