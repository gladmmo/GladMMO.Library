using System; using FreecraftCore;
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
				throw new InvalidOperationException($"Cannot load content for EntityType: {context.EntityGuid.TypeId}");
			}

			ILoadableContentResourceManager contentResourceManager = ContentResourceManagers[context.ContentType];

			return CreateFromResourceManager(context, contentResourceManager);
		}

		private CustomModelLoaderCancelable CreateFromResourceManager(CustomModelLoaderCreationContext context, ILoadableContentResourceManager contentResourceManager)
		{
			Task<IPrefabContentResourceHandle> avatarPrefabAsync = contentResourceManager.LoadContentPrefabAsync(context.ContentId);

			return new CustomModelLoaderCancelable(avatarPrefabAsync, Logger, prefab =>
			{
				if (avatarPrefabAsync.IsFaulted)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to load Content: {contentResourceManager.ContentType} Id: {context.ContentId}");

					return;
				}

				try
				{
					//Callback should only occur is avatarPrefabAsync has completed
					IPrefabContentResourceHandle handle = avatarPrefabAsync.GetAwaiter().GetResult();

					OnContentPrefabCompletedDownloading?.Invoke(this, new ContentPrefabCompletedDownloadEventArgs(handle, prefab, context.EntityGuid));
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to load Creature Model Id: {context.ContentId}. Error: {e.Message}");

					throw;
				}
			});
		}
	}
}
