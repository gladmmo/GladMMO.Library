using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GladMMO
{
	//From: //From HaloLive: https://github.com/HaloLive/HaloLive.Library/tree/9ca485677a8c6f85bf06de53193af704aa508dcd/src/HaloLive.Hosting.Authorization.Server/Services
	/// <summary>
	/// Claims reader based on https://github.com/aspnet/Identity/blob/f555a26b4a554f73eea70b4b34fca823fab9a643/src/Microsoft.Extensions.Identity.Core/UserManager.cs
	/// </summary>
	public interface IClaimsPrincipalReader
	{
		/// <summary>
		/// Returns the User ID claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The User ID claim value, or null if the claim is not present.</returns>
		/// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier"/>.</remarks>
		string GetUserId(ClaimsPrincipal principal);

		/// <summary>
		/// Returns the Name claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The Name claim value, or null if the claim is not present.</returns>
		/// <remarks>The Name claim is identified by <see cref="ClaimsIdentity.DefaultNameClaimType"/>.</remarks>
		string GetUserName(ClaimsPrincipal principal);

		/// <summary>
		/// Returns the pfid claim if present otherwise null.
		/// (non-standard claim)
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The pfid (PlayFab Id) claim or null if the claim is not present.</returns>
		string GetPlayfabId(ClaimsPrincipal principal);

		/// <summary>
		/// Indicates if the claim has the specified <see cref="role"/>.
		/// </summary>
		/// <param name="principal">The principal value.</param>
		/// <param name="role">The role.</param>
		/// <returns>True if the claim has the role.</returns>
		bool HasGuardiansRole(ClaimsPrincipal principal, GuardianApplicationRole role);

		/// <summary>
		/// Reads the UUID/GUID/JTI value from the <see cref="principal"/>.
		/// </summary>
		/// <param name="principal">The principal value.</param>
		/// <returns></returns>
		string GetGloballyUniqueUserId(ClaimsPrincipal principal);
	}
}