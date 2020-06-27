using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="AuraStateChangedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnAuraStateChanged"/>
	/// </summary>
	public interface IAuraStateChangedEventSubscribable
	{
		event EventHandler<AuraStateChangedEventArgs> OnAuraStateChanged;
	}

	/// <summary>
	/// Event arguments for the <see cref="IAuraStateChangedEventSubscribable"/> interface.
	/// </summary>
	public sealed class AuraStateChangedEventArgs : EventArgs
	{
		/// <summary>
		/// The entity target for the aura state change.
		/// </summary>
		public ObjectGuid Target { get; }

		/// <summary>
		/// The data for the aura update.
		/// </summary>
		public AuraUpdateData Data { get; }

		public AuraStateChangedEventArgs([NotNull] ObjectGuid target, [NotNull] AuraUpdateData data)
		{
			Target = target ?? throw new ArgumentNullException(nameof(target));
			Data = data ?? throw new ArgumentNullException(nameof(data));
		}
	}
}