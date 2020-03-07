using System; using FreecraftCore;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	[Injectee]
	public sealed class ActionBarRowAdapter : BaseUnityUI<IUIActionBarRow>, IUIActionBarRow
	{
		[Inject]
		private IActionBarButtonPressedEventPublisher PressPublisher;

		//The buttons for the row.
		[SerializeField]
		private List<ActionBarButtonAdapter> ActionBarButtons;

		[SerializeField]
		private ActionBarIndex _startIndex;

		[SerializeField]
		private ActionBarIndex _endIndex;

		public ActionBarIndex StartIndex => _startIndex;

		public ActionBarIndex EndIndex => _endIndex;

		private Dictionary<ActionBarIndex, IUIActionBarButton> ActionBarCollection { get; } = new Dictionary<ActionBarIndex, IUIActionBarButton>(13);

		private void Start()
		{
			if (ActionBarButtons.Count <= 0)
				throw new InvalidOperationException($"Cannot have actionbar row with 0 actionbar buttons.");

			if ((int)_endIndex < (int)_startIndex)
				throw new InvalidOperationException($"Cannot have {nameof(ActionBarIndex)} end lower than start.");

			ActionBarIndex index = (ActionBarIndex)(StartIndex);

			foreach (var barButton in ActionBarButtons)
			{
				//For copying, static analysis warning.
				var index1 = index;

				barButton.ActionBarButton.AddOnClickListener(() => PressPublisher.PublishEvent(this, new ActionBarButtonPressedEventArgs(index1)));
				ActionBarCollection.Add(index, barButton);

				index += 1;
			}
		}

		public IEnumerator<KeyValuePair<ActionBarIndex, IUIActionBarButton>> GetEnumerator()
		{
			return ActionBarCollection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) ActionBarCollection).GetEnumerator();
		}

		public int Count => ActionBarCollection.Count;

		public bool ContainsKey(ActionBarIndex key)
		{
			return ActionBarCollection.ContainsKey(key);
		}

		public bool TryGetValue(ActionBarIndex key, out IUIActionBarButton value)
		{
			return ActionBarCollection.TryGetValue(key, out value);
		}

		public IUIActionBarButton this[ActionBarIndex key] => ActionBarCollection[key];

		public IEnumerable<ActionBarIndex> Keys => ActionBarCollection.Keys;

		public IEnumerable<IUIActionBarButton> Values => ActionBarCollection.Values;
	}
}
