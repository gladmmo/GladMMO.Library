using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Don't subscribe to this event if you want to do something
	/// on player SPAWN. This is for the networked spawn event payload
	/// being recieved.
	/// </summary>
	public interface ISelfPlayerSpawnEventSubscribable
	{
		event EventHandler<SelfPlayerSpawnEventArgs> OnSelfPlayerSpawnEvent;
	}

	public sealed class SelfPlayerSpawnEventArgs : EventArgs
	{
		//public EntityCreationData CreationData { get; }

		/// <inheritdoc />
		public SelfPlayerSpawnEventArgs(/*[NotNull] EntityCreationData creationData*/)
		{
			//CreationData = creationData ?? throw new ArgumentNullException(nameof(creationData));
		}
	}
}
