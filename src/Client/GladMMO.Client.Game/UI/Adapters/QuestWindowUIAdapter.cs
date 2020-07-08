using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class QuestWindowUIAdapter : BaseUnityUI<IUIQuestWindow>, IUIQuestWindow
	{
		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Title;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Description;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _Objective;

		public IUIText Title => _Title;

		public IUIText Description => _Description;

		public IUIText Objective => _Objective;
	}
}
