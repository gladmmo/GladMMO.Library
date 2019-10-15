using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SetEntityActorTargetMessage : EntityActorMessage
	{
		public NetworkEntityGuid TargetGuid { get; }

		public SetEntityActorTargetMessage([NotNull] NetworkEntityGuid targetGuid)
		{
			TargetGuid = targetGuid ?? throw new ArgumentNullException(nameof(targetGuid));
		}
	}
}
