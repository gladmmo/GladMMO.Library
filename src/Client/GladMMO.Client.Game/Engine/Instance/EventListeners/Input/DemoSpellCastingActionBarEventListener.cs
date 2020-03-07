using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DemoSpellCastingActionBarEventListener : BaseActionBarButtonPressedEventListener
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyActionBarCollection ActionBarCollection { get; }

		private ILog Logger { get; }

		public DemoSpellCastingActionBarEventListener(IActionBarButtonPressedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyActionBarCollection actionBarCollection,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			ActionBarCollection = actionBarCollection ?? throw new ArgumentNullException(nameof(actionBarCollection));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnActionBarButtonPressed(ActionBarIndex index)
		{
			//If we have a set index then we can just send the request to interact/cast
			if (ActionBarCollection.IsSet(index))
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Action bar Index: {index} pressed.");

				if(ActionBarCollection[index].Type == ActionBarIndexType.Spell)
					SendService.SendMessage(new SpellCastRequestPayload(ActionBarCollection[index].ActionId));
			}
			else
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Action bar Index: {index} pressed but no associated action.");
			}
		}
	}
}
