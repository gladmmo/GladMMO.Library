using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace GladMMO
{
	/// <summary>
	/// Marks an action with a <see cref="ResponseCacheAttribute"/>
	/// with caching disables.
	/// </summary>
	public sealed class NoResponseCacheAttribute : ResponseCacheAttribute
	{
		/// <summary>
		/// Creates a new <see cref="ResponseCacheAttribute"/> with
		/// disables caching.
		/// </summary>
		public NoResponseCacheAttribute()
		{
			//Disables caching and storing
			NoStore = true;
			Location = ResponseCacheLocation.None;
		}
	}
}
