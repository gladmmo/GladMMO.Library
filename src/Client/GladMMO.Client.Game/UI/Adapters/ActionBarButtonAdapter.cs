using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO
{
	public sealed class ActionBarButtonAdapter : BaseUnityUI<IUIActionBarButton>, IUIActionBarButton, IUIMouseBoundsEventListener
	{
		[SerializeField]
		private Image _ActionBarIcon;

		[SerializeField]
		private Button _ActionButton;

		private Lazy<IUIButton> _ActionBarButton { get; }

		private Lazy<IUIImage> _ActionBarImageIcon { get; }

		public IUIButton ActionBarButton => _ActionBarButton.Value;

		public IUIImage ActionBarImageIcon => _ActionBarImageIcon.Value;

		public bool isActive => _ActionBarIcon.isActiveAndEnabled && _ActionButton.isActiveAndEnabled;

		public event EventHandler<bool> OnActionBarMouseOverChanged;

		public ActionBarButtonAdapter()
		{
			_ActionBarButton = new Lazy<IUIButton>(() => new UnityButtonUIButtonAdapterImplementation(_ActionButton));
			_ActionBarImageIcon = new Lazy<IUIImage>(() => new UnityImageUIFillableImageAdapterImplementation(_ActionBarIcon));
		}

		public void SetElementActive(bool state)
		{
			_ActionBarIcon.gameObject.SetActive(state);
			_ActionButton.gameObject.SetActive(state);
		}

		public void OnMouseOver()
		{
			OnActionBarMouseOverChanged?.Invoke(this, true);
		}

		public void OnMouseExit()
		{
			OnActionBarMouseOverChanged?.Invoke(this, false);
		}
	}
}
