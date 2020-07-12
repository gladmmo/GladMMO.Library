using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public static class EventHandlerExtensions
	{
		/// <summary>
		/// Invokes the provided <see cref="eventHandler"/> on the Main Thread on the NEXT FRAME
		/// of Unity3D's update.
		/// </summary>
		/// <typeparam name="T">The args type.</typeparam>
		/// <param name="eventHandler"></param>
		/// <param name="source"></param>
		/// <param name="eventArgs"></param>
		public static void InvokeMainThread<T>(this EventHandler<T> eventHandler, object source, T eventArgs)
			where T : EventArgs
		{
			if (eventHandler == null)
				return;

			//Make sure to check Invoke again, thread could change it? Resharper says can never happen though. Not sure about that.
			UnityAsyncHelper.UnityMainThreadContext.Post(state => eventHandler?.Invoke(source, eventArgs), null);
		}
	}
}
