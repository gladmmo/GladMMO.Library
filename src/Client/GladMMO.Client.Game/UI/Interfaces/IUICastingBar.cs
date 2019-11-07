using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUICastingBar
	{
		IUIFillableImage CastingBarFillable { get; }

		IUIElement CastingBarRoot { get; }

		IUIText CastingBarSpellNameText { get; }
	}
}
