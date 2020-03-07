using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public abstract class BaseEntityBehaviourComponent
	{
		protected ObjectGuid TargetEntity { get; }

		protected BaseEntityBehaviourComponent([NotNull] ObjectGuid targetEntity)
		{
			TargetEntity = targetEntity ?? throw new ArgumentNullException(nameof(targetEntity));
		}
	}
}
