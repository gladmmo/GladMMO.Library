using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Stub/Mock for old TypeSafe.Http.Net to help return to it
	/// in the future should a transition back occur.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	public class AuthenticationTokenAttribute : HeaderAttribute
	{
		public AuthenticationTokenAttribute()
			: base("Authorization")
		{
			
		}
	}
}
