using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum ClientGameMode
	{
		/// <summary>
		/// Mode indicates default content should be loaded.
		/// Default implementations of services should be used.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Mode indicates Gaia Online related content and services should be used.
		/// </summary>
		GaiaOnline = 1,
	}
}
