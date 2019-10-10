using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseEntityBehaviourComponent
	{
		protected NetworkEntityGuid TargetEntity { get; }

		protected BaseEntityBehaviourComponent([NotNull] NetworkEntityGuid targetEntity)
		{
			TargetEntity = targetEntity ?? throw new ArgumentNullException(nameof(targetEntity));
		}
	}
}
