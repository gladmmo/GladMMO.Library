using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine;

namespace GladMMO
{
	public sealed class CustomModelLoaderCancelableFactory : IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext>, IContentPrefabCompletedDownloadEventSubscribable
	{
		private Dictionary<UserContentType, ILoadableContentResourceManager> ContentResourceManagers { get; }

		private ILog Logger { get; }

		public event EventHandler<ContentPrefabCompletedDownloadEventArgs> OnContentPrefabCompletedDownloading;

		public CustomModelLoaderCancelableFactory(
			[NotNull] IEnumerable<ILoadableContentResourceManager> contentResourceManagers, 
			[NotNull] ILog logger)
		{
			ContentResourceManagers = new Dictionary<UserContentType, ILoadableContentResourceManager>();

			//Create the resource manager map
			foreach (var resourceManager in contentResourceManagers)
				ContentResourceManagers.Add(resourceManager.ContentType, resourceManager);

			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public CustomModelLoaderCancelable Create(CustomModelLoaderCreationContext context)
		{
			if (!ContentResourceManagers.ContainsKey(context.ContentType))
			{
				throw new InvalidOperationException($"Cannot load content for EntityType: {context.EntityGuid.EntityType}");
			}

			ILoadableContentResourceManager contentResourceManager = ContentResourceManagers[context.ContentType];

			return CreateFromResourceManager(context, contentResourceManager);
		}

		private CustomModelLoaderCancelable CreateFromResourceManager(CustomModelLoaderCreationContext context, ILoadableContentResourceManager contentResourceManager)
		{
			Task<IPrefabContentResourceHandle> avatarPrefabAsync = contentResourceManager.LoadContentPrefabAsync(context.ContentId);

			return new CustomModelLoaderCancelable(avatarPrefabAsync, Logger, prefab =>
			{
				//Callback should only occur is avatarPrefabAsync has completed
				IPrefabContentResourceHandle handle = avatarPrefabAsync.Result;

				OnContentPrefabCompletedDownloading?.Invoke(this, new ContentPrefabCompletedDownloadEventArgs(handle, prefab, context.EntityGuid));
			});
		}
	}
}
