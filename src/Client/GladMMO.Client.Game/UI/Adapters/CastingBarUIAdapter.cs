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
		[SerializeField]
		private Text _CastingBarSpellNameText;

		[SerializeField]
		private Image _CastingBarFillable;

		[SerializeField]
		private Transform _CastingBarRoot;

		private Lazy<IUIText> _castingBarSpellNameText { get; }

		private Lazy<IUIFillableImage> _castingBarFillable { get; }

		public IUIFillableImage CastingBarFillable { get; }

		public IUIText CastingBarSpellNameText { get; }

		public CastingBarUIAdapter()
		{
			_castingBarSpellNameText = new Lazy<IUIText>(() => new UnityTextUITextAdapterImplementation(_CastingBarSpellNameText));
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
