using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GladMMO
{
	/// <summary>
	/// Meta-data that marks an action/controller with an <see cref="AuthorizeAttribute"/>
	/// that indicates JWT should be used.
	/// </summary>
	public sealed class AuthorizeJwtAttribute : AuthorizeAttribute
	{
		/// <summary>
		/// Indicates that JWT should be used.
		/// </summary>
		public AuthorizeJwtAttribute()
		{
			AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
		}

		/// <summary>
		/// Indicates that JWT should be used.
		/// Also specifies the required Roles needed to authorize.
		/// </summary>
		public AuthorizeJwtAttribute(params GuardianApplicationRole[] role)
		{
			AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
			Roles = role?.Aggregate("", (s, applicationRole) => $"{s},{applicationRole.ToString()}");
		}
	}
}
