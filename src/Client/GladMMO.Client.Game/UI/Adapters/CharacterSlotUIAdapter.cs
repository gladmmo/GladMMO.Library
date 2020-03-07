using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GladMMO
{
	public sealed class CharacterSlotUIAdapter : BaseUnityUI<IUICharacterSlot>, IUICharacterSlot
	{
		public Toggle ToggleObject;

		[FormerlySerializedAs("TextObject")]
		public Text ButtonTextObject;

		public Text LevelTextObject;

		public Text LocationTextObject;

		private Lazy<IUIText> _levelText { get; }
		private Lazy<IUIText> _locationText { get; }

		public IUIText LevelText => _levelText.Value;

		public IUIText LocationText => _locationText.Value;

		public CharacterSlotUIAdapter()
		{
			_levelText = new Lazy<IUIText>(() => new UnityTextUITextAdapterImplementation(LevelTextObject));
			_locationText = new Lazy<IUIText>(() => new UnityTextUITextAdapterImplementation(LocationTextObject));
		}

		/// <inheritdoc />
		public void SetElementActive(bool state)
		{
			gameObject.SetActive(state);
		}

		public bool isActive => gameObject.activeSelf;

		/// <inheritdoc />
		public void AddOnToggleChangedListener(Action<bool> action)
		{
			ToggleObject.onValueChanged.AddListener(b => action(b));
		}

		/// <inheritdoc />
		public void AddOnToggleChangedListenerAsync(Func<bool, Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			ToggleObject.onValueChanged.AddListener(value =>
			{
				StartCoroutine(this.AsyncCallbackHandler(action(value)));
			});
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => ToggleObject.interactable;
			set => ToggleObject.interactable = value;
		}

		/// <inheritdoc />
		public string Text
		{
			get => ButtonTextObject.text;
			set => ButtonTextObject.text = value;
		}
	}
}
