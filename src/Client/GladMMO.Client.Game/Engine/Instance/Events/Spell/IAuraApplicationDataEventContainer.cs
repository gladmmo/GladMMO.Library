using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public interface IAuraApplicationDataEventContainer
	{
		/// <summary>
		/// The aura slot to occupy.
		/// </summary>
		byte Slot { get; }

		/// <summary>
		/// The ID of the spell associated with the aura application.
		/// </summary>
		int SpellId { get; }

		/// <summary>
		/// The application data.
		/// </summary>
		AuraApplicationStateUpdate ApplicationData { get; }
	}
}
