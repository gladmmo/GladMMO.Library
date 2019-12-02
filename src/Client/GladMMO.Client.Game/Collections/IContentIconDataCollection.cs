using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IContentIconDataCollection : IReadOnlyDictionary<int, ContentIconInstanceModel>
	{
		void Add(int key, ContentIconInstanceModel contentIconEntry);
	}

	public sealed class DefaultContentIconDataCollection : IContentIconDataCollection
	{
		private Dictionary<int, ContentIconInstanceModel> ContentIcons { get; }

		public DefaultContentIconDataCollection()
		{
			ContentIcons = new Dictionary<int, ContentIconInstanceModel>();
		}

		public IEnumerator<KeyValuePair<int, ContentIconInstanceModel>> GetEnumerator()
		{
			return ContentIcons.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) ContentIcons).GetEnumerator();
		}

		public int Count => ContentIcons.Count;

		public bool ContainsKey(int key)
		{
			return ContentIcons.ContainsKey(key);
		}

		public bool TryGetValue(int key, out ContentIconInstanceModel value)
		{
			return ContentIcons.TryGetValue(key, out value);
		}

		public ContentIconInstanceModel this[int key] => ContentIcons[key];

		public IEnumerable<int> Keys => ContentIcons.Keys;

		public IEnumerable<ContentIconInstanceModel> Values => ContentIcons.Values;

		public void Add(int key, [NotNull] ContentIconInstanceModel contentIconEntry)
		{
			if (contentIconEntry == null) throw new ArgumentNullException(nameof(contentIconEntry));
			if (key <= 0) throw new ArgumentOutOfRangeException(nameof(key));

			ContentIcons.Add(key, contentIconEntry);
		}
	}
}
