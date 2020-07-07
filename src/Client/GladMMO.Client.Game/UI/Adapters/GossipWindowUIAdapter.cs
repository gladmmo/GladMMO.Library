using System;
using System.Collections.Generic;
using System.Text;
using Glader;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class GossipWindowUIAdapter : BaseUnityUI<IUIGossipWindow>, IUIGossipWindow
	{
		[SerializeField]
		private UnityElementUIAdapter _RootElement;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _GossipText;

		[SerializeField]
		private List<TextMeshProUILabeledButtonAdapter> _GossipMenuButtons;

		[SerializeField]
		private List<TextMeshProUILabeledButtonAdapter> _GossipQuestButtons;

		public IUIElement RootElement => _RootElement;

		public IUIText GossipText => _GossipText;

		public IReadOnlyList<IUILabeledButton> GossipMenuButtons => _GossipMenuButtons;

		public IReadOnlyList<IUILabeledButton> GossipQuestButtons => _GossipQuestButtons;
	}
}
