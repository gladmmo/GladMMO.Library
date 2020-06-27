using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="AuraApplicationAppliedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnAuraApplicationApplied"/>
	/// </summary>
	public interface IAuraApplicationAppliedEventSubscribable
	{
		event EventHandler<AuraApplicationAppliedEventArgs> OnAuraApplicationApplied;
	}

	/// <summary>
	/// Event arguments for the <see cref="IAuraApplicationAppliedEventSubscribable"/> interface.
	/// </summary>
	public sealed class AuraApplicationAppliedEventArgs : EventArgs
	{
		public byte Slot { get; }

		public int SpellId { get; }

		public AuraApplicationStateUpdate ApplicationData { get; }

		public AuraApplicationAppliedEventArgs(byte slot, int spellId, [NotNull] AuraApplicationStateUpdate applicationData)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
			ApplicationData = applicationData ?? throw new ArgumentNullException(nameof(applicationData));
			Slot = slot;
		}
	}
}