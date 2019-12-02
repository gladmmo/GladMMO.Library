using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class ActionBarRowAdapter : BaseUnityUI<IUIActionBarRow>, IUIActionBarRow
	{
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

		public ActionBarRowAdapter()
		{
			if (ActionBarButtons.Count <= 0)
				throw new InvalidOperationException($"Cannot have actionbar row with 0 actionbar buttons.");

			if ((int)_endIndex < (int)_startIndex)
				throw new InvalidOperationException($"Cannot have {nameof(ActionBarIndex)} end lower than start.");

			foreach (var barButton in ActionBarButtons)
				ActionBarCollection.Add(StartIndex, barButton);
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
