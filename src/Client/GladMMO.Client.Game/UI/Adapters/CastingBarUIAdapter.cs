using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GladMMO
{
	public sealed class CastingBarUIAdapter : BaseUnityUI<IUICastingBar>, IUICastingBar
	{
		//TODO: This is a hack
		[SerializeField]
		private MonoBehaviour _CastingBarSpellNameText;

		[SerializeField]
		private Image _CastingBarFillable;

		[SerializeField]
		private Transform _CastingBarRoot;

		private Lazy<IUIText> _castingBarSpellNameText { get; }

		private Lazy<IUIFillableImage> _castingBarFillable { get; }

		public IUIFillableImage CastingBarFillable { get; }

		//TODO: This is a hack
		public IUIText CastingBarSpellNameText => (IUIText) _CastingBarSpellNameText;

		public CastingBarUIAdapter()
		{
			_castingBarFillable = new Lazy<IUIFillableImage>(() => new UnityImageUIFillableImageAdapterImplementation(_CastingBarFillable));
		}

		/// <inheritdoc />
		public void SetElementActive(bool state)
		{
			_CastingBarRoot.gameObject.SetActive(state);
		}

		public bool isActive => _CastingBarRoot.gameObject.activeSelf;
	}
}
