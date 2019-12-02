using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO
{
	public sealed class ActionBarButtonAdapter : BaseUnityUI<IUIActionBarButton>, IUIActionBarButton
	{
		[SerializeField]
		private Image _ActionBarIcon;

		[SerializeField]
		private Button _ActionButton;

		private Lazy<IUIButton> _ActionBarButton { get; }

		private Lazy<IUIImage> _ActionBarImageIcon { get; }

		public IUIButton ActionBarButton => _ActionBarButton.Value;

		public IUIImage ActionBarImageIcon => _ActionBarImageIcon.Value;

		public ActionBarButtonAdapter()
		{
			_ActionBarButton = new Lazy<IUIButton>(() => new UnityButtonUIButtonAdapterImplementation(_ActionButton));
			_ActionBarImageIcon = new Lazy<IUIImage>(() => new UnityImageUIFillableImageAdapterImplementation(_ActionBarIcon));
		}
	}
}
