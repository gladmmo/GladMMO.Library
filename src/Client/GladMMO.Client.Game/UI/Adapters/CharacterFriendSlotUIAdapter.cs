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
	public sealed class CharacterFriendSlotUIAdapter : BaseUnityUI<IUICharacterFriendSlot>, IUICharacterFriendSlot
	{
		public Button ButtonObject;

		[FormerlySerializedAs("TextObject")]
		public Text ButtonTextObject;

		public Text LevelTextObject;

		public Text LocationTextObject;

		private Lazy<IUIText> _levelText { get; }
		private Lazy<IUIText> _locationText { get; }

		public IUIText LevelText => _levelText.Value;

		public IUIText LocationText => _locationText.Value;

		public CharacterFriendSlotUIAdapter()
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

		public void AddOnClickListener([NotNull] Action action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			ButtonObject.onClick.AddListener(() => action());
		}

		public void AddOnClickListenerAsync(Func<Task> action)
		{
			if(action == null) throw new ArgumentNullException(nameof(action));

			ButtonObject.onClick.AddListener(() =>
			{
				StartCoroutine(this.AsyncCallbackHandler(action()));
			});
		}

		public void SimulateClick(bool eventsOnly)
		{
			throw new NotImplementedException($"TODO: Implement simulated click on friends");
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => ButtonObject.interactable;
			set => ButtonObject.interactable = value;
		}

		/// <inheritdoc />
		public string Text
		{
			get => ButtonTextObject.text;
			set => ButtonTextObject.text = value;
		}
	}
}
