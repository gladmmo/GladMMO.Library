using System;
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
		public NetworkEntityGuid TargetedEntity { get; }

		public LocalPlayerTargetChangedEventArgs([NotNull] NetworkEntityGuid targetedEntity)
		{
			TargetedEntity = targetedEntity ?? throw new ArgumentNullException(nameof(targetedEntity));
		}
	}
}
