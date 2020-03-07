using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SetEntityActorTargetMessage : EntityActorMessage
	{
		public ObjectGuid TargetGuid { get; }

		public SetEntityActorTargetMessage([NotNull] ObjectGuid targetGuid)
		{
			TargetGuid = targetGuid ?? throw new ArgumentNullException(nameof(targetGuid));
		}
	}
}
