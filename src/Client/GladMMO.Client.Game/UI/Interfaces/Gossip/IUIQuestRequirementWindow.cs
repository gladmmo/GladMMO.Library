using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Basically the incomplete/required item
	/// window for WoW. Basically iterates to the user
	/// what the requirements for the quest is.
	/// </summary>
	public interface IUIQuestRequirementWindow : IUIWindow
	{
		IUIText Title { get; }

		IUIText Description { get; }

		IUIButton AcceptButton { get; }
	}
}
