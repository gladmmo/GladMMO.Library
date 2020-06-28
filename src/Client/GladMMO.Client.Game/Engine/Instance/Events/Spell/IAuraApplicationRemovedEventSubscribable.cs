using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="AuraApplicationRemovedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnAuraApplicationRemoved"/>
	/// </summary>
	public interface IAuraApplicationRemovedEventSubscribable
	{
		event EventHandler<AuraApplicationRemovedEventArgs> OnAuraApplicationRemoved;
	}

	/// <summary>
	/// Event arguments for the <see cref="IAuraApplicationRemovedEventSubscribable"/> interface.
	/// </summary>
	public sealed class AuraApplicationRemovedEventArgs : EventArgs
	{
		/// <summary>
		/// The aura's target guid.
		/// </summary>
		public ObjectGuid Target { get; }

		/// <summary>
		/// The aura slot to occupy.
		/// </summary>
		public byte Slot { get; }

		/// <summary>
		/// The ID of the spell associated with the aura application.
		/// </summary>
		public int SpellId { get; }

		public AuraApplicationRemovedEventArgs([NotNull] ObjectGuid target, byte slot, int spellId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			Slot = slot;
			SpellId = spellId;
			Target = target ?? throw new ArgumentNullException(nameof(target));
		}
	}
}