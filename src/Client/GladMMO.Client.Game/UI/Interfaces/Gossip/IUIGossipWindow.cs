using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIGossipWindow : IUIWindow
	{
		IUIElement RootElement { get; }

		IUIText GossipText { get; }

		IReadOnlyList<IUILabeledButton> GossipMenuButtons { get; }

		IReadOnlyList<IUILabeledButton> GossipQuestButtons { get; }

		void Clear();
	}
}
