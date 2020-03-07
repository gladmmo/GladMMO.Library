using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILocalPlayerTargetChangedEventListener
	{
		event EventHandler<LocalPlayerTargetChangedEventArgs> OnPlayerTargetChanged;
	}

	public class LocalPlayerTargetChangedEventArgs : EventArgs
	{
		public ObjectGuid TargetedEntity { get; }

		public LocalPlayerTargetChangedEventArgs([NotNull] ObjectGuid targetedEntity)
		{
			TargetedEntity = targetedEntity ?? throw new ArgumentNullException(nameof(targetedEntity));
		}
	}
}
