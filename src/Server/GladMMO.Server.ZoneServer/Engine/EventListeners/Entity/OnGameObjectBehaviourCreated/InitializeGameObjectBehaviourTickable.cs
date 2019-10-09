using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeGameObjectBehaviourTickable : EventQueueBasedTickable<IGameObjectBehaviourCreatedEventSubscribable, GameObjectBehaviourCreatedEventArgs>
	{
		public InitializeGameObjectBehaviourTickable(IGameObjectBehaviourCreatedEventSubscribable subscriptionService, ILog logger) 
			: base(subscriptionService, true, logger)
		{

		}

		protected override void HandleEvent(GameObjectBehaviourCreatedEventArgs args)
		{
			if (args.Component is IBehaviourComponentInitializable gi)
				gi.Initialize();
		}
	}
}
