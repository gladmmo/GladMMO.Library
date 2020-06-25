using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a spell tooltip UI element.
	/// </summary>
	public interface IUISpellTooltip
	{
		/// <summary>
		/// Sets the text value of the specified <see cref="slot"/>
		/// to the provided <see cref="text"/>.
		/// </summary>
		/// <param name="slot">The tooltip data slot.</param>
		/// <param name="text">The text to set.</param>
		void SetText(SpellTooltipInfoSlot slot, string text);

		/// <summary>
		/// Sets the active state of the specified <see cref="slot"/>.
		/// </summary>
		/// <param name="slot">The tooltip data slot.</param>
		/// <param name="state">The active state to set.</param>
		void SetSlotState(SpellTooltipInfoSlot slot, bool state);

		/// <summary>
		/// Indicates if the specified <see cref="slot"/> is enabled/active.
		/// </summary>
		/// <param name="slot">The tooltip data slot.</param>
		bool IsSlotActive(SpellTooltipInfoSlot slot);

		/// <summary>
		/// Attempts to set the entire tooltip panel active state to <see cref="state"/>.
		/// </summary>
		/// <param name="state">The state.</param>
		/// <returns>The resulting active state.</returns>
		bool SetActive(bool state);
	}
}
