using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	public enum ProxiedHttpMethod
	{
		Post = 1,
		Put = 2,
		Patch = 3
	}

	[JsonObject]
	public sealed class ProxiedHttpRequestModel
	{
		[JsonProperty]
		public ProxiedHttpMethod Method { get; private set; }

		[JsonProperty]
		public string SerializedJsonBody { get; private set; }

		[JsonProperty]
		public string AuthorizationToken { get; private set; }

		public ProxiedHttpRequestModel(ProxiedHttpMethod method, [NotNull] string serializedJsonBody, [NotNull] string authorizationToken)
		{
			if(!Enum.IsDefined(typeof(ProxiedHttpMethod), method)) throw new InvalidEnumArgumentException(nameof(method), (int)method, typeof(ProxiedHttpMethod));
			if (string.IsNullOrWhiteSpace(serializedJsonBody)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serializedJsonBody));
			if (string.IsNullOrWhiteSpace(authorizationToken)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(authorizationToken));

			Method = method;
			SerializedJsonBody = serializedJsonBody;
			AuthorizationToken = authorizationToken;
		}
	}
}
