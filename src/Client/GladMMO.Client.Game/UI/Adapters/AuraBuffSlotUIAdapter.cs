using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO
{
	public sealed class AuraBuffSlotUIAdapter : BaseUnityUI<IUIAuraBuffSlot>, IUIAuraBuffSlot, IUIMouseBoundsEventListener, IUIElement
	{
		[SerializeField]
		private TextMeshProUGUIUITextAdapter _DurationText;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter _CounterText;

		[SerializeField]
		private UnityImageUIImageAdapter _AuraIconImage;

		[SerializeField]
		private UnityButtonUIButtonAdapter _AuraButton;

		[SerializeField]
		private AuraBuffType _BuffType;

		public AuraBuffType BuffType => _BuffType;

		public IUIText DurationText => _DurationText;

		public IUIText CounterText => _CounterText;

		public IUIImage AuraIconImage =>_AuraIconImage;

		public IUIElement RootElement => this;

		public IUIButton AuraButton => _AuraButton;

		public bool isActive => gameObject.activeSelf;

		public event EventHandler<bool> OnAuraBuffMouseHoverChanged;

		public void OnMouseOver()
		{
			OnAuraBuffMouseHoverChanged?.Invoke(this, true);
		}

		public void OnMouseExit()
		{
			OnAuraBuffMouseHoverChanged?.Invoke(this, false);
		}

		public void SetElementActive(bool state)
		{
			gameObject.SetActive(state);
		}
	}
}
