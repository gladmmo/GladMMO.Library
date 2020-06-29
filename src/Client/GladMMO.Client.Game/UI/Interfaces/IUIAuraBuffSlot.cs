using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIAuraBuffSlot
	{
		AuraBuffType BuffType { get; }

		/// <summary>
		/// The text element responsible for the duration of the buff.
		/// </summary>
		IUIText DurationText { get; }

		/// <summary>
		/// Stack or charge aura buff counter.
		/// </summary>
		IUIText CounterText { get; }

		/// <summary>
		/// The icon image for the aura.
		/// </summary>
		IUIImage AuraIconImage { get; }

		/// <summary>
		/// The root element for the aura buff slot.
		/// </summary>
		IUIElement RootElement { get; }

		/// <summary>
		/// The button component of the aura.
		/// </summary>
		IUIButton AuraButton { get; }

		event EventHandler<bool> OnAuraBuffMouseHoverChanged;
	}
}
