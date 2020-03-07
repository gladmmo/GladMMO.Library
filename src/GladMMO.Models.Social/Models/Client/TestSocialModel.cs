using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class TestSocialModel : BaseSocialModel
	{
		[JsonProperty]
		public string TestMessage { get; private set; }

		public TestSocialModel([JetBrains.Annotations.NotNull] string testMessage)
		{
			if (string.IsNullOrWhiteSpace(testMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(testMessage));

			TestMessage = testMessage;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private TestSocialModel()
		{
			
		}
	}
}
