using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class ApplicationQuitOnBackButtonEventListener : ButtonClickedEventListener<ISceneBackButtonClickedSubscribable>
	{
		private ILog Logger { get; }

		public ApplicationQuitOnBackButtonEventListener(ISceneBackButtonClickedSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			if(!Application.isEditor)
				Application.Quit(0);
			else
			{
				//In the editor, log so it's known to be working
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Quit button pressed.");
			}
		}
	}
}
