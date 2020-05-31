using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GladMMO
{
	public interface IWorldDownloadBeginEventSubscribable
	{
		event EventHandler<WorldDownloadBeginEventArgs> OnWorldDownloadBegins;
	}

	public sealed class WorldDownloadBeginEventArgs : EventArgs
	{
		public AsyncOperationHandle DownloadOperation { get; }

		public WorldDownloadBeginEventArgs([NotNull] AsyncOperationHandle downloadOperation)
		{
			DownloadOperation = downloadOperation;
		}
	}
}
