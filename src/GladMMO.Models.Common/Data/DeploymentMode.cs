using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum DeploymentMode
	{
		/// <summary>
		/// Used for internal or local testing.
		/// </summary>
		Internal = 0,

		/// <summary>
		/// Used for non-local development or testing.
		/// </summary>
		PrivateDevelopment = 1,

		/// <summary>
		/// Used for public testing dev
		/// </summary>
		PublicDevelopment = 2,

		/// <summary>
		/// Used for the live deployment of the build.
		/// </summary>
		Production = 3,
	}
}
