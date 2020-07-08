using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIQuestWindow
	{
		IUIElement RootElement { get; }

		IUIText Title { get; }

		IUIText Description { get; }

		IUIText Objective { get; }
	}
}
