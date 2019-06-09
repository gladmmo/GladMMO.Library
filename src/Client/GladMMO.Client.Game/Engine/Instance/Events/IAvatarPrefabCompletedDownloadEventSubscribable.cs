using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IAvatarPrefabCompletedDownloadEventSubscribable
	{
		event EventHandler<AvatarPrefabCompletedDownloadEventArgs> OnAvatarPrefabCompletedDownloading;
	}

	public sealed class AvatarPrefabCompletedDownloadEventArgs : EventArgs
	{
		public IPrefabContentResourceHandle PrefabHandle { get; }

		public GameObject DownloadedPrefabObject { get; }

		public NetworkEntityGuid EntityGuid { get; }

		public AvatarPrefabCompletedDownloadEventArgs([NotNull] IPrefabContentResourceHandle prefabHandle, [NotNull] GameObject downloadedPrefabObject, [NotNull] NetworkEntityGuid entityGuid)
		{
			PrefabHandle = prefabHandle ?? throw new ArgumentNullException(nameof(prefabHandle));
			DownloadedPrefabObject = downloadedPrefabObject ?? throw new ArgumentNullException(nameof(downloadedPrefabObject));
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
