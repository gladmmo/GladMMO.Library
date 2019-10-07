using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IWorldDownloadBeginEventSubscribable
	{
		event EventHandler<WorldDownloadBeginEventArgs> OnWorldDownloadBegins;
	}

	public sealed class WorldDownloadBeginEventArgs : EventArgs
	{
		public AsyncOperation DownloadOperation { get; }

		public WorldDownloadBeginEventArgs([NotNull] AsyncOperation downloadOperation)
		{
			DownloadOperation = downloadOperation ?? throw new ArgumentNullException(nameof(downloadOperation));
		}
	}
}
