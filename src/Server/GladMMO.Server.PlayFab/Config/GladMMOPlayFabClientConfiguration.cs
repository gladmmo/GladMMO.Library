using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class GladMMOPlayFabClientConfiguration
	{
		public string PlayFabSecretKey { get; }

		public string PlayFabId { get; }

		public GladMMOPlayFabClientConfiguration(string playFabSecretKey, string playFabId)
		{
			//They can be null.
			PlayFabSecretKey = playFabSecretKey;
			PlayFabId = playFabId;
		}

		public void AssertContainsSecretKey()
		{
			if(String.IsNullOrWhiteSpace(PlayFabSecretKey))
				throw new InvalidOperationException($"Expected {PlayFabSecretKey} to be valid in {nameof(GladMMOPlayFabClientConfiguration)}.");
		}
	}
}
