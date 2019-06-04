using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Attribute that indicates server authentication is required.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class PlayFabServerAuthenticationHeaderAttribute : HeadersAttribute
	{
		public PlayFabServerAuthenticationHeaderAttribute()
			: base("X-SecretKey: temp") //t for temp, will be replaced.
		{
		}
	}
}
