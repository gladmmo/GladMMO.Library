using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILoadingScreenStateChangedEventSubscribable
	{
		event EventHandler<LoadingScreenStateChangedEventArgs> OnLoadingScreenStateChanged;
	}

	public sealed class LoadingScreenStateChangedEventArgs : EventArgs
	{
		public bool Active { get; private set; }

		public LoadingScreenStateChangedEventArgs(bool active)
		{
			Active = active;
		}
	}
}
