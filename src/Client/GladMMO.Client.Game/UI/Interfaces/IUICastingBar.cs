using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUICastingBar : IUIElement
	{
		IUIFillableImage CastingBarFillable { get; }

		IUIText CastingBarSpellNameText { get; }
	}
}
