using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIQuestCompleteWindow : IUIWindow
	{
		IUIText Title { get; }

		IUIText Description { get; }

		IUIButton AcceptButton { get; }
	}
}
