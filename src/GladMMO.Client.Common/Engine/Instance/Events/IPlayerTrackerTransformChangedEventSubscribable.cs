using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerTrackerTransformChangedEventSubscribable
	{
		event EventHandler<PlayerTrackerTransformChangedEventArgs> OnTrackerTransformChanged;
	}

	public sealed class PlayerTrackerTransformChangedEventArgs : EventArgs
	{
		public EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> ChangeInformation { get; }

		public PlayerTrackerTransformChangedEventArgs([NotNull] EntityAssociatedData<PlayerNetworkTrackerChangeUpdateRequest> changeInformation)
		{
			ChangeInformation = changeInformation ?? throw new ArgumentNullException(nameof(changeInformation));
		}
	}
}
