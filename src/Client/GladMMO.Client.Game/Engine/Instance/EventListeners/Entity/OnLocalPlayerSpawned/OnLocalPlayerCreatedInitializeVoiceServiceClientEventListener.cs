using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using VivoxUnity;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeVivoxOnStart : IGameStartable
	{
		private VivoxUnity.Client VoiceClient { get; }

		public InitializeVivoxOnStart([NotNull] VivoxUnity.Client voiceClient)
		{
			VoiceClient = voiceClient ?? throw new ArgumentNullException(nameof(voiceClient));
		}

		public async Task OnGameStart()
		{
			await new UnityYieldAwaitable();
			VoiceClient.Initialize();
		}
	}

	[AdditionalRegisterationAs(typeof(IVoiceNetworkInitializedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnLocalPlayerCreatedInitializeVoiceServiceClientEventListener : BaseSingleEventListenerInitializable<ILocalPlayerSpawnedEventSubscribable, LocalPlayerSpawnedEventArgs>, IVoiceNetworkInitializedEventSubscribable
	{
		private ILog Logger { get; }

		public event EventHandler OnVoiceNetworkInitialized;

		private IChatTextMessageRecievedEventPublisher TextChatPublisher { get; }

		public OnLocalPlayerCreatedInitializeVoiceServiceClientEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IChatTextMessageRecievedEventPublisher textChatPublisher) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			TextChatPublisher = textChatPublisher ?? throw new ArgumentNullException(nameof(textChatPublisher));
		}

		protected override void OnEventFired(object source, LocalPlayerSpawnedEventArgs args)
		{
			try
			{
				OnVoiceNetworkInitialized?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Vivox initialization failed. Reason: {e.Message}.\n\nStack: {e.StackTrace}");

				TextChatPublisher.PublishEvent(this, new TextChatEventArgs("Failed to connect to voice service.", ChatChannelType.System));
			}
		}
	}
}
