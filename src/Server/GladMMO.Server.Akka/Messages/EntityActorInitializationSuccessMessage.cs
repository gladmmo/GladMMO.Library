using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Empty message that indicates an Entity actor's state has been successfully initialized.
	/// Entity actors can used this to do some specialized initialization here
	/// or they can startup some long running logic or AI.
	/// </summary>
	public sealed class EntityActorInitializationSuccessMessage : EntityActorMessage
	{
		public EntityActorInitializationSuccessMessage()
		{
			
		}
	}
}
