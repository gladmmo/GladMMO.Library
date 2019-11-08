using System;
using System.Collections.Generic;
using System.Text;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.UI;

namespace GladMMO.UI
{
	[Injectee]
	public sealed class ActionBarButtonEventDispatcher : MonoBehaviour
	{
		[Inject]
		private IActionBarButtonPressedEventPublisher PressPublisher;

		[SerializeField]
		public List<Button> ActionBarButtons = new List<Button>();

		void Start()
		{
			ActionBarIndex index = 0;
			foreach (var button in ActionBarButtons)
			{
				//Warning of modified closure.
				ActionBarIndex staticIndex = index;
				button.onClick.AddListener(() => PressPublisher.PublishEvent(this, new ActionBarButtonPressedEventArgs(staticIndex)));

				index++;
			}
		}
	}
}
