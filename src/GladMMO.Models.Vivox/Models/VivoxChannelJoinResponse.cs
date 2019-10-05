using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class VivoxChannelJoinResponse
	{
		[JsonRequired]
		[JsonProperty]
		public string AuthToken { get; private set; }

		[JsonProperty]
		[JsonRequired]
		public string ChannelURI { get; private set; }

		public VivoxChannelJoinResponse([JetBrains.Annotations.NotNull] string authToken, [JetBrains.Annotations.NotNull] string channelUri)
		{
			if (string.IsNullOrWhiteSpace(authToken)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(authToken));
			if (string.IsNullOrWhiteSpace(channelUri)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(channelUri));

			AuthToken = authToken;
			ChannelURI = channelUri;
		}

		[JsonConstructor]
		private VivoxChannelJoinResponse()
		{
			
		}
	}
}
