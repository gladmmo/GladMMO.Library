using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using VivoxUnity;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IVoiceSessionAuthenticatedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AuthenticateVoiceSessionEventListener : BaseSingleEventListenerInitializable<IVoiceNetworkInitializedEventSubscribable>, IVoiceSessionAuthenticatedEventSubscribable
	{
		private VivoxUnity.Client VoiceClient { get; }

		private ILog Logger { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		public event EventHandler<VoiceSessionAuthenticatedEventArgs> OnVoiceSessionAuthenticated;

		public AuthenticateVoiceSessionEventListener(IVoiceNetworkInitializedEventSubscribable subscriptionService, 
			[NotNull] ILog logger,
			[NotNull] VivoxUnity.Client voiceClient,
			[NotNull] ILocalPlayerDetails playerDetails) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					ProjectVersionStage.AssertBeta();
					//TODO: We should use authorative serverside authorization/tokens.
					ILoginSession session = VoiceClient.GetLoginSession(new AccountId("vrguardian-vrg-dev", PlayerDetails.LocalPlayerGuid.EntityId.ToString(), "vdx5.vivox.com"));

					await session.LoginAsync(new Uri("https://vdx5.www.vivox.com/api2"), session.GetLoginToken(VivoxDemoAPIKey.AccessKey, TimeSpan.FromSeconds(90)))
						.ConfigureAwait(true);

					//TODO: Does this above task complete immediately or does it wait until the state is known??
					if (session.State == LoginState.LoggedIn)
					{
						OnVoiceSessionAuthenticated?.Invoke(this, new VoiceSessionAuthenticatedEventArgs(session));
					}
					else
					{
						throw new InvalidOperationException($"Failed to authentication with Vivox.");
					}
				}
				catch(Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to Initialize Vivox Voice. Reason: {e.Message}\n\nStack: {e.StackTrace}");

					throw;
				}
			});
		}
	}
}
