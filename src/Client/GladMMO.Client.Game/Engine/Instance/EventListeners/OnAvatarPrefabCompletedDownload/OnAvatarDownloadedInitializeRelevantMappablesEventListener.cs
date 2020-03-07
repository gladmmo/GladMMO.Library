using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnAvatarDownloadedInitializeRelevantMappablesEventListener : BaseSingleEventListenerInitializable<IContentPrefabCompletedDownloadEventSubscribable, ContentPrefabCompletedDownloadEventArgs>
	{
		private IEntityGuidMappable<IPrefabContentResourceHandle> PrefabHandleMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private ILog Logger { get; }

		public OnAvatarDownloadedInitializeRelevantMappablesEventListener(IContentPrefabCompletedDownloadEventSubscribable subscriptionService, 
			[NotNull] IEntityGuidMappable<IPrefabContentResourceHandle> prefabHandleMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			PrefabHandleMappable = prefabHandleMappable ?? throw new ArgumentNullException(nameof(prefabHandleMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ContentPrefabCompletedDownloadEventArgs args)
		{
			//It's possible we don't know about the entity anymore, since this is a long async process.
			if (!KnownEntities.isEntityKnown(args.EntityGuid))
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Encountered {nameof(ContentPrefabCompletedDownloadEventArgs)} for unknown Entity: {args.EntityGuid}");
				return;
			}

			//We need to check, and release if there is any, before we replace it.
			if (PrefabHandleMappable.ContainsKey(args.EntityGuid))
			{
				IPrefabContentResourceHandle handle = PrefabHandleMappable.RetrieveEntity(args.EntityGuid);
				handle.Release();
				PrefabHandleMappable.RemoveEntityEntry(args.EntityGuid);
			}

			PrefabHandleMappable.AddObject(args.EntityGuid, args.PrefabHandle);
		}
	}
}
