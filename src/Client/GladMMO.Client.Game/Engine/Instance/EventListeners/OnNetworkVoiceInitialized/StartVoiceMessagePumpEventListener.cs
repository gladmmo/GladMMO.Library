using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class StartVoiceMessagePumpEventListener : BaseSingleEventListenerInitializable<IVoiceNetworkInitializedEventSubscribable>, IGameTickable
	{
		private bool isInitialized { get; set; } = false;

		private ILog Logger { get; }

		public StartVoiceMessagePumpEventListener(IVoiceNetworkInitializedEventSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			isInitialized = true;
		}

		public void Tick()
		{
			try
			{
				//TODO: Maybe hide this behind an extension
				if(isInitialized)
					VivoxUnity.Client.RunOnce();
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Vivox Error: {e.Message}\n\nStack: {e.StackTrace}");
			}
		}
	}
}
