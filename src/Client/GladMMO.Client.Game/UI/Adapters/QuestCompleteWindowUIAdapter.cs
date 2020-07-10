using System;
using System.Collections.Generic;
using System.Text;
using Glader;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class QuestCompleteWindowUIAdapter : BaseUnityUI<IUIQuestCompleteWindow>, IUIQuestCompleteWindow
	{
		[SerializeField]
		private UnityElementUIAdapter _RootElement;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Title;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Description;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Objective;

		[SerializeField]
		private UnityButtonUIButtonAdapter _AcceptButton;

		public IUIElement RootElement => _RootElement;

		public IUIText Title => _Title;

		public IUIText Description => _Description;

		public IUIText Objective => _Objective;

		public IUIButton AcceptButton => _AcceptButton;
	}
}
