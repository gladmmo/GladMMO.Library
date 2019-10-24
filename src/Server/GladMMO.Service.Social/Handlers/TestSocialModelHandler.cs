using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class TestSocialModelHandler : BaseSignalRMessageHandler<TestSocialModel, IRemoteSocialHubClient>
	{
		private ILogger<TestSocialModelHandler> Logger { get; }

		public TestSocialModelHandler([JetBrains.Annotations.NotNull] ILogger<TestSocialModelHandler> logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override async Task OnMessageRecieved(IHubConnectionMessageContext<IRemoteSocialHubClient> context, TestSocialModel payload)
		{
			Logger.LogError($"Recieved TestMessage: {payload.TestMessage}");
		}
	}
}
