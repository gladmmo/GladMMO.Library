using System;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Attribute that indicates an authentication header will be used or is required.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class RequiresAuthenticationAttribute : HeadersAttribute
	{
		public RequiresAuthenticationAttribute()
			: base("Authorization: temp") //t for temp, will be replaced.
		{
		}
	}
}
