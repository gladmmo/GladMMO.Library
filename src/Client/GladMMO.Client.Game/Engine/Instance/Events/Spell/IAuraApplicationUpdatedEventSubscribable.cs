using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="AuraApplicationUpdatedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnAuraApplicationUpdated"/>
	/// </summary>
	public interface IAuraApplicationUpdatedEventSubscribable
	{
		event EventHandler<AuraApplicationUpdatedEventArgs> OnAuraApplicationUpdated;
	}

	/// <summary>
	/// Event arguments for the <see cref="IAuraApplicationUpdatedEventSubscribable"/> interface.
	/// </summary>
	public sealed class AuraApplicationUpdatedEventArgs : EventArgs, IAuraApplicationDataEventContainer
	{
		/// <summary>
		/// The aura slot to occupy.
		/// </summary>
		public byte Slot { get; }

		/// <summary>
		/// The ID of the spell associated with the aura application.
		/// </summary>
		public int SpellId { get; }

		/// <summary>
		/// The application data.
		/// </summary>
		public AuraApplicationStateUpdate ApplicationData { get; }

		public AuraApplicationUpdatedEventArgs(byte slot, int spellId, [NotNull] AuraApplicationStateUpdate applicationData)
		{
			if(spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
			ApplicationData = applicationData ?? throw new ArgumentNullException(nameof(applicationData));
			Slot = slot;
		}
	}
}