using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class VivoxChannelData
	{
		/// <summary>
		/// Indictates if it's a 3D positional channel.
		/// </summary>
		public bool isPositionalChannel { get; }

		/// <summary>
		/// Indicates the channel's name.
		/// </summary>
		public string ChannelName { get; }

		public VivoxChannelData(bool isPositionalChannel, [JetBrains.Annotations.NotNull] string channelName)
		{
			if(string.IsNullOrWhiteSpace(channelName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(channelName));

			this.isPositionalChannel = isPositionalChannel;
			ChannelName = channelName;
		}
	}
}
