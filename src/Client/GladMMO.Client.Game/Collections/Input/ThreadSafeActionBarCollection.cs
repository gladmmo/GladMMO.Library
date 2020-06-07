using System; using FreecraftCore;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class ThreadSafeActionBarCollection : IActionBarCollection
	{
		private ConcurrentDictionary<ActionBarIndex, CharacterActionBarInstanceModel> BackingCollection { get; }

		public ThreadSafeActionBarCollection()
		{
			BackingCollection = new ConcurrentDictionary<ActionBarIndex, CharacterActionBarInstanceModel>();
		}

		public IEnumerator<CharacterActionBarInstanceModel> GetEnumerator()
		{
			return BackingCollection.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count => BackingCollection.Count;

		public bool IsSet(ActionBarIndex index)
		{
			if (BackingCollection.ContainsKey(index))
				return true; //Old design was we'd have EMPTY type
			else
				return false;
		}

		public CharacterActionBarInstanceModel this[ActionBarIndex index] => IsSet(index) ? BackingCollection[index] : null;

		public void Add([NotNull] CharacterActionBarInstanceModel actionBarModel)
		{
			if (actionBarModel == null) throw new ArgumentNullException(nameof(actionBarModel));

			//TODO: Throw if fails.
			BackingCollection.TryAdd(actionBarModel.BarIndex, actionBarModel);
		}
	}
}
