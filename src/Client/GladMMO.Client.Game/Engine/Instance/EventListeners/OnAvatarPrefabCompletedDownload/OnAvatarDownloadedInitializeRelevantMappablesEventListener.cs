using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnAvatarDownloadedInitializeRelevantMappablesEventListener : BaseSingleEventListenerInitializable<IAvatarPrefabCompletedDownloadEventSubscribable, AvatarPrefabCompletedDownloadEventArgs>
	{
		private IEntityGuidMappable<IPrefabContentResourceHandle> PrefabHandleMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private ILog Logger { get; }

		public OnAvatarDownloadedInitializeRelevantMappablesEventListener(IAvatarPrefabCompletedDownloadEventSubscribable subscriptionService, 
			[NotNull] IEntityGuidMappable<IPrefabContentResourceHandle> prefabHandleMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			PrefabHandleMappable = prefabHandleMappable ?? throw new ArgumentNullException(nameof(prefabHandleMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, AvatarPrefabCompletedDownloadEventArgs args)
		{
			//It's possible we don't know about the entity anymore, since this is a long async process.
			if (!KnownEntities.isEntityKnown(args.EntityGuid))
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Encountered {nameof(AvatarPrefabCompletedDownloadEventArgs)} for unknown Entity: {args.EntityGuid}");
				return;
			}

			//We need to check, and release if there is any, before we replace it.
			if (PrefabHandleMappable.ContainsKey(args.EntityGuid))
			{
				IPrefabContentResourceHandle handle = PrefabHandleMappable.RetrieveEntity(args.EntityGuid);
				handle.Release();
				PrefabHandleMappable.Remove(args.EntityGuid);
			}

			PrefabHandleMappable.AddObject(args.EntityGuid, args.PrefabHandle);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Instantiating new Avatar for Entity: {args.EntityGuid}");

			//Now we've assigned the handle, we need to actually handle the spawning/loading of the avatar.
		}
	}
}
