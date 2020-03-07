using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public static class GladMMOAsyncSettings
	{
		//This mostly exists to combat issues with WebGL not supporting ConfigureAwaitFalse()
		//as pointed about by the ridiculous issue listed as "By Design"
		//here: https://issuetracker.unity3d.com/issues/webgl-task-awaited-with-task-dot-configureawait-false-does-not-continue-in-a-webgl-build
		public static bool ConfigureAwaitFalseSupported { get; set; } = true;

		public static ConfiguredTaskAwaitable<TResult> ConfigureAwaitFalse<TResult>(this Task<TResult> awaitable)
		{
			if (awaitable == null) throw new ArgumentNullException(nameof(awaitable));

			//Readaibility wise it is simpler to exprese it this way
			if (ConfigureAwaitFalseSupported)
				return awaitable.ConfigureAwait(false);
			else
				return awaitable.ConfigureAwait(true);
		}

		public static ConfiguredTaskAwaitable ConfigureAwaitFalseVoid(this Task awaitable)
		{
			if(awaitable == null) throw new ArgumentNullException(nameof(awaitable));

			//Readaibility wise it is simpler to exprese it this way
			if(ConfigureAwaitFalseSupported)
				return awaitable.ConfigureAwait(false);
			else
				return awaitable.ConfigureAwait(true);
		}
	}
}
