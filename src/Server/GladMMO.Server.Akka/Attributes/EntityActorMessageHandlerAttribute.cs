using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class EntityActorMessageHandlerAttribute : Attribute
	{
		public Type TargetActorType { get; }

		public EntityActorMessageHandlerAttribute(Type targetActorType)
		{
			TargetActorType = targetActorType ?? throw new ArgumentNullException(nameof(targetActorType));

			if (!typeof(IEntityActor).IsAssignableFrom(targetActorType))
				throw new InvalidOperationException($"Provided Type: {targetActorType.Name} is not a valid Entity Actor.");
		}
	}
}
