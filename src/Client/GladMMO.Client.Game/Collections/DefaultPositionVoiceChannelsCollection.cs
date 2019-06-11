using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	public sealed class DefaultPositionVoiceChannelsCollection : IPositionalVoiceChannelCollection
	{
		private List<IChannelSession> InternalChannels { get; } = new List<IChannelSession>(2);

		public IEnumerator<IChannelSession> GetEnumerator()
		{
			return InternalChannels.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) InternalChannels).GetEnumerator();
		}

		public void Add(IChannelSession item)
		{
			InternalChannels.Add(item);
		}

		public void Clear()
		{
			InternalChannels.Clear();
		}

		public bool Contains(IChannelSession item)
		{
			return InternalChannels.Contains(item);
		}

		public void CopyTo(IChannelSession[] array, int arrayIndex)
		{
			InternalChannels.CopyTo(array, arrayIndex);
		}

		public bool Remove(IChannelSession item)
		{
			return InternalChannels.Remove(item);
		}

		public int Count => InternalChannels.Count;

		public bool IsReadOnly => false;
	}
}
