using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerRotationChangeEventSubscribable
	{
		event EventHandler<PlayerRotiationChangeEventArgs> OnPlayerRotationChanged;
	}

	public sealed class PlayerRotiationChangeEventArgs : EventArgs
	{
		public ObjectGuid EntityGuid { get; }

		public float Rotation { get; }

		public PlayerRotiationChangeEventArgs([NotNull] ObjectGuid entityGuid, float rotation)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			Rotation = rotation;
		}
	}
}
