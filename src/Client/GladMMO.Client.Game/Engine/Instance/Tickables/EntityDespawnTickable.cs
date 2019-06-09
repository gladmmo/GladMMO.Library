using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class EntityDespawnTickable : EventQueueBasedTickable<INetworkEntityVisibilityLostEventSubscribable, NetworkEntityVisibilityLostEventArgs>
	{
		private IObjectDestructorable<NetworkEntityGuid> EntityDestructor { get; }

		private IReadonlyKnownEntitySet KnownEntites { get; }

		/// <inheritdoc />
		public EntityDespawnTickable(INetworkEntityVisibilityLostEventSubscribable subscriptionService, ILog logger,
			[NotNull] IObjectDestructorable<NetworkEntityGuid> entityDestructor,
			[NotNull] IReadonlyKnownEntitySet knownEntites) 
			: base(subscriptionService, true, logger)
		{
			EntityDestructor = entityDestructor ?? throw new ArgumentNullException(nameof(entityDestructor));
			KnownEntites = knownEntites ?? throw new ArgumentNullException(nameof(knownEntites));
		}

		/// <inheritdoc />
		protected override void HandleEvent(NetworkEntityVisibilityLostEventArgs args)
		{
			//If we don't know it, we likely encountered the rare edge case that is the result
			//of some hacks that were added to keep the wheels from falling off.
			//These will eventually be fixed, but for now we should just skip ones we don't know
			//because we can't remove what we don't know.
			if(!KnownEntites.isEntityKnown(args.EntityGuid))
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Encountered {nameof(NetworkEntityVisibilityLostEventArgs)} with Guid: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId} who is not a KNOWN entity. This should never happen.");

				return;
			}
			else
				if(Logger.IsInfoEnabled)
					Logger.Info($"About to cleanup Entity: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId}");

			//TODO: This is a semi-slow process, can any of this be offloaded to the other thread?
			EntityDestructor.Destroy(args.EntityGuid);
		}
	}
}
