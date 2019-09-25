using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine;

namespace GladMMO
{
	public sealed class CustomModelLoaderCancelableFactory : IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext>, IAvatarPrefabCompletedDownloadEventSubscribable
	{
		private ILoadableContentResourceManager ContentResourceManager { get; }

		private ILog Logger { get; }

		public event EventHandler<AvatarPrefabCompletedDownloadEventArgs> OnAvatarPrefabCompletedDownloading;

		public CustomModelLoaderCancelableFactory([NotNull] ILoadableContentResourceManager contentResourceManager, [NotNull] ILog logger)
		{
			ContentResourceManager = contentResourceManager ?? throw new ArgumentNullException(nameof(contentResourceManager));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public CustomModelLoaderCancelable Create(CustomModelLoaderCreationContext context)
		{
			Task<IPrefabContentResourceHandle> avatarPrefabAsync = ContentResourceManager.LoadAvatarPrefabAsync(context.ContentId);

			return new CustomModelLoaderCancelable(avatarPrefabAsync, Logger, prefab =>
			{
				//Callback should only occur is avatarPrefabAsync has completed
				IPrefabContentResourceHandle handle = avatarPrefabAsync.Result;

				OnAvatarPrefabCompletedDownloading?.Invoke(this, new AvatarPrefabCompletedDownloadEventArgs(handle, prefab, context.EntityGuid));
			});
		}
	}
}
