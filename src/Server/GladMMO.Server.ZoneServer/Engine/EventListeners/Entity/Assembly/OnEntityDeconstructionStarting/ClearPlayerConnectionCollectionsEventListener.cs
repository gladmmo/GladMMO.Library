using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClearPlayerConnectionCollectionsEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionStartingEventSubscribable, EntityDeconstructionStartingEventArgs>
	{
		private IConnectionEntityCollection ConnectionToEntityMap { get; }

		private ILog Logger { get; }

		public ClearPlayerConnectionCollectionsEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService,
			[NotNull] IConnectionEntityCollection connectionToEntityMap,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			ConnectionToEntityMap = connectionToEntityMap ?? throw new ArgumentNullException(nameof(connectionToEntityMap));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EntityDeconstructionStartingEventArgs args)
		{
			//TODO: Abstract this.
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			//We need to unregister BOTH of these collections, session collection orginally was cleanedup
			//immediately on disconnect. Now it is not and must be done here.
			//ConnectionToEntityMap.Remove(0);
			if(Logger.IsWarnEnabled)
				Logger.Warn($"Leak Warning. ConnectionId to EntityGuid map is not being cleaned up. We need to eventually implement this.");
		}
	}
}
