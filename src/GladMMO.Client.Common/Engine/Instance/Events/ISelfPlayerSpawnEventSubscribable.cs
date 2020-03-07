using System; using FreecraftCore;
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
		//public ObjectCreationData CreationData { get; }

		/// <inheritdoc />
		public SelfPlayerSpawnEventArgs(/*[NotNull] ObjectCreationData creationData*/)
		{
			//CreationData = creationData ?? throw new ArgumentNullException(nameof(creationData));
		}
	}
}
