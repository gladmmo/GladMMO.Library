using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEngine;
using TMPro;

namespace GladMMO
{
	public sealed class TextMeshProUILabeledButtonAdapter : MonoBehaviour, IUILabeledButton
	{
		[SerializeField]
		private UnityButtonUIButtonAdapter ButtonAdapter;

		[SerializeField]
		private TextMeshProUGUIUITextAdapter ButtonText;

		public void AddOnClickListener(Action action)
		{
			ButtonAdapter.AddOnClickListener(action);
		}

		public void AddOnClickListenerAsync(Func<Task> action)
		{
			ButtonAdapter.AddOnClickListenerAsync(action);
		}

		public void RemoveOnClickListener(Action action)
		{
			ButtonAdapter.RemoveOnClickListener(action);
		}

		public void SimulateClick(bool eventsOnly)
		{
			ButtonAdapter.SimulateClick(eventsOnly);
		}

		public bool IsInteractable
		{
			get => ButtonAdapter.IsInteractable;
			set => ButtonAdapter.IsInteractable = value;
		}

		public string Text
		{
			get => ButtonText.Text;
			set => ButtonText.Text = value;
		}

		public void SetElementActive(bool state)
		{
			gameObject.SetActive(state);
		}

		public bool isActive => gameObject.activeSelf;
	}
}
