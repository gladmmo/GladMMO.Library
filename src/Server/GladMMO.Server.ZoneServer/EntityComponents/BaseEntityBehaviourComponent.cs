using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class BaseEntityBehaviourComponent
	{
		protected NetworkEntityGuid TargetEntity { get; }

		public BaseEntityBehaviourComponent([NotNull] NetworkEntityGuid targetEntity)
		{
			TargetEntity = targetEntity ?? throw new ArgumentNullException(nameof(targetEntity));
		}
	}
}
